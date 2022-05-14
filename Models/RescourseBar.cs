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
    public class ResourceBar
    {
        public Texture2D texture;
        public Rectangle position;
        public double[] resources;
        public SpriteFont font;
        public int offset;
        public ResourceBar(SpriteFont font)
        {
            resources = new double[5];
            for(int i = 0; i < 5; i++)
            {
                if (i > 1)
                {
                    resources[i] = -1;
                }
                else {
                    resources[i] = 0;
                }
            }
            this.font = font;
        }

        public void updateResources(int index, double amount)
        {
            resources[index] += amount;
        }
        public void updateResources(int[] amounts)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                resources[i] += amounts[i];
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            for (int i = 0; i < resources.Length; i++)
            {
                if (resources[i] == -1)
                {
                    continue;
                }
                String s = " : " + formatString((int) resources[i]);
                Vector2 pos = new Vector2(position.X + 35 + offset, position.Y + i * 46 + 28);
                spriteBatch.DrawString(font, s, pos, Color.LightGray);
            }

        }

        public String formatString(int i)
        {
            String s = "";
            double temp = i;
            if (i >= 1000)
            {
                temp /= 1000.0;
                s += "K";
            }
            if (i >= 1000000)
            {
                temp /= 1000000.0;
                s += "M";
            }
            if (temp != 0)
            {
                temp = Math.Round(temp, 1);
            }
            s = temp + s;
            return s;
        }
    }
}
