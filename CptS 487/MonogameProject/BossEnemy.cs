using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public abstract class BossEnemy : Shooter
    {
        public static PhasedShooting MidbossShooting(TextureManager t) => new PhasedShooting(new ShootingPattern[] {
            new BossShooting(new BasicBulletHandler(t["bossbullet"],new ConstantMovement(Vector2.UnitY, 6))),
            new SplitShooting(new BasicBulletHandler(t["bossbullet"],new ConstantMovement(Vector2.UnitY, 6)), 2, 300, 2000)
        });
        public static PhasedShooting FinalBossShooting(TextureManager t) => new PhasedShooting(new ShootingPattern[] {
            new TimedShots(new HomingBulletHandler(t["bossbullet"]),500),
            new BurstShooting(new BasicBulletHandler(t["bossbullet"],null), 8, 2000, 2000),
            new BurstShooting(new BasicBulletHandler(t["bossbullet"],null), 12, 2000, 2000),
            new BurstShooting(new BasicBulletHandler(t["bossbullet"],null), 10, 350, 10000),
        });

        //public BossEnemy(Movement movement, ShootingPattern shooting):base(
        //    movement,
        //    shooting,20
        //    )
        public BossEnemy(Movement movement, ShootingPattern shootingPattern, Texture2D texture, int initialHp) : base(movement, shootingPattern, initialHp, texture)

        {
            Position = new Vector2(50, 10);            
        }
    }
}
