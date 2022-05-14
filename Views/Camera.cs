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
    public class Camera
    {
        public Matrix transformation;
        public Vector2 center;
        public Viewport viewport;
        public float zoom = 1f;
        public float rotation = 0f;
        public int lolRot;
        public World world;
        

        public void Update(Vector2 position) {
          
            center.X = position.X;
            center.Y = position.Y;
            //lolRot++;
            transformation = Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0)) * Matrix.CreateRotationZ(rotation) * 
            Matrix.CreateRotationX(MathHelper.ToRadians(lolRot)) * Matrix.CreateRotationY(MathHelper.ToRadians(lolRot)) *
            Matrix.CreateScale(new Vector3(zoom, zoom, 0)) * Matrix.CreateTranslation(new Vector3(viewport.Width / 2, viewport.Height / 2, 0));
        }


    }
}
