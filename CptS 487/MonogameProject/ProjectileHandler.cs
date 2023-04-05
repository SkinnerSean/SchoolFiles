using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    abstract public class ProjectileHandler
    {
        abstract public Projectile createProjectile(Vector2 firingLocation);
    }

    public class BasicBulletHandler : ProjectileHandler
    {
        Texture2D texture;
        Movement movement;
        public BasicBulletHandler(Texture2D texture, Movement movement)
        {
            this.texture = texture;
            this.movement = movement;
        }

        override public Projectile createProjectile(Vector2 firingLocation)
        {
            //return new Projectile(texture, new ConstantMovement(velocity, 6), 1);
            return new Projectile(firingLocation, movement, texture, 1);
        }
    }

    //public class BasicEnemyBulletHandler : ProjectileHandler
    //{
    //    Vector2 velocity;

    //    public BasicEnemyBulletHandler(Vector2 velocity)
    //    {
    //        this.velocity = velocity;
    //    }

    //    override public Projectile createProjectile(Vector2 firingLocation)
    //    {
    //        return new BasicEnemyBullet(firingLocation, new ConstantMovement(velocity, 4), 1);
    //    }
    //}

    //public class BossBulletHandler : ProjectileHandler
    //{
    //    public override Projectile createProjectile(Vector2 firingLocation)
    //    {
    //        return new BossBullet(firingLocation, new TowardsPlayerMovement(firingLocation,2), 5);
    //    }
    //}

    //public class BurstBulletHandler : ProjectileHandler
    //{

    //    public override Projectile createProjectile(Vector2 firingLocation)
    //    {
    //        return new BossBullet(firingLocation, new ConstantMovement(firingLocation, 4), 5);
    //    }

    //    public Projectile createProjectile(Vector2 firingLocation, Vector2 angle)
    //    {
    //        return new BossBullet(firingLocation, new ConstantMovement(angle, 4), 5);
    //    }
    //}

    public class HomingBulletHandler : ProjectileHandler
    {
        Texture2D texture;
        public HomingBulletHandler(Texture2D texture)
        {
            this.texture = texture;
        }
        public override Projectile createProjectile(Vector2 firingLocation)
        {
            //return new BossBullet(firingLocation, new HomingMovement(firingLocation, 3), 5);
            return new Projectile(firingLocation, new HomingMovement(firingLocation, 3), texture, 1);

        }
    }
}
