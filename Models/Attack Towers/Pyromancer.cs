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

    public class Pyromancer : AttackSuper
    {
        public Shockwave wave;
        public Texture2D shockTex;
        public Pyromancer() : base()
        {
            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 4, 5, 6, 7, 8 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 3;
            speed = 75;
            rotation = 0;
            damage = 25;
            color = Color.White;
            currentTier = 1;
            upgrade1Damage = 31;
            upgrade2Damage = 37;
            infoTexts[0] = formatter.formatString(200, "60 Iron. Increase damage by 6");
            infoTexts[1] = formatter.formatString(200, "70 Iron. Increase damage by 6");
            infoTexts[3] = formatter.formatString(200, "100 Gold, 75 Steel. \nTier up to 'Smelter'");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
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
            wave.color = Color.Orange;
            wave.doesBurn = true;
            wave.damageOverTime = 2;
            wave.maxBurn = 210;
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
            if (world.activeEnemies.Count == 0)
            {
                setIdle();
            }
            if (wave != null)
            {
                wave.Update();
            }

            if (attack == AttackState.Attacking)
            {
                for (int j = 0; j < world.activeEnemies.Count; j++)
                {
                    if (wave != null && wave.position.Intersects(world.activeEnemies[j].hitbox) && wave.maxNumHits >= 0)
                    {
                        if (world.activeEnemies[j].wave != null && world.activeEnemies[j].wave != this.wave)
                        {

                            world.activeEnemies[j].incurDamage(trueDamage, 3);
                            setBurning(world.activeEnemies[j]);
                            world.activeEnemies[j].wave = wave;
                        }
                        if (world.activeEnemies[j].wave == null)
                        {

                            world.activeEnemies[j].incurDamage(trueDamage, 3);
                            setBurning(world.activeEnemies[j]);
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
                if (world.bar.resources[2] >= 60)
                {
                    world.bar.resources[2] -= 60;
                    damage = upgrade1Damage;
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[2] >= 70)
                {
                    world.bar.resources[2] -= 70;
                    damage = upgrade2Damage;
                    upgrade.showInfoBox = false;
                    upgradeIndex++;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }

        public override void checkTier()
        {
            if (world.bar.resources[3] >= 75 && world.bar.resources[0] >= 100)
            {
                world.sfx.PlaySoundQuietly("upgrade");
                world.bar.resources[0] -= 100;
                world.bar.resources[3] -= 75;
                if (world.shouldRefundUpgrade)
                {
                    if (upgradeIndex == 1)
                    {
                        world.bar.resources[2] += 30;

                    }
                    if (upgradeIndex == 2)
                    {
                        world.bar.resources[2] += 65;
                    }
                }
                world.shownInfoBox = null;
                world.shownUpgrade = null;
                world.shownTier = null;
                world.loader.replaceSmelter(this);
            }
        }
        public void setBurning(EnemySuper e)
        {
            e.maxBurn = 200;
            if (e.damageOverTime < 2)
            {
                e.damageOverTime = 2;
            }
            e.isBurning = true;
            e.burningTimer = 0;
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
