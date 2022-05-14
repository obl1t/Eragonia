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
    class Motorizer : EnemySuper
    {
        public int slowTimer = 0;
        public int chargeTimer = 0;
        public List<int> chargeAnimOrder = new List<int>();
        public List<Rectangle> chargeSources = new List<Rectangle>();
        public Texture2D chargeTexture;
        public int chargeCurrentIndex = 0;
        public int boostTimer = 0;
        public List<int> boostAnimOrder = new List<int>();
        public List<Rectangle> boostSources = new List<Rectangle>();
        public Texture2D boostTexture;
        public int boostCurrentIndex = 0;
        public enum moveState { slow, charge, fast };
        moveState mS = moveState.slow;

        public Motorizer() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            addChargeAnimOrder(new int[] { 1, 2, 3, 4, 5 });
            addBoostAnimOrder(new int[] { 1, 2, 3, 4 });
            enemyStartIndex = 3;
            chargeCurrentIndex = 0;
            boostCurrentIndex = 0;
            rotation = 0;
            color = Color.White;
            speed = 2;
            health = 3000.0;
            goldWorth = 75;
            //prestigeWorth = 10;
            totalHealth = 3000;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 682, i * 682, 682, 682));
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    chargeSources.Add(new Rectangle(j * 682, i * 682, 682, 682));
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    boostSources.Add(new Rectangle(j * 682, i * 682, 682, 682));
                }
            }
        }

        public void addChargeAnimOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                chargeAnimOrder.Add(order[i] - 1);
            }
        }

        public void addBoostAnimOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                boostAnimOrder.Add(order[i] - 1);
            }
        }


        public override void Update()
        {
            base.Update();

            if (mS == moveState.slow)
            {
                speed = 3;
                slowTimer++;

                if (slowTimer % 300 == 0)
                {
                    slowTimer = 0;
                    mS = moveState.charge;
                }
            }
            else if (mS == moveState.charge)
            {
                speed = 0;
                chargeTimer++;

                if (chargeTimer % 120 == 0)
                {
                    chargeCurrentIndex++;
                    chargeTimer = 0;
                }

                if (chargeCurrentIndex > 0)
                {
                    if (chargeTimer % 30 == 0)
                    {
                        chargeTimer = 0;
                        if (chargeCurrentIndex == 4)
                        {
                            mS = moveState.fast;
                            chargeTimer = 0;
                            chargeCurrentIndex = 0;
                        }
                        else
                        {
                            chargeCurrentIndex++;
                        }
                    }
                }
            }
            else
            {
                speed = 5;
                boostTimer++;

                if (boostTimer % FRAMES_PER_FRAME == 0)
                {
                    boostCurrentIndex++;
                    if (boostCurrentIndex >= boostAnimOrder.Count)
                    {
                        boostCurrentIndex = 0;
                    }
                }

                if (boostTimer % 180 == 0)
                {
                    boostTimer = 0;
                    mS = moveState.slow;
                }
            }
        }

        public override void incurDamage(int points, int towerType)
        {
            health -= points;
            base.incurDamage(points, towerType);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (mS == moveState.slow)
                base.Draw(gameTime, spriteBatch);
            else if (mS == moveState.charge)
            {
                spriteBatch.Draw(chargeTexture, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), chargeSources[chargeAnimOrder[chargeCurrentIndex]],
               new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(chargeTexture, position, chargeSources[chargeAnimOrder[chargeCurrentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                if (showHealth)
                    spriteBatch.Draw(healthSprite, healthBar, new Rectangle(0, 0, 1152, 648), healthColor);
            }
            else
            {
                spriteBatch.Draw(boostTexture, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), boostSources[boostAnimOrder[boostCurrentIndex]],
               new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(boostTexture, position, boostSources[boostAnimOrder[boostCurrentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                if (showHealth)
                    spriteBatch.Draw(healthSprite, healthBar, new Rectangle(0, 0, 1152, 648), healthColor);
            }
        }
    }
}
