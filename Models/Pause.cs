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
    public class Pause
    {
        public Texture2D tex;
        public Rectangle rec;
        public Rectangle pos;
        public Color col;
        public MouseState mouse;
        public World world;
        public Boolean showButtons;
        public Button[] pauseButtons;
        public int timer = 0;
        public int offset;


        public Pause(Texture2D t, Rectangle r, Rectangle p, Color c, Button[] b)
        {
            tex = t;
            rec = r;
            pos = p;
            col = c;
            col.A = 0;
            pauseButtons = b;
        }


        public void setPosition(int offsetX)
        {
            offset += offsetX;
            for (int i = 0; i < pauseButtons.Count(); i++)
            {
                pauseButtons[i].offset += offsetX;


                pauseButtons[i].rec.X += offsetX;
                pauseButtons[i].painNum += offsetX;
            }
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
            if (col.A == 255 && timer > 170)
            {
                world.gS = World.gameState.TitleScreen;
            }

        }

        public void Update()
        {
            for (int i = 0; i < 2; i++)
            {
                pauseButtons[i].isOverChoice(mouse.X, mouse.Y, mouse, world.oldM);
                pauseButtons[i].whatButton();
                if (pauseButtons[0].bp == Button.buttonPress.pressed)
                {
                    world.gS = World.gameState.PlayGame;
                }
                if (pauseButtons[1].bp == Button.buttonPress.pressed)
                {
                    world.Exit();
                }
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            // TODO: Add your drawing code here
            sb.Draw(tex, rec, pos, col);
            if (world.gS == World.gameState.Pause)
            {
                for (int i = 0; i < 2; i++)
                {
                    pauseButtons[i].Draw(gameTime, sb);
                }
            }
        }
    }
}
