using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace BulletsGoBrrr
{
	public abstract class ShootingPattern
	{
		public abstract ICollection<Projectile> Shoot(int timeInMilliseconds, Vector2 fireFrom,double hpPercentage);
	}
}

