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
    public class HellfirePillar
    {
        public Texture2D pillarTexture;
        public Texture2D segmentTexture;
        public List<Rectangle> pillarSources = new List<Rectangle>();
        public List<Rectangle> segmentSources = new List<Rectangle>();
        public int currentIndex = 0;
        public int[] pillarOrder;
        public Rectangle pillarPos;
        public Rectangle[] segmentPos;
        public World world;
        public AttackSuper towerToDelete;
        public Tile tileToReplace;
        public int timer;
        public Color color;
        public Boolean shouldDelete;
        public void Initialize()
        {
            color = new Color(255, 255, 255, 0);
            pillarOrder = new int[] { 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 8, 9, 10, 11, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 1, 1, 1, 1 };
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    pillarSources.Add(new Rectangle(264 * j, 256 * i, 264, 256));
                    segmentSources.Add(new Rectangle(264 * j, 256 * i, 264, 256));
                }
            }
            pillarTexture = world.Content.Load<Texture2D>("Boss/GUI/bossPillar");
            segmentTexture = world.Content.Load<Texture2D>("Boss/GUI/bossSegment");
            int a = pillarPos.Y / 64;
            segmentPos = new Rectangle[a];
            for(int i = 0; i < segmentPos.Length; i++) {
                segmentPos[i] = new Rectangle(pillarPos.X, i * 64, 66, 64);
            }
        }
        public void Update() {
            if(timer % 5 == 0) {
                currentIndex++;
                if(currentIndex >= pillarOrder.Length) {
                    currentIndex -= 1;
                }
            }
            if(timer < 50) {
                color.A += 5;
            }

            if(currentIndex == 11) {
                world.attackTowers.Remove(towerToDelete);
                tileToReplace.towerPlaced = false;
            }
            if(currentIndex > 30 && color.A > 10) {
                color.A -= 10;
            }
            if(color.A <= 10 && currentIndex > 30) {
                shouldDelete = true;
            }
            timer++;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(pillarTexture, pillarPos, pillarSources[pillarOrder[currentIndex]], color);
            for (int i = 0; i < segmentPos.Length; i++)
            {
                spriteBatch.Draw(segmentTexture, segmentPos[i], segmentSources[pillarOrder[currentIndex]], color);
            }
        }
    }
}
