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
    class Mirage : EnemySuper
    {
        public int timer = 0;
        public WaveLoader loader;

        public Mirage() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            enemyStartIndex = 3;
            rotation = 0;
            color = Color.White;
            speed = 2;
            health = 2500.0;
            goldWorth = 60;
            //prestigeWorth = 10;
            totalHealth = 2500;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 682, i * 682, 682, 682));
                }
            }

        }

        public override void Update()
        {
            base.Update();


            timer++;

            if (timer % 600 == 0)
                spawnShadow();
        }

        public void spawnShadow()
        {
            loader.spawnShadow(position.X, position.Y, pixelsMoved, movementDirection);
        }

        public override void incurDamage(int points, int towerType)
        {
            health -= points;
            base.incurDamage(points, towerType);
        }
    }
}
