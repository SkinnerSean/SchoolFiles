using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace BulletsGoBrrr
{
	public class TimedShots: ShootingPattern
	{
		int NextFireTime = int.MinValue;
		ProjectileHandler handler;
		int interval;
		public TimedShots(ProjectileHandler handler, int interval)
		{
			NextFireTime = (int)GameManager.CurrentGame.TotalMilliseconds;
			this.handler = handler;
			this.interval = interval;
		}

		public override ICollection<Projectile> Shoot(int ms, Vector2 fireFrom, double percent)
		{
			if (this.NextFireTime < ms)
			{
				this.NextFireTime = ms + this.interval;
				return new List<Projectile>
				{
					handler.createProjectile(fireFrom)
				};
			}
			return new List<Projectile>();
		}
        }
    }


