﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    class SplitShooting : ShootingPattern
    {
        int bullets = 0;
        int NextFireTime = int.MinValue;
        int msPerCycle = 2000;
        int msBetweenShots = 2000;

        ProjectileHandler handler;
        public SplitShooting(ProjectileHandler handler,int bullets, int stagger, int staggerTotal) {
            this.handler = handler; this.bullets = bullets; this.msBetweenShots = stagger; this.msPerCycle = staggerTotal;
        }

        public override ICollection<Projectile> Shoot(int ms, Vector2 fireFrom, double percent)
        {
            List<Projectile> bulletList = new List<Projectile>();
            double angle = 45 * (Math.PI / 180);
            double newAngle = 0;
            float percentage = (ms % msPerCycle) / (float)msPerCycle;

            if (percentage > 0.5)
            {
                if (this.NextFireTime < ms)
                {
                    this.NextFireTime = ms + msBetweenShots;

                    newAngle = ((90) / this.bullets) * Math.PI / 180;

                    for (int i = 0; i <= this.bullets; i++)
                    {
                        Projectile p = handler.createProjectile(fireFrom);
                        p.Movement = new ConstantMovement(new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)), 6);
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