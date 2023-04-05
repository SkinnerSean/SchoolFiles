using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public abstract class Shooter:Damageable
    {
        protected Shooter(Movement movement, ShootingPattern shootingPattern, int initialHP, Texture2D texture) : base(movement, initialHP, texture)
        {
            this.shootingPattern = shootingPattern;
        }

        public override Vector2 Position { get; protected set; }
        protected ShootingPattern shootingPattern;

        public virtual Vector2 FiringLocation { get; protected set; }


        public ICollection<Projectile> Shoot(int elapsedMs)
        {
            return this.shootingPattern.Shoot(elapsedMs, FiringLocation, (double)this.HitPoints / this.InitialHitPoints);
        }

        // Preforms a gameloop update on the enemy
        public ICollection<Projectile> Update(int elapsedMs)
        {
            this.Move();
            return this.Shoot(elapsedMs);
        }

    }
}
