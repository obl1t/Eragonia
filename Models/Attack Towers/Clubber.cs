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
    public class Clubber : AttackSuper
    {
        public Shockwave wave;
        public Texture2D shockTex;
        public Clubber() : base()
        {
            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 2, 4, 3, 3, 3, 3, 3, 3, 3, 3, 2, 2 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 10;
            speed = 90;
            rotation = 0;
            color = Color.White;
            damage = 20;
            upgrade1Damage = 25;
            upgrade2Damage = 30;
            infoTexts[0] = formatter.formatString(200, "40 Stone. Increase damage by 5");
            infoTexts[1] = formatter.formatString(200, "50 Stone. Increase damage by 5");
            infoTexts[2] = formatter.formatString(200, "50 Gold, 60 Iron. \nTier up to 'Knight'");
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }

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
            wave.setVelocity();
            world.sfx.PlaySound("swoosh");
        }

        public override void checkTier()
        {
            if (world.bar.resources[2] >= 60 && world.bar.resources[0] >= 50)
            {
                
                world.bar.resources[0] -= 50;
                world.bar.resources[2] -= 60;
                world.sfx.PlaySoundQuietly("upgrade");
                if (world.shouldRefundUpgrade)
                {
                    if (upgradeIndex == 1)
                    {
                        world.bar.resources[1] += 20;

                    }
                    if (upgradeIndex == 2)
                    {
                        world.bar.resources[1] += 45;
                    }
                }
                world.shownInfoBox = null;
                world.shownUpgrade = null;
                world.shownTier = null;
                world.loader.replaceKnight(this);
            }
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
                if (world.bar.resources[1] >= 40)
                {
                    world.bar.resources[1] -= 40;
                    damage = upgrade1Damage;
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[1] >= 50)
                {
                    world.bar.resources[1] -= 50;
                    damage = upgrade2Damage;
                    upgrade.showInfoBox = false;
                    upgradeIndex++;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
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
