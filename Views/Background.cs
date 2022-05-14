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
    class Background
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;


        public Background(Texture2D t, Rectangle r, Rectangle p, Color c)
        {
            tex = t;
            rec = r;
            pos = p;
            col = c;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            sb.Draw(tex, rec, pos, col);
        }
    }
}
