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
    public class Daze
    {
        public Texture2D dazeTex;
        public Vector2 origin;
        public Rectangle source;
        public Rectangle position;
        public int rotation;
        public World world;

        public void Initialize() {
            dazeTex = world.Content.Load<Texture2D>("Boss/GUI/daze");
            origin = new Vector2(dazeTex.Width / 2, dazeTex.Height / 2);
            source = new Rectangle(0, 0, dazeTex.Width, dazeTex.Height);
        }
        public void Update() {
            rotation -= 2;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(dazeTex, position, source, Color.White, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            
        }
    }
}
