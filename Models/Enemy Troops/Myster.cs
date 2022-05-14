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
    class Myster : EnemySuper
    {
        int timer = 0;

        public Myster() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            enemyStartIndex = 3;
            rotation = 0;
            color = Color.White;
            speed = 2;
            health = 4000.0;
            goldWorth = 50;
            //prestigeWorth = 10;
            totalHealth = 4000;

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
            health -= points;
            base.incurDamage(points, towerType);
        }

        public override void uncurDamage()
        {
            timer++;
            if (health <= 3980 && timer % 180 == 0)
            {
                health += 20;
            }
            else if (health < 4000 && health > 3980 && timer % 180 == 0)
            {
                health += (4000 - health);
            }
        }
    }
}
