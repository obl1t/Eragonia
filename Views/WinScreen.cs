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
    public class WinScreen
    {
       
        public String eHF = "Victory";
        public SpriteFont font;
        public SpriteFont font2;
        public MouseState m;
        public MouseState oldM;
        public Color color = Color.White;
        public World world;
        public Vector2 winPos;

        public Button[] winButtons;

        public WinScreen(MouseState oldM, SpriteFont fo, Button[] b)
        {
            winButtons = b;
            font = fo;
            this.oldM = oldM;
            color.A = 255;
            for (int i = 0; i < winButtons.Count(); i++)
            {
                winButtons[i].col.A = 255;
            }
            
        }
        public void Initialize() {
            font = world.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont");
            font2 = world.Content.Load<SpriteFont>("Other/Font1");
            winPos = new Vector2(640 - font.MeasureString("Victory").X / 2, 100);
        }
        public void Update()
        {
            
            for (int i = 0; i < winButtons.Count(); i++)
            {
                
                winButtons[i].isOverChoice(m.X, m.Y, m, oldM);
                winButtons[i].whatButton();

            }

            if (winButtons[0].bp == Button.buttonPress.pressed)
            {
                world.retry();
            }
            if (winButtons[1].bp == Button.buttonPress.pressed)
            {
                world.save(@"Content/saveData.txt");
                world.Exit();
            }
            if (winButtons[2].bp == Button.buttonPress.pressed)
            {
                world.gS = World.gameState.Stats;
                world.stats.SetText();
            }

        }

        public void raiseColor()
        {
            color.A += 2;
        }

        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //sb.DrawString(font, eHF, winPos, color);
            sb.DrawString(font, "Victory", winPos, Color.Black);
            for (int i = 0; i < winButtons.Count(); i++)
            {
                winButtons[i].Draw(gameTime, sb);
            }
        }

    }
}
