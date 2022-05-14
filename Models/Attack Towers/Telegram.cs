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
    public class Telegram : AttackSuper
    {
        public Telegram() { 
            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2});
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 0;
            speed = 10;
            rotation = 0;
            damage = 25;
            color = Color.White;
            upgrade1Damage = 35;
            upgrade2Damage = 45;
            upgrade1Range = 150;
            upgrade2Range = 170;
            currentTier = 2;
            doesTurn = false;
            infoTexts[0] = formatter.formatString(200, "80 Steel. Increase damage boost by +10%");
            infoTexts[1] = formatter.formatString(200, "90 Steel. Increase damage boost by +10%");
            infoTexts[4] = formatter.formatString(200, "NANIIIIIII?!");
            setAttack();
            
            for (int i = 0; i< 2; i++)
            {
                for (int j = 0; j< 1; j++)
                {
                    sources.Add(new Rectangle(j* 512, i* 560, 512, 560));
                }
            }
        }

        public override void doAttack()
        {
            double d = 100 + damage;
            d /= 100.0;
            for(int i = 0; i <  world.attackTowers.Count; i++) {
                if(this.isInRange(world.attackTowers[i].position)){
                    world.attackTowers[i].additionalDamageMult = d;
                }
            }
        }

        public override void addRange(Texture2D rangeTex, int radius)
        {
            range = new Range();
            range.center = new Vector2(position.X, position.Y+6);
            range.radius = radius;
            range.texture = rangeTex;
            range.addPosition();
        }
        public override void isBeingClicked(MouseState mouse)
        {
            base.isBeingClicked(mouse);
            for (int i = 0; i < world.attackTowers.Count; i++)
            {
                if (this.isInRange(world.attackTowers[i].position) && showRange && world.attackTowers[i].upgrade2Range != 250)
                {
                    world.attackTowers[i].color = Color.LightGreen;
                }
                else {
                    world.attackTowers[i].color = Color.White;
                }
            }
        }

        public override void checkUpgrade()
        {
            if (upgradeIndex == 0)
            {
                if (world.bar.resources[2] >= 80)
                {
                    world.bar.resources[2] -= 80;
                    damage = upgrade1Damage;
                    range.radius = upgrade1Range;
                    range.addPosition();
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[2] >= 90)
                {
                    world.bar.resources[2] -= 90;
                    damage = upgrade2Damage;
                    
                    range.radius = upgrade2Range;
                    range.addPosition();
                    upgradeIndex++;
                    upgrade.showInfoBox = false;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (showRange)
            {
                range.Draw(gameTime, spriteBatch);
                upgrade.Draw(gameTime, spriteBatch);
                tierUp.Draw(gameTime, spriteBatch);
            }
            spriteBatch.Draw(spriteSheet, position, sources[animationOrder[currentIndex]], color, MathHelper.ToRadians(rotation), origin, SpriteEffects.None, 0.0f);
            icon.Draw(gameTime, spriteBatch);
        }
    }
}
