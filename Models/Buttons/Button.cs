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
    public class Button
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle[] pos;
        public Rectangle posPos;
        public Color col;
        public enum buttonPress { notPressed, hover, pressed }
        public buttonPress bp;
        public buttonPress oldBp;
        public String text;
        public Boolean hasFont;
        public int yOffset = 0;
        public SpriteFont font;
        public int painNum;
        public World world;
        public int offset;
        public Boolean forceClickSound = false;
        public Button(Texture2D t, Rectangle r, Rectangle[] p, Color c, SpriteFont f, Boolean hF)
        {
            tex = t;
            rec = r;
            pos = p;
            posPos = pos[0];
            col = c;
            bp = buttonPress.notPressed;
            oldBp = buttonPress.notPressed;
            font = f;
            hasFont = hF;
        }



        public void isOverChoice(int mouseX, int mouseY, MouseState m, MouseState oldM)
        {
            if (mouseX + offset >= rec.Left && mouseX + offset <= rec.Right && mouseY >= rec.Top && mouseY <= rec.Bottom)
            {
                bp = buttonPress.hover;
                if (m.LeftButton == ButtonState.Pressed && oldM.LeftButton != ButtonState.Pressed)
                {
                    bp = buttonPress.pressed;
                    
                }
                if((oldBp != buttonPress.hover && world != null && world.gS != World.gameState.Config) || (oldBp != buttonPress.hover && forceClickSound
                && world != null)) {
                    world.sfx.PlaySoundQuietly("click");
                }
            }
            else
            {
                bp = buttonPress.notPressed;
            }
            oldBp = bp;
            
        }

        public void raiseColor()
        {
            col.A += 2;
        }

        public void whatButton()
        {
            if (bp == buttonPress.notPressed)
            {
                posPos = pos[0];
            }
            else if (bp == buttonPress.hover)
            {
                posPos = pos[1];
            }
            else if (bp == buttonPress.pressed)
            {
                posPos = pos[2];
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            sb.Draw(tex, rec, posPos, col);
            if(hasFont)
                sb.DrawString(font, text, new Vector2(font.MeasureString(text).X + painNum, font.MeasureString(text).Y + rec.Y - 15 + yOffset), col);
        }
    }
}
