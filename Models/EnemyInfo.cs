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
    public class EnemyInfo
    {
        public Texture2D texture;
        public Texture2D enemyTex;
        public Rectangle enemyPos;
        public Rectangle enemySource;
        public Rectangle position;
        public SpriteFont bigFont;
        public SpriteFont smallFont;
        public World world;
        public String title;
        public Vector2 titlePos;
        public String stats;
        public String desc;
        public int offsetX;

        public Button button;
        public FormatterMaster formatter = new FormatterMaster();
        public MouseState oldMouse = Mouse.GetState();
        public void Initialize() {
            position = new Rectangle(440, 300, 400, 262);
           
            enemyPos = new Rectangle(position.X + 16, position.Y + 16, 128, 128);
            texture = world.Content.Load<Texture2D>("GUI/enemyInfo");
            bigFont = world.Content.Load<SpriteFont>("Other/Font1");
            titlePos = new Vector2(708 - (bigFont.MeasureString(title).X / 2), 335);
            smallFont = world.Content.Load<SpriteFont>("Other/Font2");
           
            button = new Button(world.Content.Load<Texture2D>("TitleScreen/Buttons/ButtonsTry"), new Rectangle(555, 550, 160, 50), 
            world.buttonSprites, Color.White, bigFont, true);
            button.text = "Close";
            button.painNum = 495;
            button.yOffset = 10;
            
        }
        public void setPosition(int offset) {
            button.rec.X += offset;
            button.painNum += offset;
            position.X += offset;
            enemyPos.X += offset;
            titlePos.X += offset;
            offsetX += offset;
        }
        public void isClicking(MouseState mouse) {
            //Console.WriteLine(position.X);
            button.isOverChoice(mouse.X + offsetX, mouse.Y, mouse, oldMouse);
            button.whatButton();
            oldMouse = mouse;
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.DrawString(bigFont, title, titlePos, Color.LightGray);
            spriteBatch.DrawString(smallFont, stats, new Vector2(620 + offsetX, 390), Color.LightGray);
            spriteBatch.DrawString(smallFont, desc, new Vector2(470 + offsetX, 460), Color.LightGray);
            spriteBatch.Draw(enemyTex, new Rectangle(enemyPos.X + 3, enemyPos.Y + 3, enemyPos.Width, enemyPos.Height), enemySource, new Color(0, 0, 0, 120));
            spriteBatch.Draw(enemyTex, enemyPos, enemySource, Color.White);
            button.Draw(gameTime, spriteBatch);
        }
    }
}
