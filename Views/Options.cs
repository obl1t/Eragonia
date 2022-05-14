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
    public class Options
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;
        public MouseState mouse;
        public World world;
        public SpriteFont font;
        public Button[] optButtons;


        public Options(Texture2D t, Rectangle r, Rectangle p, Color c, Button[] b)
        {
            tex = t;
            rec = r;
            pos = p;
            col = c;
            
            optButtons = b;
        }

        public void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                optButtons[i].isOverChoice(mouse.X, mouse.Y, mouse, world.oldM);
                optButtons[i].whatButton();
                if (optButtons[0].bp == Button.buttonPress.pressed && world.configObj.configButtons[3].bp != Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.Config;
                    
                }
                if (optButtons[1].bp == Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.TitleScreen;
                   
                }
            }
            
        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            //sb.Draw(tex, rec, pos, col);
            for (int i = 0; i < 2; i++)
            {
                optButtons[i].Draw(gameTime, sb);
            }
            sb.DrawString(font, "Volume: ", new Vector2(100, 222), Color.White);
            sb.DrawString(font, "   SFX: ", new Vector2(100, 422), Color.White);
        }
    }
}
