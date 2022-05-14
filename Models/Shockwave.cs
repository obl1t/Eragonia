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
    public class Shockwave
    {
        public Texture2D texture;
        public int rotation;
        public Rectangle position;
        Vector2 velocity;
        Vector2 location;
        public Color color;
        public int maxNumHits;
        public Boolean doesBurn;
        public int damageOverTime;
        public int maxBurn;
        public Boolean shouldRemove = false;
        public void setVelocity()
        {
            double a = Math.Cos(MathHelper.ToRadians(rotation));
            double b = Math.Sin(MathHelper.ToRadians(rotation));
            velocity = new Vector2((float)a * 5, (float)b * 5);
            location.X = position.X;
            location.Y = position.Y;
        }
        public void Update()
        {
            velocity.X *= 0.92f;
            velocity.Y *= 0.92f;
            color.A = (byte)((int)color.A * 0.94);
            if (color.A <= 30)
            {
                shouldRemove = true;
            }
            location.X += velocity.X;
            location.Y += velocity.Y;
            position.X = (int)location.X;
            position.Y = (int)location.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, 136, 336), color, MathHelper.ToRadians(rotation), new Vector2(136 / 2, 336 / 2), SpriteEffects.None, 0.0f);
        }
    }
}
