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
    public class Healthbar
    {
        public Texture2D healthTex;
        public Texture2D dummyTex;
        public Rectangle barPos;
        public Rectangle healthPos;
        public World world;
        public SpriteFont font;
        public Vector2 textPos;
        public Boolean isFillingUp = false;
        public Color color = new Color(200, 0, 0);

        public void Initialize() {
            healthTex = world.Content.Load<Texture2D>("Boss/GUI/healthBar");
            dummyTex = world.dummyTexture;
            healthPos = new Rectangle(187, 50, 826, 40);
            barPos = new Rectangle(215, 62, 0, 16);
            textPos = new Vector2(435, 20);
            font = world.Content.Load<SpriteFont>("Other/Font1");
        }
        public void Update() {
            if(isFillingUp) {
                barPos.Width += 5;
                if(barPos.Width + 5 >= 796) {
                    barPos.Width = 796;
                    isFillingUp = false;
                }
            }
           
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(dummyTex, barPos, color);
            spriteBatch.Draw(healthTex, healthPos, Color.White);
                                          
            spriteBatch.DrawString(font, "The First Dark Lord", textPos, Color.White);
        }
    }
}
