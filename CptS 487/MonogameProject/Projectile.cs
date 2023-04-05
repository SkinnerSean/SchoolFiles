using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{

    // need Sprite class to inherit from
    public class Projectile: Sprite
    {
        /// <summary>
        /// The position the top-left corner of this projectile.
        /// </summary>
        public override Vector2 Position { get; protected set; }

        /// <summary>
        /// The damage this projectile will deal on collision.
        /// </summary>
        public int Damage { get; protected set; }

        // change once hitbox is refactored
        public Projectile(Vector2 position, Movement movement, Texture2D texture, int damage) :base(movement, texture)
        {
            this.Position = position;
            this.Damage = damage;
        }

        public void Update()
        {
            Move();
        }
        public bool ShouldRemove(GameManager manager)
        {
            return Position.X > manager.ScreenWidth + 30 || Position.X < -30 || Position.Y > manager.ScreenHeight + 30 || Position.Y < -30;
        }
    }

    //public class BasicBullet : Projectile
    //{

    //    private static Texture2D graphic;

    //    // other class specific attributes? damage? splash radius?
        
    //    public BasicBullet(Vector2 newPos, Movement movement, int damage) : base(movement,damage)
    //    {
    //        Position = newPos; // set to position right in front of character that is firing - need to determine what angle being fired at (done in game logic)
    //                           // need to rotate texture based on which direction bullet is being fired - done in game logic

    //        //Hitbox.HandleCollision += (bulletHitbox, target, layer, shouldDelete) =>
    //        //{
    //        //    Damageable damageable = ((Damageable)target.Parent);
    //        //    damageable.TakeDamage(damage);
    //        //};

    //        //float modifier = 1.0F;
    //        //if (slow)
    //        //{
    //        //    modifier = 0.5F;
    //        //}

    //        //Velocity = newVel * modifier; // set default velocity - use modifier for slow mode
    //    }


    //    // move to movement classes
    //    public override void Update(GameTime gameTime)
    //    {
    //        Move();
    //    }
    //}

    //public class BasicEnemyBullet : Projectile
    //{

    //    private static Texture2D graphic;


    //    public BasicEnemyBullet(Vector2 newPos, Movement movement, int damage) : base(movement, damage)
    //    {
    //        Position = newPos;
    //    }


    //    // move to movement classes
    //    public override void Update(GameTime gameTime)
    //    {
    //        Move();
    //    }
    //}

    //public class BossBullet : Projectile
    //{

    //    private static Texture2D graphic;

    //    // other class specific attributes? damage? splash radius?

    //    public BossBullet(Vector2 newPos, Movement movement, int damage) : base(movement,damage)
    //    {
    //        Position = newPos; // set to position right in front of character that is firing - need to determine what angle being fired at (done in game logic)
    //                           // need to rotate texture based on which direction bullet is being fired - done in game logic

    //        //Hitbox.HandleCollision += (bulletHitbox, target, layer, shouldDelete) =>
    //        //{
    //        //    Damageable damageable = ((Damageable)target.Parent);
    //        //    damageable.TakeDamage(damage);
    //        //};

    //        //float modifier = 1.0F;
    //        //if (slow)
    //        //{
    //        //    modifier = 0.5F;
    //        //}

    //        //Velocity = newVel * modifier; // set default velocity - use modifier for slow mode
    //    }

    //    // move to movement classes
    //    public override void Update(GameTime gameTime)
    //    {
    //        Move();
    //    }
    //}

    //public class Missile : Projectile
    //{

    //    private static Texture2D graphic;

    //    public Missile(Movement movement) : base(movement, 10)
    //    {

    //    }
    //    public override void Update(GameTime gameTime)
    //    {
    //    }    
    //}
}
