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
    class TipsAndTricks
    {
        Random rand = new Random();

        public Color col;
        public SpriteFont font;
        public List<String> text = new List<String>();
        public string seeText;



        public TipsAndTricks(Color c, SpriteFont f)
        {
            col = c;
            font = f;

            seeText = "";
        }

        public void changeTip()
        {
            seeText = text[rand.Next(0, text.Count)];
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.DrawString(font, seeText, new Vector2(100, 880), col);
        }
    }
}
