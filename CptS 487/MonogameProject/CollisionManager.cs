using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace BulletsGoBrrr
{
    public class CollisionManager
    {
        public void PlayerProjectileCollisions()
        {
            GameManager game = GameManager.CurrentGame;
            for(int i = 0; i < game.AllEnemies.Count; i++)
            {
                Enemy enemy = game.AllEnemies[i];
                

                var collidedWithEnemy = game.PlayerProjectiles.Where(proj => enemy.Bounds.Intersects(proj.Bounds));
                
                if (collidedWithEnemy.Any())
                {
                    foreach (Projectile p in collidedWithEnemy)
                    {
                        /// TODO: Damage based on projectile
                        enemy.TakeDamage(p.Damage);
                        //powerball.TakeDamage(1);
                        Console.WriteLine($"{p.Damage}:{enemy.hitpoints}");
                    }
                    game.PlayerProjectiles.RemoveWhere(collidedWithEnemy.Contains);
                   
                }
            }
        }
        public void PowerballProjectileCollisions()
        {

            GameManager game = GameManager.CurrentGame;
            for (int i = 0; i < game.Powerups.Count; i++)
            {
                PowerUps powerball = game.Powerups[i];
                var collidedWithPowerball = game.PlayerProjectiles.Where(proj => powerball.Bounds.Intersects(proj.Bounds));
                if (collidedWithPowerball.Any())
                {
                    foreach (Projectile p in collidedWithPowerball)
                    {
                        /// TODO: Damage based on projectile  
                        powerball.TakeDamage(p.Damage);
                    }
                    game.PlayerProjectiles.RemoveWhere(collidedWithPowerball.Contains);
                }
            }



            //GameManager game = GameManager.CurrentGame;
            //var collidedWithPlayer = game.Powerups.Where(powerup => game.player.Bounds.Intersects(powerup.Bounds));
            //foreach (PowerUps p in collidedWithPlayer)
            //{
            //    /// TODO: Damage based on projectile
            //    p.TakeDamage(1);
            //}
            //game.Powerups.RemoveWhere(collidedWithPlayer.Contains);
        
    }
        public void EnemyProjectileCollisions()
        {
            GameManager game = GameManager.CurrentGame;
            var collidedWithPlayer = game.EnemyProjectiles.Where(proj => game.player.Bounds.Intersects(proj.Bounds));
            foreach (Projectile p in collidedWithPlayer)
            {
                /// TODO: Damage based on projectile
                game.player.TakeDamage(p.Damage);
            }
            game.EnemyProjectiles.RemoveWhere(collidedWithPlayer.Contains);
        }
    }
}
