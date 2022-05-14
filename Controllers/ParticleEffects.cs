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
    public class ParticleEffects
    {
        public World world;
        public Texture2D particleTex;
        public List<Particle> particles = new List<Particle>();
        public Boolean isSpawningParticles = false;
        public int timer;
        public Random rand = new Random();
        public int offset;
        public void Initialize() {
            particleTex = world.Content.Load<Texture2D>("Boss/GUI/fireball");

        }
        public void spawnParticle() {
            Particle p = new Particle();
            p.texture = particleTex;
            int temp1 = rand.Next(0, 1281);
            int temp2 = rand.Next(0, 961);
            int temp3 = rand.Next(15, 40);
            int temp4 = rand.Next(0, 2);
            if(temp4 == 0) {
                p.position = new Rectangle(temp1 + offset, -10, temp3, temp3);
            }
            else {
                p.position = new Rectangle(-10, temp2, temp3, temp3);
            }
           
            temp1 = rand.Next(3, 7);
            p.velocity = new Vector2(temp1, temp1);
            particles.Add(p);
        }
        public void Update() {
            for(int i = particles.Count - 1; i >= 0; i--) {
                particles[i].Update();
                if(particles[i].position.X > 2560 || particles[i].position.Y > 960) {
                    particles.RemoveAt(i);
                }
            }
            if(isSpawningParticles) {
                if(timer <= 0) {
                    timer = rand.Next(15, 30);
                    spawnParticle();
                }
                timer--;
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            for(int i = 0; i < particles.Count; i++) {
                particles[i].Draw(gameTime, spriteBatch);
            }
        }

    }
}
