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
    public class Boss : EnemySuper
    {
        public Texture2D[] bossTextures;
        public int[] numberOfSources = new int[7];
        public Rectangle idleSource;
        public List<Rectangle> lookSources = new List<Rectangle>();
        public List<Rectangle> maceSources = new List<Rectangle>();
        public List<Rectangle> laughSources = new List<Rectangle>();
        public List<Rectangle> swordSources = new List<Rectangle>();
        public List<Rectangle> destroySources = new List<Rectangle>();
        public Rectangle deathSource;
        public Texture2D swordTexture;
        public int[] lookOrder;
        public int[] maceOrder;
        public int[] laughOrder;
        public int[] swordOrder;
        public int soundIndex;
        public int[] destroyOrder;
        public SpriteFont font;
        public Vector2 textPos;
        public int textIndex;
        public String text = "";
        public Boolean isDying;
        public Boolean isDead;
        public int numPillars = 3;
        public List<String> texts = new List<String>();
        public List<String> hurtTexts = new List<String>();
        public Color textColor = new Color(255, 0, 0, 0);
        public int currentIndex;
        Random rand = new Random();
        public Boolean isTaunting;
        public Boolean isHurt;
        public Boolean[] hasBeenHurt = new Boolean[3];
        public enum AttackState { Idle, HellPillar, SpawnEnemy, SlowTowers, Looking, Laughing, Dying};
        public AttackState state;
        public List<HellfirePillar> pillars = new List<HellfirePillar>();
        public int timer = 0;
        public int tauntTimer = 0;
        public int hurtTimer = 0;
        public int lookTimer = 0;
        public int spawnTimer = 0;
        public int rotationVelocity = 0;
   
      
        public Boolean shouldContinue = false;
        public void Initialize() {
            soundIndex = 0;
            bossTextures = new Texture2D[7];
            bossTextures[0] = world.Content.Load<Texture2D>("Boss/bossIdle");
            bossTextures[1] = world.Content.Load<Texture2D>("Boss/bossDestroy");
            bossTextures[2] = world.Content.Load<Texture2D>("Boss/bossSword");
            bossTextures[3] = world.Content.Load<Texture2D>("Boss/bossMace");
            swordTexture = world.Content.Load<Texture2D>("Boss/GUI/bossSword");
            bossTextures[4] = world.Content.Load<Texture2D>("Boss/bossLooking");
            bossTextures[5] = world.Content.Load<Texture2D>("Boss/bossLaugh");
            bossTextures[6] = world.Content.Load<Texture2D>("Boss/bossBroken");
            font = world.Content.Load<SpriteFont>("Other/Font2");
            for (int i = 0; i < 3; i++) {
                for(int j = 0; j < 3; j++) {
                    lookSources.Add(new Rectangle(330 * j, 350 * i, 330, 350));
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    maceSources.Add(new Rectangle(330 * j, 350 * i, 330, 350));
                }
            }
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    laughSources.Add(new Rectangle(330 * j, 350 * i, 330, 350));
                }
            }
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    swordSources.Add(new Rectangle(264 * j, 280 * i, 264, 280));
                }
            }
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    destroySources.Add(new Rectangle(330 * j, 350 * i, 330, 350));
                }
            }
            deathSource = new Rectangle(0, 0, 330, 350);
            health = 40000;
            totalHealth = 40000;
            numberOfSources[0] = 10;
            numberOfSources[6] = 1;
            destroyOrder = new int[] { 1, 1, 1, 1, 2, 3, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 
            15, 16, 17, 18, 19, 20, 21, 22, 22, 22, 23, 24, 25 };
            numberOfSources[1] = destroyOrder.Length;

            swordOrder = new int[] {1, 2, 3, 4, 5, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 15, 15, 16, 17, 18, 19, 19, 19, 20, 21, 22, 23,
            24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35};
            numberOfSources[2] = swordOrder.Length;

            maceOrder = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 11, 11, 11, 11, 11, 11, 11, 12, 13, 14, 14, 15, 15, 16, 16,
            17, 17, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 18, 19, 20, 21, 22, 23, 24, 25, 1, 1, 1, 1, 1 };
            numberOfSources[3] = maceOrder.Length;

            lookOrder = new int[] { 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 8, 8, 8, 8, 8, 8, 8, 8, 7, 6, 5, 4, 3, 2 };
            numberOfSources[4] = lookOrder.Length;

            laughOrder = new int[] { 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 3, 3, 4, 4, 3, 3, 4, 4, 3, 3, 3, 3, 2, 1};
            numberOfSources[5] = laughOrder.Length;

            
            texts.Add("Weak.");
            texts.Add("Have you done your weekly papers?");
            texts.Add("You serve zero purpose.");
            texts.Add("I will end you... NOW!");
            texts.Add("Pathethic.");
            texts.Add("Your forces are nothing.");
            texts.Add("Pitiable.");
            texts.Add("Foolish.");
            texts.Add("You dare defy me?");
            texts.Add("Primitive constructs.");
            texts.Add("Kore ga, Requiem da.");
            texts.Add("YOUR LIFE IS MEANINGLESS!");
            texts.Add("Weak beyond belief.");
            texts.Add("Laughable.");

            hurtTexts.Add("Is that all you've got?!");
            hurtTexts.Add("I'm just getting started!");
            hurtTexts.Add("I WILL NOT DIE!");
            hurtTexts.Add("How... I...");

            currentIndex = 0;
            position = new Rectangle(100, 478, 66, 70);
            pixelsMoved = 100;
            color = Color.White;
            rotation = 270;
            idleSource = new Rectangle(0, 0, 330, 350);
            Rectangle a = lookSources[0];
            // a.Height -= 150;
            origin = new Vector2(a.Width / 2, a.Height / 2);
            dummyTexture = world.dummyTexture;
            //hitbox = new Rectangle(0, 0, 999, 999);
            hitbox = new Rectangle(position.X - 15, position.Y - 15, 30, 30);
            state = AttackState.Looking;
            speed = 1;
        }

        public override void incurDamage(int points, int towerType)
        {
            if(state == AttackState.Looking || state == AttackState.HellPillar) {
                return;
            }
            world.totalDamage += points;
            world.towerDamages[towerType] += points;
            //Console.WriteLine(health);
            health -= points;
            if(towerType == 3)
            {
                health -= points * 500;
                world.totalDamage += points * 500;
                world.towerDamages[towerType] += points * 500;
            }
            base.incurDamage(points, towerType);
           
            double a = (double)health / (double)totalHealth;
            a *= 796;
            world.healthBar.barPos.Width = (int)a;

            if (!hasBeenHurt[0] && (double)health / (double) totalHealth <= 0.75) {
                state = AttackState.HellPillar;
                hurtTimer = 0;
                isHurt = true;
                text = hurtTexts[0];
                currentIndex = 0;
                timer = 0;
                isTaunting = false;
                tauntTimer = 0;
                textColor.A = 0;
                resetOrigin();
                calculateTauntPos();
                hasBeenHurt[0] = true;
            }

            if (!hasBeenHurt[1] && (double)health / (double)totalHealth <= 0.50)
            {
                state = AttackState.HellPillar;
                hurtTimer = 0;
                isHurt = true;
                text = hurtTexts[1];
                currentIndex = 0;
                timer = 0;
                isTaunting = false;
                resetOrigin();
                tauntTimer = 0;
                textColor.A = 0;
                calculateTauntPos();
                hasBeenHurt[1] = true;
            }

            if (!hasBeenHurt[2] && (double)health / (double)totalHealth <= 0.25)
            {
                state = AttackState.HellPillar;
                hurtTimer = 0;
                isHurt = true;
                text = hurtTexts[2];
                numPillars = 4;
                currentIndex = 0;
                textColor.A = 0;
                resetOrigin();
                isTaunting = false;
                calculateTauntPos();
                timer = 0;
                tauntTimer = 0;
                hasBeenHurt[2] = true;
            }
            if(health <= 0 && !isDying)
            {
                isDying = true;
                state = AttackState.Dying;
                hurtTimer = 0;
                isHurt = true;
                MediaPlayer.Stop();
                markForDeletion = true;
                for(int i = 0; i < world.activeEnemies.Count; i++) {
                    world.activeEnemies[i].health = 0;
                }
                text = hurtTexts[3];
                numPillars = 0;
                currentIndex = 0;
                textColor.A = 0;
                resetOrigin();
                isTaunting = false;
                calculateTauntPos();
                timer = 0;
                tauntTimer = 0;
            }
        }
        public void doLook() {
            if (lookTimer == 60)
            {
                rotationVelocity = -1;
            }
            else if (lookTimer == 100)
            {
                rotationVelocity = 0;
            }
            else if (lookTimer == 150)
            {
                rotationVelocity = 1;
            }
            else if (lookTimer == 230)
            {
                rotationVelocity = 0;

            }
            else if (lookTimer == 280)
            {
                rotationVelocity = -1;
                world.healthBar.isFillingUp = true;
            }
            else if (lookTimer == 320)
            {
                rotationVelocity = 0;
            }
            else if (lookTimer == 360)
            {
                rotationVelocity = 0;
                shouldContinue = true;
            }
            rotation += rotationVelocity;
            lookTimer++;
        }
        public void doDaze() {
            for(int i = 0; i < world.attackTowers.Count; i++) {
                AttackSuper temp = world.attackTowers[i];
                Vector2 towerPos = new Vector2(temp.position.X, temp.position.Y);
                Vector2 thisPos = new Vector2(position.X, position.Y);
                Vector2 dist = Vector2.Subtract(towerPos, thisPos);
                Double d = dist.X * dist.X;
                d += dist.Y * dist.Y;
                d = Math.Sqrt(d);
                if(d < 150) {
                    world.attackTowers[i].isDazed = true;
                }
            }
        }
        public void doTaunt() {
            
            if(tauntTimer <= 50) {
                textColor.A += 5;
            }
            if(tauntTimer >= 150 && tauntTimer < 200) {
                textColor.A -= 5;
                
            }
            if(tauntTimer == 200)
            {
                isTaunting = false;
            }
            if(tauntTimer <= text.Length * 3 + 1 && tauntTimer % 3 == 0) {
                world.sfx.PlaySound("blipLow");
            }
            tauntTimer++;

        }
        public void doCry()
        {
            if (rotation <= 360 && isDying)
            {
                rotation += 1;
            }
            if(hurtTimer >= 100 && hurtTimer <= 150 && isDying) {
                textColor.A += 5;
            }
            if(hurtTimer >= 300 && hurtTimer <= 350 && isDying) {
                textColor.A -= 5;
            }
            if(hurtTimer == 400 && isDying) {
                isDead = true;
            }

            if(soundIndex < text.Length && isDying && hurtTimer >= 100) {
                if(text.Substring(soundIndex, 1).Equals(" ")) {
                    if(hurtTimer % 20 == 0) {
                        soundIndex++;
                    }
                }
                else if(text.Substring(soundIndex, 1).Equals(".")) {
                    if(hurtTimer % 12 == 0) {
                        soundIndex++;
                        world.sfx.PlaySound("blipLow");
                    }

                }
                else {
                    if(hurtTimer % 3 == 0) {
                        soundIndex++;
                        world.sfx.PlaySound("blipLow");
                    }
                }
            }



            if (hurtTimer <= 50 && !isDying) {
                textColor.A += 5;
            }
            
            if (hurtTimer >= 150 && hurtTimer <= 200 && !isDying)
            {
                textColor.A -= 5;

            }
            
            if(hurtTimer == 100 && !isDying) {
                world.sfx.PlaySound("hellPillar", 0.1f);
            }
            if(hurtTimer == 120 && !isDying) {
                doPillar();
                
            }
            if (hurtTimer == 250)
            {
                isHurt = false;
                timer = 0;
                currentIndex = 0;
                
            }
            hurtTimer++;
        }
        public void calculateTauntPos() {
            Vector2 temp = font.MeasureString(text);
            textColor.A = 0;
            textPos = new Vector2(position.X - temp.X / 2, position.Y - 50);
        }
        public void doSpawn()
        {
            if(spawnTimer == 150)
            {
                if ((double)health / (double)totalHealth >= 0.5)
                {
                    world.levelLoad.spawnMyster();
                }
                else
                {
                    world.levelLoad.spawnMotorizer();
                }
            }
            if(spawnTimer == 200)
            {
                if ((double)health / (double)totalHealth >= 0.5)
                {
                    world.levelLoad.spawnMyster();

                }
                else
                {
                    world.levelLoad.spawnMotorizer();
                }
            }
            if(spawnTimer == 250)
            {
                if ((double)health / (double)totalHealth >= 0.5)
                {
                    world.levelLoad.spawnMyster();
                }
                else
                {
                    world.levelLoad.spawnMotorizer();

                }
            }
            
            spawnTimer++;
            
        }
        public void doPillar() {
            for(int i = 1; i <= numPillars; i++) {
                if(world.attackTowers.Count - i >= 0) {
                    HellfirePillar temp = new HellfirePillar();
                    temp.world = world;
                    AttackSuper delete = world.attackTowers[world.attackTowers.Count - i];
                    temp.towerToDelete = delete;
                    if (delete.doesTurn)
                    {
                        temp.pillarPos = new Rectangle(delete.position.X - 34, delete.position.Y - 26, 66, 64);
                    }
                    else {
                        temp.pillarPos = new Rectangle(delete.position.X - 34, delete.position.Y - 32, 66, 64);
                    }
                    temp.tileToReplace = world.getTile(delete.position.X, delete.position.Y);
                    temp.Initialize();
                    pillars.Add(temp);
                }
            }
            
        }
        public override void Update() {
            for(int i = 0; i < pillars.Count; i++) {
                pillars[i].Update();
                if(pillars[i].shouldDelete) {
                    pillars.RemoveAt(i);
                }
            }
            if(isDying) {
                doCry();
                return;
            }
            if(state == AttackState.Looking) {
                doLook();
            }
            if (isTaunting)
            {
                doTaunt();
            }
            else if (isHurt)
            {
                doCry();

            }
            if (state == AttackState.SpawnEnemy)
            {
                doSpawn();
            }
            if(state == AttackState.Dying)
            {
                return;
            }
            if(timer % 5 == 0) {
                
                if(currentIndex == 14 && state == AttackState.Looking && !shouldContinue) {
                    return;
                }
                if(currentIndex == 6 && state == AttackState.SlowTowers) {
                    world.sfx.PlaySound("mace", 0.2f);
                }
                if(currentIndex == 20 && state == AttackState.SlowTowers) {
                    doDaze();
                }
                if (currentIndex == 0 && state == AttackState.Laughing) {
                    isTaunting = true;
                }
                if(currentIndex == 8 && state == AttackState.SpawnEnemy) {
                    world.sfx.PlaySound("sword", 0.2f);
                }
                currentIndex++;
                
                if(currentIndex >= numberOfSources[(int) state]) {
                    //Console.WriteLine((int)state);
                    if (state != AttackState.Idle) {
                        state = AttackState.Idle;
                        timer = 0;
                    }
                    resetOrigin();
                    currentIndex = 0;
                }
            }
            if(timer > 300) {
                timer = 0;
                int a = rand.Next(3);
                if (a == 0)
                {
                    state = AttackState.SlowTowers;
                    
                }
                else if(a == 1)
                {
                    state = AttackState.SpawnEnemy;
                    spawnTimer = 0;
                }
                else {
                    state = AttackState.Laughing;
                    tauntTimer = 0;
                    text = texts[textIndex];
                    isTaunting = true;
                    textIndex++;
                    textColor.A = 0;
                    calculateTauntPos();
                    if(textIndex >= texts.Count) {
                        textIndex = 0;
                    }
                }
                resetOrigin();
            }
            timer++;
           
        }
        public void resetOrigin()
        {
            Rectangle a;
            a = lookSources[0];
            if(state == AttackState.SpawnEnemy)
            {
                a = swordSources[0];
            }
           
            // a.Height -= 150;
            origin = new Vector2(a.Width / 2, a.Height / 2);
        }
        public override void upStep()
        {
            rotation = 180;
            position.Y -= speed;
            hitbox.Y -= speed;
        }
        public override void downStep()
        {
            rotation = 0;
            position.Y += speed;
            hitbox.Y += speed;
        }
        public override void leftStep()
        {
            rotation = 90;
            position.X -= speed;
            hitbox.X -= speed;
        }
        public override void rightStep()
        {
            rotation = 270;
            position.X += speed;
            hitbox.X += speed;
        }

        public override void pathing()
        {
            
            if(state != AttackState.Idle) {
                return;
            }
            otherTimer++;
            if(otherTimer % 3 == 0) {
                Tile t = world.getTile(position.X, position.Y);
                if (t.isCorner)
                {
                    if (t.rotation == 0)
                    {
                        if (position.Y <= t.position.Y)
                        {
                            position.Y = t.position.Y;
                            hitbox.Y = t.position.Y - 15;
                            movementDirection = 0;
                        }
                    }
                    if (t.rotation == 90)
                    {
                        if (position.X >= t.position.X)
                        {
                            position.X = t.position.X;
                            hitbox.Y = t.position.Y - 15;
                            movementDirection = 1;
                        }
                    }
                    if (t.rotation == 180)
                    {
                        if (position.X >= t.position.X)
                        {
                            position.X = t.position.X;
                            hitbox.Y = t.position.Y - 15;
                            movementDirection = 2;
                        }
                    }
                    if (t.rotation == 270)
                    {
                        if (position.Y >= t.position.Y)
                        {
                            position.Y = t.position.Y;
                            hitbox.Y = t.position.Y - 15;
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

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (state == AttackState.Idle) {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), idleSource,
                new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, idleSource,
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            if(state == AttackState.Looking) {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), lookSources[lookOrder[currentIndex] - 1],
                    new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, lookSources[lookOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            if (state == AttackState.SlowTowers)
            {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), maceSources[maceOrder[currentIndex] - 1],
                    new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, maceSources[maceOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            if (state == AttackState.Laughing)
            {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), laughSources[laughOrder[currentIndex] - 1],
                    new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, laughSources[laughOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
               
            }
            if (state == AttackState.SpawnEnemy)
            {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), swordSources[swordOrder[currentIndex] - 1],
                    new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, swordSources[swordOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(swordTexture, position, swordSources[swordOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);

            }
            if(state == AttackState.HellPillar) {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), destroySources[destroyOrder[currentIndex] - 1],
                       new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, destroySources[destroyOrder[currentIndex] - 1],
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            if(state == AttackState.Dying)
            {
                spriteBatch.Draw(bossTextures[(int)state], new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), deathSource,
                      new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(bossTextures[(int)state], position, deathSource,
                color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            }
            for (int i = 0; i < pillars.Count; i++)
            {
                pillars[i].Draw(gameTime, spriteBatch);
                
            }
            spriteBatch.DrawString(font, text, textPos, textColor);
            //spriteBatch.Draw(dummyTexture, hitbox, Color.White);
        }
    }
}
