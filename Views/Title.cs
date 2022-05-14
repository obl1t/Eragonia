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
    public class Title
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;
        public MouseState mouse;
        public World world;
        public Boolean showButtons;
        public Button[] titleButtons;
        public int timer = 0;


        public Title(Texture2D t, Rectangle r, Rectangle p, Color c, Button[] b)
        {
            tex = t;
            rec = r;
            pos = p;
            col = c;
            col.A = 0;
            titleButtons = b;
        }

        public void DoStart()
        {
            timer++;
            if (timer < 50)
            {
                return;
            }

            if (col.A + 3 >= 255)
            {
                col.A = 255;

            }
            else
            {
                col.A += 3;
            }
            if (col.A == 255 && timer > 180)
            {
                world.gS = World.gameState.TitleScreen;
            }

        }

        public void Update()
        {
            for (int i = 0; i < 4; i++)
            {
                titleButtons[i].isOverChoice(mouse.X, mouse.Y, mouse, world.oldM);
                titleButtons[i].whatButton();
                if (titleButtons[0].bp == Button.buttonPress.pressed)
                {
                    world.baseLoad(@"Content/saveData.txt");
                    world.transistioner.state = World.gameState.Artifacts;
                    world.transistioner.turnBlack = true;
                    world.transistion = World.transistionState.Transistioning;
                }
                if (titleButtons[1].bp == Button.buttonPress.pressed)
                {
                    world.loadInGame(@"Content/saveData.txt");
                    world.transistioner.state = World.gameState.Artifacts;
                    world.transistioner.turnBlack = true;
                    world.isFirstTimePlaying = false;
                    world.transistion = World.transistionState.Transistioning;
                    titleButtons[1].bp = Button.buttonPress.notPressed;
                }
                if (titleButtons[2].bp == Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.Options;
                }
                if (titleButtons[3].bp == Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.QuitGame;
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        { 
            // TODO: Add your drawing code here
            sb.Draw(tex, rec, pos, col);
            if (world.gS == World.gameState.TitleScreen)
            {
                for (int i = 0; i < 4; i++)
                {
                    titleButtons[i].Draw(gameTime, sb);
                }
            }
        }
    }
}
