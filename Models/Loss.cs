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
using System.IO;


namespace Eragonia_Demo_Day_One
{
    public class Loss 
    {
        // |  ||
        // || |_

        public String eHF = "Eragonia has Fallen";
        public SpriteFont font;
        public MouseState m;
        public MouseState oldM;
        public Color color = Color.White;
        public World world;
        

        public Button[] lossButtons;

        public Loss(MouseState oldM, SpriteFont fo, Button[] b)
        {
            lossButtons = b;
            font = fo;
            this.oldM = oldM;
            color.A = 0;
            for (int i = 0; i < lossButtons.Count(); i++)
            {
                lossButtons[i].col.A = 0;
            }
        }

        public void Update()
        {
            for (int i = 0; i < lossButtons.Count(); i++)
            {
                lossButtons[i].isOverChoice(m.X, m.Y, m, oldM);
                lossButtons[i].whatButton();
                
            }

            if (lossButtons[0].bp == Button.buttonPress.pressed)
            {
                world.retry();
            }
            if (lossButtons[1].bp == Button.buttonPress.pressed)
            {
                world.save(@"Content/saveData.txt");
                world.Exit();
            }
            if (lossButtons[2].bp == Button.buttonPress.pressed)
            {
                world.gS = World.gameState.Stats;
            }

        }

        public void raiseColor()
        {
            color.A += 2;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.DrawString(font, eHF, new Vector2(250, 100), color);
            for (int i = 0; i < lossButtons.Count(); i++)
            {
                lossButtons[i].Draw(gameTime, sb);
            }
        }

    }
}
