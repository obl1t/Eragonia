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

    public class Smelter : AttackSuper
    {
        public Shockwave wave;
        public Texture2D shockTex;
        public Smelter() : base()
        {
            addAnimationOrder(new int[] { 1, 2, 2, 2, 2, 2, 2, 2, 3, 4, 5, 6, 7, 8, 9 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 3;
            speed = 75;
            rotation = 0;
            damage = 50;
            color = Color.White;
            currentTier = 2;
            upgrade1Damage = 31;
            upgrade2Damage = 37;
            infoTexts[0] = formatter.formatString(200, "60 Steel. Increase damage by 9");
            infoTexts[1] = formatter.formatString(200, "70 Steel. Increase damage by 9");
            infoTexts[2] = formatter.formatString(200, "Nani");
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
            wave.setVelocity();
            world.sfx.PlaySound("swoosh");
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
                if (world.bar.resources[3] >= 60)
                {
                    world.bar.resources[3] -= 60;
                    damage = upgrade1Damage;
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[3] >= 70)
                {
                    world.bar.resources[3] -= 70;
                    damage = upgrade2Damage;
                    upgrade.showInfoBox = false;
                    upgradeIndex++;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }


        public void setBurning(EnemySuper e)
        {
            e.maxBurn = 280;
            if (e.damageOverTime < 3)
            {
                e.damageOverTime = 3;
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
