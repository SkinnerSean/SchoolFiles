using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletsGoBrrr
{
    public class PhasedShooting:ShootingPattern
    {
        private ShootingPattern[] patterns;
        public PhasedShooting(ShootingPattern[] patterns)
        {
            this.patterns = patterns;
        }

        public override ICollection<Projectile> Shoot(int timeInMilliseconds, Vector2 fireFrom, double hpPercentage)
        {
            int index = (int)Math.Floor(hpPercentage.Remap(0, 1, patterns.Length, 0));
            Console.WriteLine($"{index}/{patterns.Length}: {hpPercentage}");
            return patterns[index].Shoot(timeInMilliseconds, fireFrom, hpPercentage);
        }
    }
}
