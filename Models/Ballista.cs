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
    public class Ballista
    {
        public Boolean canDamage;
        public Texture2D shadowTex;
        public Rectangle shadowPos;
        public Texture2D explosionTex;
        public List<Rectangle> sources = new List<Rectangle>();
        public const int FPS = 5;
        public Rectangle position;
        public Color color = new Color(255, 255, 255, 0);
        public int timer = 0;
        public int currentIndex = 0;
        public enum State { Shadow, Exploding, None };
        public State state = State.None;
        public EnemySuper enemyFocusingOn;
        public Ballista()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sources.Add(new Rectangle(j * 602, i * 602, 602, 602));
                }
            }
        }
        public void Update()
        {
            if (state == State.Shadow)
            {
                color.A += 8;
                if (color.A + 8 >= 255)
                {
                    color.A = 255;
                    canDamage = true;
                    state = State.Exploding;
                }
            }
            if (state == State.Exploding)
            {

                if (timer % FPS == 0)
                {
                    currentIndex++;
                    if (currentIndex > 5)
                    {
                        canDamage = false;
                    }
                    if (currentIndex > 8)
                    {
                        currentIndex = 0;
                        state = State.None;
                        canDamage = false;
                    }
                }
                timer++;
            }
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            if (state == State.Shadow)
            {
                spriteBatch.Draw(shadowTex, shadowPos, color);
            }

            if (state == State.Exploding)
            {
                spriteBatch.Draw(explosionTex, position, sources[currentIndex], Color.White);
            }
        }
    }
}
