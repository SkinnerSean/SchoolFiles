using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    class BurstShooting : ShootingPattern
    {
        int bullets = 0;
        int pattern = 0;
        int NextFireTime = int.MinValue;
        int msPerCycle = 2000;
        int msBetweenShots = 2000;

        ProjectileHandler handler;
        public BurstShooting(ProjectileHandler handler, int bullets, int stagger, int staggerTotal) { this.handler = handler; this.bullets = bullets; this.msBetweenShots = stagger; this.msPerCycle = staggerTotal; }

        public override ICollection<Projectile> Shoot(int ms, Vector2 fireFrom, double percent)
        {
            List<Projectile> bulletList = new List<Projectile>();
            double angle = 0;
            double newAngle = 0;
            float percentage = (ms % msPerCycle) / (float)msPerCycle;

            if (pattern == 1)
            {
                pattern = 2;
                angle = 0;
            }
            else
            {
                pattern = 1;
                angle = Math.PI / 3;
            }

            if (percentage > 0.5)
            {
                if (this.NextFireTime < ms)
                {
                    this.NextFireTime = ms + msBetweenShots;

                    newAngle = (360 / this.bullets) * Math.PI/180;

                    for (int i = 0; i <= this.bullets; i++)
                    {
                        Projectile p = handler.createProjectile(fireFrom);
                        p.Movement = new ConstantMovement(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)),2);
                        bulletList.Add(p);
                        angle += newAngle;
                    }

                    return bulletList;
                }
            }
            return new List<Projectile>();
        }
    }
}

