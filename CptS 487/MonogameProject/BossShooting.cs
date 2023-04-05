using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    public class BossShooting : ShootingPattern
    {
        int NextFireTime = int.MinValue;
        static int msPerCycle = 2000;
        static int msBetweenShots = 50;

        ProjectileHandler handler;
        public BossShooting(ProjectileHandler handler) { this.handler = handler; }

        public override ICollection<Projectile> Shoot(int ms, Vector2 fireFrom, double percent)
        {
            float percentage = (ms % msPerCycle) /  (float)msPerCycle;

            if (percentage > 0.5)
            {
                if (this.NextFireTime < ms)
                {
                    this.NextFireTime = ms + msBetweenShots;
                    return new List<Projectile>
                    {
                        handler.createProjectile(fireFrom)
                    };
                }
            }
            return new List<Projectile>();   
        }
    }
}

