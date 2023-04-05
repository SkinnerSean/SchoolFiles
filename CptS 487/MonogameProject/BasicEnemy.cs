using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public class BasicEnemy : Enemy
    {
       // private int unitHP; // The unit's health

        public static Random rng = new Random();

        public static int TicksBetweenShots = 50;
        public override Vector2 FiringLocation => new Vector2(Position.X + Bounds.Width / 2, Bounds.Bottom);

        //public BasicEnemy(Movement movement,Texture2D texture) : base(movement,
        //                new TimedShots(new BasicEnemyBulletHandler(Vector2.UnitY), 1000),1, texture)
        public BasicEnemy(Movement movement, ShootingPattern shootingPattern,Texture2D texture, int initialHp) : base(movement, shootingPattern, initialHp, texture)
        {
            Position = new Vector2(rng.Next(300), 3);
        }

    }
}