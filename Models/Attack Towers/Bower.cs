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
    class Bower : AttackSuper
    {
        public List<Projectile> projectiles = new List<Projectile>();
        public Texture2D projectileTex;
        public Bower()
        {

            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 5, 5, 5, 5, 6, 1, 1 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 8;
            speed = 135;
            rotation = 0;
            damage = 25;
            color = Color.White;
            infoTexts[0] = formatter.formatString(160, "40 Stone. Increase range by 70");
            infoTexts[1] = formatter.formatString(160, "50 Stone. Increase range by 80");
            infoTexts[2] = formatter.formatString(200, "50 Gold, 60 Iron. \nTier up to 'Crossbower'");
            upgrade1Range = 420;
            upgrade2Range = 500;

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
                if (world.bar.resources[1] >= 40)
                {
                    world.bar.resources[1] -= 40;
                    range.radius = upgrade1Range;
                    range.addPosition();
                    upgradeIndex++;
                    return;
                }
            }
            if (upgradeIndex == 1)
            {
                if (world.bar.resources[1] >= 50)
                {
                    world.bar.resources[1] -= 50;
                    range.radius = upgrade2Range;
                    range.addPosition();
                    upgrade.showInfoBox = false;
                    upgradeIndex++;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }

        public override void checkTier()
        {
            if (world.bar.resources[2] >= 60 && world.bar.resources[0] >= 50)
            {
                world.sfx.PlaySoundQuietly("upgrade");
                world.bar.resources[0] -= 50;
                world.bar.resources[2] -= 60;
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
                world.loader.replaceCrossbower(this);
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
