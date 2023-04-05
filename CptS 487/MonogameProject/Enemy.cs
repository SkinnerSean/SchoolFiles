using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public abstract class Enemy: Shooter
    {
        protected Enemy(Movement movement, ShootingPattern shootingPattern, int initialHP, Texture2D texture) : base(movement,shootingPattern, initialHP, texture)
        { }

        public override void Die()
        {
            GameManager.CurrentGame.AllEnemies.Remove(this);
        }

    }
}
