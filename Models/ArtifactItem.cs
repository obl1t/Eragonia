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
    public class ArtifactItem
    {
        public Texture2D texture;
        public Rectangle position;
        public Rectangle[] sources = new Rectangle[3];
        public enum ViewType { Default, Hovered, Pressed }
        public ViewType type;
        public Rectangle barPosition;
        public Texture2D barTexture;
        public Color barColor = new Color(0, 220, 220);
        public SpriteFont font;
        public Vector2 textPosition;
        public String[] texts = new String[4];
        public int[] prestigeCosts = new int[3];
        public World world;
        public int artifactValue = 0;
        public int artifactID;
        public MouseState oldMouse;
        public Rectangle shadowPos;
        public Color shadowColor = new Color(0, 0, 0, 120);
        StringFormatter formatter = new StringFormatter();

        public void isClicking(MouseState mouse)
        {
            if (mouse.X >= position.Left && mouse.X <= position.Right && mouse.Y <= position.Bottom && mouse.Y >= position.Top && type != ViewType.Pressed)
            {
                type = ViewType.Hovered;
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    incrementArtifact();
                }
            }
            else if (type != ViewType.Pressed)
            {
                type = ViewType.Default;
            }
            oldMouse = mouse;
        }
        public void incrementArtifact()
        {

            if (artifactValue < 3)
            {
                if (world.prestige >= prestigeCosts[artifactValue])
                {
                    world.spendPrestige(prestigeCosts[artifactValue]);
                    artifactValue++;
                    world.applyArtifact(artifactID, artifactValue);

                    barPosition.Width += 56;
                }

            }
            if (artifactValue == 3)
            {
                type = ViewType.Pressed;
            }
        }
        public void setSources()
        {
            sources[0] = new Rectangle(0, 0, 732, 534);
            sources[1] = new Rectangle(0, 534, 732, 534);
            sources[2] = new Rectangle(0, 1068, 732, 534);
            textPosition = new Vector2(position.X + 20, position.Y + 55);
            shadowPos = new Rectangle(position.X, position.Y + 5, position.Width, position.Height);
            barPosition = new Rectangle(position.X + 38, position.Y + 12, 0, 24);
        }
        public void setTexts(String[] text)
        {
            for (int i = 0; i < 3; i++)
            {
                texts[i] = formatter.formatString(200, text[i]);
            }
            texts[3] = formatter.formatString(224, "MAXED OUT");

        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(barTexture, barPosition, barColor);
            spriteBatch.Draw(texture, shadowPos, sources[(int)type], shadowColor);
            spriteBatch.Draw(texture, position, sources[(int)type], Color.White);

            spriteBatch.DrawString(font, texts[artifactValue], textPosition, Color.White);

        }
    }
}
