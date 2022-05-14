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
    public class Knight : AttackSuper
    {
        public Shockwave wave;
        public Texture2D shockTex;
        public Knight() : base()
        {
            addAnimationOrder(new int[] { 1, 1, 1, 1, 2, 3, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 1;
            speed = 90;
            rotation = 0;
            color = Color.White;
            damage = 40;
            upgrade1Damage = 49;
            upgrade2Damage = 60;
            currentTier = 1;
            infoTexts[0] = formatter.formatString(200, "50 Iron. Increase damage by 9");
            infoTexts[1] = formatter.formatString(200, "65 Iron. Increase damage by 11");
            infoTexts[3] = formatter.formatString(200, "80 Gold, 70 Steel. \nTier up to 'Samurai'");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }

        }
        public override void setAttack()
        {
            base.setAttack();
            timer = 80;
        }
        public override void doAttack()
        {
            wave = new Shockwave();
            wave.position = new Rectangle(position.X + (int)(position.Width / 2 * Math.Cos(MathHelper.ToRadians(rotation + 90))), position.Y +
                            (int)(position.Height / 2 * Math.Sin(MathHelper.ToRadians(rotation + 90))), 17, 42);
            wave.texture = shockTex;
            wave.rotation = this.rotation + 90;
            wave.color = Color.White;
            wave.maxNumHits = 3;
            world.sfx.PlaySound("swoosh");
            wave.setVelocity();
        }
        public override void Update()
        {
            base.Update();



            for (int j = 0; j < world.activeEnemies.Count; j++)
            {
                if (isInRange(world.activeEnemies[j]))
                {
                    if (enemyFocusingOn == null)
                    {

                        enemyFocusingOn = world.activeEnemies[j];
                        break;
                    }
                }
            }
            if (enemyFocusingOn != null)
            {
                if (attack != AttackState.Attacking)
                {
                    setAttack();
                }

                face(enemyFocusingOn.hitbox);

            }
            else
            {
                setIdle();
            }
            if (wave != null)
            {
                wave.Update();
            }
            if (world.activeEnemies.Count == 0)
            {
                setIdle();
            }
            if (attack == AttackState.Attacking)
            {
                for (int j = 0; j < world.activeEnemies.Count; j++)
                {
                    if (wave != null && wave.position.Intersects(world.activeEnemies[j].hitbox) && wave.maxNumHits >= 0)
                    {
                        if (world.activeEnemies[j].wave != null && world.activeEnemies[j].wave != this.wave)
                        {

                            world.activeEnemies[j].incurDamage(trueDamage, 1);
                            world.activeEnemies[j].wave = wave;
                        }
                        if (world.activeEnemies[j].wave == null)
                        {

                            world.activeEnemies[j].incurDamage(trueDamage, 1);
                            world.activeEnemies[j].wave = wave;
                        }
                        if (world.activeEnemies[j].health <= 0)
                        {

                            enemyFocusingOn = null;
                        }
                        wave.maxNumHits--;

                    }
                }
            }
            if (wave != null && wave.shouldRemove)
            {
                wave = null;
            }
            if (enemyFocusingOn != null && !isInRange(enemyFocusingOn))
            {
                enemyFocusingOn = null;

            }
        }
        public override void checkUpgrade()
        {
            if (upgradeIndex == 0)
            {
                if (world.bar.resources[2] >= 50)
                {
                    world.bar.resources[2] -= 50;
                    damage = upgrade1Damage;
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[2] >= 65)
                {
                    world.bar.resources[2] -= 65;
                    damage = upgrade2Damage;
                    upgrade.showInfoBox = false;
                    upgradeIndex++;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }

        public override void checkTier()
        {
            if (world.bar.resources[3] >= 70 && world.bar.resources[0] >= 80)
            {
                world.sfx.PlaySoundQuietly("upgrade");
                world.bar.resources[0] -= 80;
                world.bar.resources[3] -= 70;
                if (world.shouldRefundUpgrade)
                {
                    if (upgradeIndex == 1)
                    {
                        world.bar.resources[2] += 25;

                    }
                    if (upgradeIndex == 2)
                    {
                        world.bar.resources[2] += 62;
                    }
                }
                world.shownInfoBox = null;
                world.shownUpgrade = null;
                world.shownTier = null;
                world.loader.replaceSamurai(this);
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);

            if (wave != null)
            {
                wave.Draw(gameTime, spriteBatch);
            }
        }
    }
}
