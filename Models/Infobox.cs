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
    public class Infobox
    {
        public SpriteFont font;
        public Texture2D texture;
        public Rectangle position;
        public String text = "" ;
        public Boolean isInitialized = false;
        public Infobox() {
            text = " ";
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isInitialized)
            {

                spriteBatch.Draw(texture, position, Color.White);
                spriteBatch.DrawString(font, text, new Vector2(position.X + 25, position.Y + 20), Color.LightGray);
            }
        }
    }
}
