using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Eragonia_Demo_Day_One
{
    public class AttackSuper
    {

        public enum AttackState { Attacking, Idle };
        public Texture2D spriteSheet;
        public List<Rectangle> sources = new List<Rectangle>();
        public Rectangle position { get; set; }
        public List<int> animationOrder = new List<int>();
        public int currentIndex = 0;
        public Color color;
        public Vector2 origin;
        public int attackStartIndex;
        public List<int> idleOrder = new List<int>();
        public int speed;
        public int damage;
        public World world;
        public Boolean doesTurn = true;
        public int rotation = 0;
        public const int FRAMES_PER_FRAME = 5;
        public Range range;
        public EnemySuper enemyFocusingOn;
        public Boolean canAttack;
        public Boolean showRange;
        public Boolean isPlaced;
        public Boolean enemyInRange;
        public int upgrade1Damage;
        public int upgrade2Damage;
        public int upgrade1Range;
        public int upgrade2Range;
        public MouseState oldMouse;
        public int id;
        public Boolean isDazed;
        public Daze daze = new Daze();
        public AttackState attack = AttackState.Idle;
        public UpgradeButton upgrade = new UpgradeButton();
        public TierUpButton tierUp = new TierUpButton();
        public Texture2D tierUpTex;
        public Texture2D upgradeTexture;
        public Texture2D infoTex;
        public Infobox box = new Infobox();
        public SpriteFont font;
        public int trueDamage;
        public int currentTier = 0;
        public int upgradeIndex = 0;
        public double additionalDamageMult = 1;
        public String[] infoTexts = new String[5];
        public Boolean isOverUpgrade = false;
        public int timer = 0;
        public int offset = 150;
        public int dazeTimer = 240;
        public FormatterMaster formatter = new FormatterMaster();
        public Icon icon = new Icon();
        public SoundEffectInstance currentSoundPlaying;
        public AttackSuper()
        {
            oldMouse = Mouse.GetState();

        }
        public void setUpgrade()
        {
            upgrade.texture = upgradeTexture;
            formatter.offset = world.offsetX;
            upgrade.position = formatter.formatRect(new Rectangle(position.X - 60, position.Y - 70, 40, 40));
            upgrade.tower = this;
            upgrade.offset = world.offsetX - 640;
            icon.texture = world.Content.Load<Texture2D>("Towers/GUI/icons");
            icon.position = new Rectangle(position.X + 15, position.Y + 20, 15, 15);
            tierUp.texture = tierUpTex;
            tierUp.position = formatter.formatRect(new Rectangle(position.X + 20, position.Y - 70, 40, 40));
            tierUp.tower = this;
            tierUp.offset = world.offsetX - 640;
            tierUp.world = world;
            box.texture = infoTex;
            box.position = formatter.formatRect(new Rectangle(position.X - 125, position.Y - 250, 250, 300));
            box.font = font;
            upgrade.world = world;
            daze.world = world;
            daze.position = new Rectangle(position.X, position.Y, 32, 32);
            daze.Initialize();
            box.isInitialized = true;
        }
        public int getState()
        {

            return (int)attack;
        }
        public void face(Rectangle other)
        {
            if(isDazed) {
                return;
            }
            Vector2 temp = new Vector2(other.X - this.position.X + 16, other.Y - this.position.Y + 16);
            rotation = (int)MathHelper.ToDegrees((float)Math.Atan2((double)temp.Y, (double)temp.X)) - 90;
        }
        public virtual void setAttack()
        {
            attack = AttackState.Attacking;
            currentIndex = attackStartIndex;
            timer = attackStartIndex * 10;
        }
        public void setIdle()
        {
            attack = AttackState.Idle;
            currentIndex = 0;
            if(currentSoundPlaying != null) {
                currentSoundPlaying.Stop();
            }
            timer = 0;
        }
        public virtual void addRange(Texture2D rangeTex, int radius)
        {
            range = new Range();
            range.center = new Vector2(position.X, position.Y);
            range.radius = radius;
            range.world = world;
            range.texture = rangeTex;
            range.addPosition();

        }
        public Boolean isInRange(EnemySuper e)
        {
            if (e.inInvs)
            {
                return false;
            }
            return range.isInRange(e.position);
        }
        public Boolean isInRange(Rectangle pos) {
            return range.isInRange(pos);
        }
        public void addAnimationOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                animationOrder.Add(order[i] - 1);
            }
        }
        public void addIdleOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                idleOrder.Add(order[i] - 1);
            }
        }
        public void calculateOrigin()
        {
            Rectangle a = sources[0];
            a.Height -= offset;
            origin = new Vector2(a.Width / 2, a.Height / 2);
        }
        public virtual void isBeingClicked(MouseState mouse)
        {
            if (!world.upgrade1Unlocked && upgradeIndex == 0)
            {
                upgrade.showInfoBox = false;
            }
            else if (!world.upgrade2Unlocked && upgradeIndex == 1)
            {
                upgrade.showInfoBox = false;
                world.shownInfoBox = null;
                upgrade.type = UpgradeButton.ViewType.Default;
            }
            else if (showRange)
            {
                upgrade.showInfoBox = true;

                //world.shownInfoBox = box;
            }
            if (world.era > 0 && this.currentTier == 0)
            {
                tierUp.showInfoBox = true;
            }
            if (world.era > 1 && this.currentTier == 1)
            {
                tierUp.showInfoBox = true;
            }


            if (upgrade.showInfoBox && showRange)
            {
                upgrade.isClicking(mouse);
            }
            if (tierUp.showInfoBox && showRange)
            {
                tierUp.isClicking(mouse);
            }


            if (mouse.X + world.offsetX - 640 >= position.X - position.Width / 2 && mouse.X + world.offsetX  - 640 <= position.X + position.Width / 2)
            {
                if (mouse.Y >= position.Y - position.Height / 2 && mouse.Y <= position.Y + position.Height / 2)
                {
                    if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                    {
                        showRange = !showRange;
                        if (showRange)
                        {
                            world.shownUpgrade = upgrade;
                            world.shownTier = tierUp;
                            AttackSuper temp = this;
                            world.attackTowers.Remove(this);
                            world.attackTowers.Add(temp);
                            world.sfx.PlaySound("click");
                            
                        }
                        else
                        {
                            world.shownUpgrade = null;
                            world.shownTier = null;

                        }
                        world.shownRangeID = id;
                    }

                }
            }
            if (world.shownRangeID != id)
            {
                showRange = false;
            }
            
            //world.shownInfoBox = upgrade.box;



            oldMouse = mouse;
        }
        public virtual void Update()
        {

            if(isDazed) {
                daze.Update();
                if (currentSoundPlaying != null)
                {
                    currentSoundPlaying.Stop();
                }
                dazeTimer--;
                if(dazeTimer <= 0) {
                    isDazed = false;
                    dazeTimer = 240;
                }
                return;
            }

            trueDamage = (int)(damage * world.damageMultiplier * additionalDamageMult);
            if (upgradeIndex == 1)
            {
                icon.index = 3;
            }
            if (upgradeIndex == 2)
            {
                icon.index = 4;
            }
            if (isOverUpgrade)
            {
                box.text = infoTexts[upgradeIndex];
                
            }
         
            else
            {
                box.text = infoTexts[currentTier + 2];
            }
            if(box.text == null) {
                box.text = "";
            }
            if (attack == AttackState.Attacking)
            {

                // Console.WriteLine(timer);

                if (timer % FRAMES_PER_FRAME == 0)
                {

                    currentIndex++;

                    if (currentIndex >= animationOrder.Count)
                    {
                        currentIndex = 0;
                    }
                }
                if (timer % speed == 0)
                {
                    doAttack();
                }
            }

            else
            {
                if (timer % FRAMES_PER_FRAME == 0)
                {

                    currentIndex++;
                    if (currentIndex >= idleOrder.Count)
                    {
                        currentIndex = 0;
                    }
                }
            }
            timer++;


        }
        public virtual void checkUpgrade()
        {

        }
        public virtual void checkTier()
        {
            
        }
        public virtual void doAttack()
        {
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (showRange)
            {
                range.Draw(gameTime, spriteBatch);
                upgrade.Draw(gameTime, spriteBatch);
                tierUp.Draw(gameTime, spriteBatch);
            }
            if (attack == AttackState.Attacking)
            {

                spriteBatch.Draw(spriteSheet, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), sources[animationOrder[currentIndex]],
                new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(spriteSheet, position, sources[animationOrder[currentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            else
            {
                spriteBatch.Draw(spriteSheet, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), sources[idleOrder[currentIndex]],
                new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(spriteSheet, position, sources[idleOrder[currentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);

            }
            if(isDazed) {
                daze.Draw(gameTime, spriteBatch);
            }
            icon.Draw(gameTime, spriteBatch);

        }
    }
}
