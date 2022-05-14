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
    class Rogue : AttackSuper
    {
        public List<Projectile> projectiles = new List<Projectile>();
        public Texture2D projectileTex;
        public Rogue()
        {
            addAnimationOrder(new int[] { 1, 2, 3, 4 });
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 0;
            speed = 20;
            rotation = 0;
            damage = 10;
            color = Color.White;
            upgrade1Damage = 12;
            upgrade2Damage = 15;
            currentTier = 1;
            infoTexts[0] = formatter.formatString(200, "50 Iron. Increase damage by 2");
            infoTexts[1] = formatter.formatString(200, "65 Iron. Increase damage by 3");
            infoTexts[3] = formatter.formatString(200, "125 Gold, 50 Steel. \nTier up to 'Assassin'");
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
            Projectile p = new Projectile();
            p.velocity = 14;
            p.rotation = this.rotation + 90;
            p.texture = projectileTex;
            p.decay = 30;
            p.position = new Rectangle(this.position.X, this.position.Y, 3, 12);
            p.setOrigin();
            p.offset = world.offsetX - 640;
            projectiles.Add(p);
            world.sfx.PlaySoundPitchUp("swoosh");
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
                    upgradeIndex++;
                    upgrade.showInfoBox = false;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }

        public override void checkTier()
        {
            if (world.bar.resources[3] >= 50 && world.bar.resources[0] >= 125)
            {
                world.sfx.PlaySoundQuietly("upgrade");

                world.bar.resources[0] -= 125;
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
                world.loader.replaceAssasin(this);
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
                            world.activeEnemies[j].incurDamage(trueDamage, 2);
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
                if (projectiles[i].timer > 4)
                {
                    projectiles[i].Draw(gameTime, spriteBatch);
                }
            }
            base.Draw(gameTime, spriteBatch);

        }

    }
}
