using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace Eragonia_Demo_Day_One
{
    public class PauseButton
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle[] pos = new Rectangle[3];
        public Rectangle posPos;
        public Color col;
        public enum buttonPress { notPressed, hover, pressed }
        public buttonPress bp = buttonPress.notPressed;
        public int offset;

        public PauseButton(Texture2D t, Rectangle r, Color c)
        {
            tex = t;
            rec = r;
            pos[0] = new Rectangle(208, 160, 799, 831);
            pos[1] = new Rectangle(1232, 160, 799, 831);
            pos[2] = new Rectangle(208, 1184, 799, 831);
            posPos = pos[0];
            col = c;
            bp = buttonPress.notPressed;
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
            }
            else
            {
                bp = buttonPress.notPressed;
            }
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
            sb.Draw(tex, rec, pos[(int)bp], col);
        }
    }
}
