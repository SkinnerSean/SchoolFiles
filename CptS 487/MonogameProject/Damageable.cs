using System;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
	public abstract class Damageable: Sprite
	{
		public int hitpoints;
		public int InitialHitPoints { get; private set; }
		public int HitPoints { get=> hitpoints; protected set=> hitpoints=Math.Max(value,0); }
		public bool IsAlive { get; protected set; } = true;

		public abstract void Die();

		public Damageable(Movement movement, int initialHP, Texture2D texture) :base(movement,texture)
		{
			this.InitialHitPoints = initialHP;
			this.hitpoints = initialHP;
		}

		public virtual void TakeDamage(int damage)
        {
			if (this.IsAlive)
			{
				this.HitPoints -= damage;
				if (this.HitPoints <= 0)
				{
					this.Die();
					this.IsAlive = false;
				}
			}
		}
	}
}

