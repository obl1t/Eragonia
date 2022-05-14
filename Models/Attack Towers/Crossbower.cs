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
    class Crossbower : AttackSuper
    {
        public List<Projectile> projectiles = new List<Projectile>();
        public Texture2D projectileTex;
        public Crossbower()
        {

            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 3, 4, 4, 4, 4, 4, 4, 4, 5, 6, 6, 6, 6 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 6;
            speed = 120;
            rotation = 0;
            damage = 45;
            color = Color.White;
            currentTier = 1;
            infoTexts[0] = formatter.formatString(160, "50 Iron. Increase range by 80");
            infoTexts[1] = formatter.formatString(160, "65 Iron. Increase damage by 10");
            infoTexts[3] = formatter.formatString(200, "100 Gold, 50 Steel. \nTier up to 'Musketeer'");
            upgrade1Range = 500;
            upgrade2Damage = 55;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    sources.Add(new Rectangle(j * 512, i * 560, 512, 560));
                }
            }
        }

        public override void doAttack()
        {
            Projectile p = new Projectile();
            p.velocity = 25;
            p.rotation = this.rotation + 90;
            p.texture = projectileTex;
            p.decay = 200;
            p.offset = world.offsetX - 640;
            p.position = new Rectangle(this.position.X, this.position.Y, 6, 24);
            p.setOrigin();
            projectiles.Add(p);
            world.sfx.PlaySound("arrow", 0.15f);
        }

        public override void checkUpgrade()
        {
            if (upgradeIndex == 0)
            {
                if (world.bar.resources[2] >= 50)
                {
                    world.bar.resources[2] -= 50;
                    range.radius = upgrade1Range;
                    range.addPosition();
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
            if (world.bar.resources[3] >= 50 && world.bar.resources[0] >= 100)
            {
                world.sfx.PlaySoundQuietly("upgrade");
                world.bar.resources[0] -= 100;
                world.bar.resources[3] -= 50;
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
                world.loader.replaceMusket(this);
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
                if (!isInRange(enemyFocusingOn))
                {

                    enemyFocusingOn = null;
                    setIdle();
                }
            }
            else
            {
                setIdle();
            }
            if (world.activeEnemies.Count == 0)
            {
                setIdle();
            }
            for (int i = 0; i < projectiles.Count; i++)
            {

                projectiles[i].Update();
                if (attack == AttackState.Attacking)
                {
                    for (int j = 0; j < world.activeEnemies.Count; j++)
                    {
                        if (projectiles[i].position.Intersects(world.activeEnemies[j].hitbox))
                        {
                            world.activeEnemies[j].incurDamage(trueDamage, 1);
                            if (world.activeEnemies[j].health <= 0)
                            {

                                enemyFocusingOn = null;
                            }
                            projectiles[i].shouldDelete = true;
                        }
                    }
                }
                if (projectiles[i].shouldDelete)
                {
                    projectiles.Remove(projectiles[i]);
                }

            }

        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].timer > 1)
                {
                    projectiles[i].Draw(gameTime, spriteBatch);
                }
            }
            base.Draw(gameTime, spriteBatch);

        }

    }
}
