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
    class Mariner : EnemySuper
    {
        public Mariner() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            enemyStartIndex = 3;
            rotation = 0;
            color = Color.White;
            speed = 2;
            health = 800.0;
            goldWorth = 30;
            //prestigeWorth = 10;
            totalHealth = 800;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 682, i * 682, 682, 682));
                }
            }

        }


        public override void incurDamage(int points, int towerType)
        {
            if (towerType == 3)
            {
                return;
            }
            health -= points;
            base.incurDamage(points, towerType);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheet, new Rectangle(position.X + 3, position.Y + 3, position.Width, position.Height), sources[animationOrder[currentIndex]],
            new Color(0, 0, 0, 120), MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            spriteBatch.Draw(spriteSheet, position, sources[animationOrder[currentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            if (showHealth)
                spriteBatch.Draw(healthSprite, healthBar, new Rectangle(0, 0, 1152, 648), healthColor);
            //spriteBatch.Draw(dummyTexture, hitbox, Color.White);
        }
    }
}
