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
    public class PauseInfoBox
    {
        public Texture2D texture;
        public Rectangle position;
        public String text = "";
        public SpriteFont fonte;
        public Color col = Color.White;
        public int painNum;
        public int offset;
        public PauseInfoBox(Texture2D t, Rectangle p, String s, SpriteFont f)
        {
            text = s;
            position = p;
            texture = t;
            fonte = f;
        }
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            sb.Draw(texture, position, col);
            sb.DrawString(fonte, text, new Vector2(fonte.MeasureString(text).X + painNum + offset, position.Y + 125), col);
        }

    }
}
