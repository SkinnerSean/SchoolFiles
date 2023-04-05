using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    public class InputShooting : ShootingPattern
    {
        int NextShot = int.MinValue;
        static int timeBetweenShots = 250;

        ProjectileHandler handler;
        public InputShooting(ProjectileHandler handler)
        {
            this.handler = handler;
        }

        public override ICollection<Projectile> Shoot(int ms, Vector2 fireFrom, double percent)
        {
            if (InputHandler.Instance.IsShooting && NextShot < ms)
            {
                NextShot = ms + timeBetweenShots;
                return new List<Projectile> { handler.createProjectile(fireFrom) };
                //bullet = new BasicBullet(new Vector2(Position.X, Position.Y - 1), new ConstantMovement(new Vector2(0, -1), 2), 1);
            }
            return new List<Projectile>();
        }
    }
}

