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
    class Mace : EnemySuper
    {

        public int spawnTimer = 0;
        public List<int> passiveAnimOrder = new List<int>();
        public List<Rectangle> passiveSources = new List<Rectangle>();
        public Texture2D passiveTexture;
        public int passiveCurrentIndex = 0;
        public Boolean isPassive = false;
        public WaveLoader loader;
        public Mace() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            addPassiveAnimOrder(new int[] { 1, 2, 3, 4, 5, 6, 7, 8 });
            enemyStartIndex = 3;
            rotation = 0;
            color = Color.White;
            speed = 1;
            health = 280;
            goldWorth = 15;
            //prestigeWorth = 10;
            totalHealth = 280;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 320, i * 320, 320, 320));
                }
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    passiveSources.Add(new Rectangle(j * 320, i * 320, 320, 320));
                }
            }

        }

        public void addPassiveAnimOrder(int[] order)
        {
            for (int i = 0; i < order.Length; i++)
            {
                passiveAnimOrder.Add(order[i] - 1);
            }
        }

        public override void Update()
        {

            base.Update();
            spawnTimer++;
            if (!isPassive)
            {
                speed = 1;
                if (spawnTimer % 180 == 0)
                {
                    spawnTimer = 0;
                    isPassive = true;
                }
            }
            else
            {
                speed = 0;
                if(spawnTimer % 10 == 0)
                {
                    passiveCurrentIndex++;
                    if(passiveCurrentIndex > 7)
                    {
                        passiveCurrentIndex = 0;
                    }
                }
                if(spawnTimer == 70)
                {
                    spawnMiniling();
                    
                   
                }
                if(spawnTimer == 80)
                {
                    isPassive = false;
                    spawnTimer = 0;
                }
            }
        }

        public override void incurDamage(int points, int towerType)
        {
            health -= points;
            base.incurDamage(points, towerType);
        }
        public void spawnMiniling()
        {
            loader.spawnMiniling(position.X, position.Y, pixelsMoved, movementDirection);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isPassive)
            {
                base.Draw(gameTime, spriteBatch);
            }
            else
            {
                spriteBatch.Draw(passiveTexture, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), passiveSources[passiveAnimOrder[passiveCurrentIndex]],
                new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                spriteBatch.Draw(passiveTexture, position, passiveSources[passiveAnimOrder[passiveCurrentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
                if (showHealth)
                    spriteBatch.Draw(healthSprite, healthBar, new Rectangle(0, 0, 1152, 648), healthColor);
            }
        }

    }
}
