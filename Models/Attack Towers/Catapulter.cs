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
    public class Catapulter : AttackSuper
    {
        public Texture2D explosionTex;
        public Texture2D shadowTex;
        public Rectangle projectilePos;
        
        public Ballista projectile = new Ballista();
        public Catapulter()
        {
            addAnimationOrder(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
                1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 3, 4, 5, 5, 5, 5, 5, 5, 5, 5, 5, 6, 6});
            addIdleOrder(new int[] { 1, 1 });
            attackStartIndex = 3;
            speed = 240;
            rotation = 0;
            doesTurn = false;
            damage = 150;
            offset = 0;
            color = Color.White;
            infoTexts[0] = formatter.formatString(160, "60 Iron. Increase damage by 20");
            infoTexts[1] = formatter.formatString(160, "70 Iron. Increase damage by 20");
            infoTexts[3] = formatter.formatString(200, "100 Gold, 70 Iron, 80 Steel. \nTier up to 'Mortar'");
            upgrade1Damage = 170;
            upgrade2Damage = 190;
            currentTier = 1;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    sources.Add(new Rectangle(j * 640, i * 640, 640, 640));
                }
            }
        }
        public void setProjectile()
        {
            projectile.explosionTex = explosionTex;
            projectile.shadowTex = shadowTex;
            icon.position = new Rectangle(position.X + 52, position.Y + 47, 15, 15);
        }
        public override void doAttack()
        {
            base.doAttack();
            projectile = new Ballista();
            projectile.explosionTex = explosionTex;
            projectile.shadowTex = shadowTex;
            projectile.position = new Rectangle(projectilePos.X - 64, projectilePos.Y - 64, 129, 129);
            projectile.shadowPos = new Rectangle(projectilePos.X - 21, projectilePos.Y - 21, 43, 43);
            projectile.state = Ballista.State.Shadow;
            projectile.timer = 0;
            projectile.color = new Color(255, 255, 255, 0);
            
        }
        public override void isBeingClicked(MouseState mouse)
        {
            base.isBeingClicked(mouse);
            if (world.era <= 1)
            {
                tierUp.showInfoBox = false;
            }
        }
        public override void Update()
        {
            base.Update();
            if((timer + 35) % speed == 0) {
                world.sfx.PlaySound("ballista", 0.3f);
                currentSoundPlaying = world.sfx.currentSound;
            }
            projectile.Update();
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
                projectilePos = enemyFocusingOn.position;

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

            if (attack == AttackState.Attacking)
            {
                for (int j = 0; j < world.activeEnemies.Count; j++)
                {
                    if (projectile.position.Intersects(world.activeEnemies[j].hitbox) && projectile.canDamage == true)
                    {
                       
                        if (!world.activeEnemies[j].projectiles.Contains(projectile))
                        {

                            world.activeEnemies[j].incurDamage(trueDamage, 4);
                            world.activeEnemies[j].projectiles.Add(projectile);
                        }
                        
                        if (world.activeEnemies[j].health <= 0)
                        {

                            enemyFocusingOn = null;
                        }

                    }
                }
            }
            /*
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
            */
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
                    upgradeIndex++;
                    damage = upgrade2Damage;
                    upgrade.showInfoBox = false;
                    upgrade.type = UpgradeButton.ViewType.Maxed;
                }
            }

        }

        public override void checkTier()
        {
            if (world.bar.resources[0] >= 100 && world.bar.resources[2] >= 70 && world.bar.resources[3] >= 80)
            {
                world.sfx.PlaySoundQuietly("upgrade");
                world.bar.resources[0] -= 100;
                world.bar.resources[2] -= 70;
                world.bar.resources[3] -= 80;
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
                world.loader.replaceMortar(this);
            }
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            projectile.Draw(gameTime, spriteBatch);
        }
    }
}
