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
    public class StatsManager
    {
        public String favoriteTowerType;
        public int totalDamage;
        public int enemiesKilled;
        public int goldEarned;
        public int stoneEarned;
        public int ironEarned;
        public int steelEarned;
        public int prestige;
        public int favoriteTowerDamage;
        public int highestWave;
        public String text = "";
        public World world;
        public Vector2 titlePos;
        public SpriteFont bigFont;
        public SpriteFont smallFont;
        public Button backButton;
        public Boolean isLoss = true;
        public MouseState oldMouse = Mouse.GetState();
        public void Initialize()
        {
            bigFont = world.Content.Load<SpriteFont>("TitleScreen/Buttons/Font/ButtonFont");
            smallFont = world.Content.Load<SpriteFont>("Other/Font1");
            titlePos = new Vector2(640 - bigFont.MeasureString("STATS").X / 2, 50);
        }
        public void Update(MouseState mouse)
        {
            backButton.isOverChoice(mouse.X, mouse.Y, mouse, oldMouse);
            backButton.whatButton();
            if (backButton.bp == Button.buttonPress.pressed)
            {
                if (isLoss)
                {
                    world.gS = World.gameState.LostGame;
                }
                else {
                    world.gS = World.gameState.WinGame;
                }
            }
        }
        public void SetText()
        {
            highestWave = world.highestWave - 1;
            totalDamage = world.totalDamage;
            int a = getMaxTowerType();
            if(a == 0) {
                favoriteTowerType = "Swinger-type";
            }
            if (a == 1)
            {
                favoriteTowerType = "Marksman-type";
            }
            if (a == 2) {
                favoriteTowerType = "Knifer-type";
            }
            if(a == 3) {
                favoriteTowerType = "Fire-type";
            }
            if(a == 4) {
                favoriteTowerType = "Bombardier-type";
            }
            goldEarned = world.resourcesGained[0];
            stoneEarned = world.resourcesGained[1];
            ironEarned = world.resourcesGained[2];
            steelEarned = world.resourcesGained[3];
            prestige = (int)world.gamePrestige;
            text = "Wave Reached: " + highestWave + "\n\nTotal Damage: " + totalDamage + "\n\nFavorite Tower: " + favoriteTowerType + " (" + favoriteTowerDamage + ")\n\n" +  
            "  Gold Earned: " + goldEarned + "\n\n  Stone Mined: " + stoneEarned + "\n\n  Iron Mined: " + ironEarned + "\n\n  Steel Mined: "
            + steelEarned + "\n\n\n\n\nPrestige Earned: " + prestige;
        }
        public int getMaxTowerType() {
            int a = -1;
            int index = -1;
            for(int i = 0; i < 5; i++) {
                if(world.towerDamages[i] > a) {
                    index = i;
                    favoriteTowerDamage = world.towerDamages[i];
                    a = world.towerDamages[i];
                }
            }
            return index;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (isLoss)
            {
                spriteBatch.DrawString(bigFont, "STATS", titlePos, Color.White);
                spriteBatch.DrawString(smallFont, text, new Vector2(100, 200), Color.LightGray);
            }
            else
            {
                spriteBatch.DrawString(bigFont, "STATS", titlePos, Color.Black);
                spriteBatch.DrawString(smallFont, text, new Vector2(100, 200), Color.Navy);
            }
           
            backButton.Draw(gameTime, spriteBatch);
        }
    }
}
