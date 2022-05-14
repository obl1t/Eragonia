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
    public class LifeHeart
    {
        public Texture2D texture;
        public World world;
        public Rectangle[] sources = new Rectangle[3];
        public int currentIndex = 0;
        public Rectangle position;
        public Boolean showHeart = false;
        public LifeHeart() {
            
        }
        public void Initialize() {
            texture = world.Content.Load<Texture2D>("GUI/lifeHeart");
            position = new Rectangle(1020, 0, 64, 64);
            sources[0] = new Rectangle(0, 0, 512, 512);
            sources[1] = new Rectangle(512, 0, 512, 512);
            sources[2] = new Rectangle(0, 512, 512, 512);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if(showHeart)
                spriteBatch.Draw(texture, position, sources[currentIndex], Color.White);
        }
    }
}
