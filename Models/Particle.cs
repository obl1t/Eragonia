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
    public class Particle
    {
        public Texture2D texture;
        public Vector2 velocity;
        public Rectangle position;
        public Color color = Color.White;

        public void Update() {
            position.X += (int)velocity.X;
            position.Y += (int)velocity.Y;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, color);
        }
    }
}
