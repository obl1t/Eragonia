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
using System.IO;

namespace Eragonia_Demo_Day_One
{
    public class EnemySuper
    {
        public Texture2D spriteSheet;
        public Texture2D healthSprite;
        public List<Rectangle> sources = new List<Rectangle>();
        public Rectangle position;
        public Texture2D dummyTexture;
        public List<int> animationOrder = new List<int>();
        public int currentIndex = 0;
        public Color color;
        public Color healthColor = Color.Green;
        public Vector2 origin;
        public int enemyStartIndex;
        public int otherTimer = 0;
        public int speed;
        public double health = 10.0;
        public double totalHealth = 10;
        public int goldWorth = 0;
        public int prestigeWorth = 1;
        public Boolean markForDeletion = false;
        public Boolean showHealth = false;
        public Rectangle hitbox;
        public int rotation = 0;
        public Shockwave wave;
        public Boolean isBurning;
        public int burningTimer = 0;
        public int damageOverTime;
        public int maxBurn;
        public const int FRAMES_PER_FRAME = 5;
        int timer = 0;
        public Boolean isInvisible;
        public World world;
        public Rectangle healthBar;
        public double pixelsMoved;
        public Boolean isFirstHit = true;
        public Texture2D fireIcon;
        public Rectangle firePosition;
        public int movementDirection = 0;
        public Boolean shouldMotor = false;
        public Boolean isSped = false;
        public List<Ballista> projectiles = new List<Ballista>();
        public int timerInv = 1;
        public Boolean inInvs = false;
        public Boolean canEndGame = true;
        public EnemySuper()
        {

        }

        public virtual void setHealth()
        {
            health *= world.healthDivider;
        }

        public virtual void setAttack()
        {
            currentIndex = enemyStartIndex;
            timer = enemyStartIndex * 10;
        }

        public virtual void incurDamage(int points, int towerType)
        {

            world.totalDamage += points;
            world.towerDamages[towerType] += points;
            if (isFirstHit && world.applyCrowbar)
            {
                isFirstHit = false;
                firePosition = new Rectangle(position.X + 10, position.Y + 10, 24, 24);
                health -= 10;
                world.towerDamages[towerType] += points;
                world.totalDamage += 10;
            }
            else if (isFirstHit)
            {
                firePosition = new Rectangle(position.X + 10, position.Y + 10, 24, 24);
                isFirstHit = false;
            }
            showHealth = true;
            healthBar.Width = (int)(((double)health / (double)totalHealth) * position.Width) + 1;

            int x = (int)((double)health / totalHealth * 255);

            healthColor.G = (byte)x;
            healthColor.R = (byte)(255 - x);

        }


        public virtual void addAnimationOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                animationOrder.Add(order[i] - 1);
            }
        }

        public virtual void stopGoing()
        {

        }

        public virtual void uncurDamage()
        {

        }

        public virtual void calculateOrigin()
        {
            Rectangle a = sources[0];
            // a.Height -= 150;
            origin = new Vector2(a.Width / 2, a.Height / 2);
            //hitbox = new Rectangle(0, 0, 999, 999);
            hitbox = new Rectangle(position.X - 15, position.Y - 15, 30, 30);
           
        }

        public virtual void Update()
        {

            stopGoing();
            uncurDamage();

            if (!isFirstHit && color.A != 255)
            {
                inInvs = true;
                hitbox.Y = -1000;
                for(int i = 0; i < world.attackTowers.Count; i++)
                {
                    world.attackTowers[i].enemyFocusingOn = null;
                }
                timerInv++;
            }
            if (timerInv % 300 == 0)
            {
                color.A = 255;
                inInvs = false;
                hitbox = new Rectangle(position.X - 15, position.Y - 15, 30, 30);
            }

            timer++;
            firePosition.X = position.X + 10;
            firePosition.Y = position.Y + 10;
            if (timer % FRAMES_PER_FRAME == 0)
            {
                currentIndex++;
                if (currentIndex >= animationOrder.Count)
                {
                    currentIndex = 0;
                }
            }
            if (isBurning && timer % 10 == 0)
            {
                incurDamage(damageOverTime, 3);
            }
            if (isBurning)
            {
                burningTimer++;
                if (burningTimer >= maxBurn)
                {
                    burningTimer = 0;
                    isBurning = false;
                    damageOverTime = 0;
                }
            }
            if (health <= 0)
            {
                markForDeletion = true;
                position.X = -500;
                world.gamePrestige += 1 * world.prestigeMult;
            }


            healthColor.A = 240;

            healthBar.X = position.X - 30;
            healthBar.Y = position.Y - 30;
        }

        public virtual void upStep()
        {
            rotation = 0;
            position.Y -= speed;
            hitbox.Y -= speed;
        }
        public virtual void downStep()
        {
            rotation = 180;
            position.Y += speed;
            hitbox.Y += speed;
        }
        public virtual void leftStep()
        {
            rotation = 270;
            position.X -= speed;
            hitbox.X -= speed;
        }
        public virtual void rightStep()
        {
            rotation = 90;
            position.X += speed;
            hitbox.X += speed;
        }

        public virtual void pathing()
        {
            otherTimer++;
            pixelsMoved += (double)(speed / 2.0);
            if (otherTimer % 2 == 0)
            {
                Tile t = world.getTile(position.X, position.Y);
                if (t.isCorner)
                {
                    if (t.rotation == 0)
                    {
                        if (position.Y <= t.position.Y)
                        {
                            position.Y = t.position.Y;
                            movementDirection = 0;
                        }
                    }
                    if (t.rotation == 90)
                    {
                        if (position.X >= t.position.X)
                        {
                            position.X = t.position.X;
                            movementDirection = 1;
                        }
                    }
                    if (t.rotation == 180)
                    {
                        if (position.X >= t.position.X)
                        {
                            position.X = t.position.X;
                            movementDirection = 2;
                        }
                    }
                    if (t.rotation == 270)
                    {
                        if (position.Y >= t.position.Y)
                        {
                            position.Y = t.position.Y;
                            movementDirection = 0;
                        }
                    }
                }
                if (movementDirection == 0)
                {
                    rightStep();
                }
                if (movementDirection == 1)
                {
                    downStep();
                }
                if (movementDirection == 2)
                {
                    upStep();
                }
            }

        }


        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), sources[animationOrder[currentIndex]],
            new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(spriteSheet, position, sources[animationOrder[currentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            if (showHealth)
                spriteBatch.Draw(healthSprite, healthBar, new Rectangle(0, 0, 1152, 648), healthColor);
            if (isBurning)
                spriteBatch.Draw(fireIcon, firePosition, Color.White);
            //spriteBatch.Draw(dummyTexture, hitbox, Color.White);
        }
    }
}

