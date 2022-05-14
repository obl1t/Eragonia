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
    class Quit
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;
        public MouseState mouse;
        public World world;

        public Button[] quitButtons;


        public Quit(Texture2D t, Rectangle r, Rectangle p, Color c, Button[] b)
        {
            tex = t;
            rec = r;
            pos = p;
            col = c;
            quitButtons = b;
        }

        public void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                quitButtons[i].isOverChoice(mouse.X, mouse.Y, mouse, world.oldM);
                quitButtons[i].whatButton();

                if (quitButtons[0].bp == Button.buttonPress.pressed)
                {
                    world.save(@"Content/saveData.txt");
                    world.Exit();
                }
                if (quitButtons[1].bp == Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.TitleScreen;
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            sb.Draw(tex, rec, pos, col);
            for (int i = 0; i < 2; i++)
            {
                quitButtons[i].Draw(gameTime, sb);
            }
        }
    }
}
