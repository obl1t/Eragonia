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
    public class ScreenTransistioner
    {
        public Texture2D texture;
        public Color color = new Color(0, 0, 0, 0);
        public Rectangle position = new Rectangle(0, 0, 1280, 960);
        public int timer = 0;
        public Boolean turnBlack = false;
        public Boolean turnWhite = false;
        public Boolean flashWhite = false;
        public Boolean flashBlack = false;
        public World world;
       
        public World.gameState state;
        public void Update()
        {
            if (turnBlack)
            {
                if (color.A <= 250)
                {
                    color.A += 5;
                }
            }
            else if(turnWhite){

                if (color.A >= 5)
                {
                    color.A -= 5;
                }
            }

            if(flashWhite) {
                if(timer < 30) {
                    timer++;
                    color.A = 0;
                    return;
                }
                color.R = 255;
                color.G = 255;
                color.B = 255;
                if(color.A < 240) {
                   
                    color.A += 20;
                }
             
               
            }
            else if(flashBlack) {
                
                if(color.A > 0) {
                    color.A -= 20;
                }
                if(color.A == 0) {
                    color = new Color(0, 0, 0, 0);
                }
            }
            
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, color);
        }
        
    }
}
