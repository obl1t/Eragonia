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
    public class BarButtons
    {
        public enum Selected { Attack, Resource, Tier, Pressed };
        public Selected current = Selected.Attack;
        public Texture2D texture;
        public Rectangle[] sources = new Rectangle[4];
        public Rectangle position;
        public int offset;
        public BarButtons()
        {

            sources[0] = new Rectangle(0, 0, 210, 39);
            sources[1] = new Rectangle(0, 39, 210, 39);
            sources[2] = new Rectangle(0, 78, 210, 39);
            sources[3] = new Rectangle(0, 39, 210, 39);
        }

        public void isClicking(MouseState mouse)
        {
            if (mouse.X + offset >= position.X && mouse.X + offset<= position.X + 32 && mouse.Y  >= position.Y && mouse.Y <= position.Y + 26 && mouse.LeftButton == ButtonState.Pressed)
            {
                current = Selected.Attack;
            }
            if (mouse.X + offset >= position.X + 54 && mouse.X + offset<= position.X + 86 && mouse.Y >= position.Y && mouse.Y <= position.Y + 26 && mouse.LeftButton == ButtonState.Pressed)
            {
                current = Selected.Resource;
            }
            if (mouse.X + offset >= position.X + 108 && mouse.X + offset<= position.X + 140 && mouse.Y>= position.Y && mouse.Y <= position.Y + 26 && mouse.LeftButton == ButtonState.Pressed)
            {
                current = Selected.Tier;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // TODO: Add your drawing code here
            spriteBatch.Draw(texture, position, sources[(int)current], Color.White);
        }

    }
}
