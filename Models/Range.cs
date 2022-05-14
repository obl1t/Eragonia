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
    public class Range
    {
        public Texture2D texture;
        public Vector2 center;
        public int radius;
        public Rectangle position;
        public Rectangle source;
        public World world;

        public void addPosition()
        {
            position = new Rectangle((int)center.X, (int)center.Y, radius * 2, radius * 2);
            source = new Rectangle(0, 0, 2048, 2048);
        }
        public Boolean isInRange(Rectangle other)
        {

            Vector2 otherCenter = new Vector2(other.X , other.Y );
            double temp = Vector2.Distance(center, otherCenter);
            return radius >= temp;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(source.Width / 2, source.Height / 2), SpriteEffects.None, 0.0f);
        }
    }
}
