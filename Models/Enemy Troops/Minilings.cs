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
    public class Miniling : EnemySuper
    {
        public Miniling() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4, 5, 6 });
            enemyStartIndex = 3;
            rotation = 0;
            color = Color.White;
            speed = 3;
            health = 15;
            goldWorth = 0;
            //prestigeWorth = 10;
            totalHealth = 15;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 320, i * 320, 320, 320));
                }
            }

        }
        public override void incurDamage(int points, int towerType)
        {
            health -= points;
            base.incurDamage(points, towerType);
        }

        public override void Update()
        {
            base.Update();
            
        }
       
    }
}
