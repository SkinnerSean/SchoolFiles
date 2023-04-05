//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;

//namespace BulletsGoBrrr
//{
//    public class FinalBoss : BossEnemy
//    {
//        private static Texture2D texture;
//        private int firingChange = 1;
//        private int pattern = 1;
//        public override Vector2 FiringLocation
//        {
//            get
//            {
//                if (firingChange == 1)
//                {
//                    return new Vector2(Position.X + Bounds.Width / 2, Bounds.Bottom);
//                }
//                else
//                {
//                    return new Vector2(Position.X + Bounds.Width / 2, Position.Y + Bounds.Height / 2);
//                }
//            }
            
//        }

//        public FinalBoss() : base(new DiagonalMovement(),new TimedShots(new HomingBulletHandler(),500))
//        {
//            FiringLocation = new Vector2(Position.X + Bounds.Width / 2, Bounds.Bottom);
//        }

//        public override void TakeDamage(int damage)
//        {
//            if (this.IsAlive)
//            {
//                this.HitPoints -= damage;
//                if (this.HitPoints <= 0)
//                {
//                    this.Die();
//                    this.IsAlive = false;
//                }
//                else if (this.HitPoints < 15 && pattern == 1)
//                {
//                    firingChange = 2;
//                    pattern++;
//                    this.shootingPattern = new BurstShooting(new BurstBulletHandler(), 8, 2000, 2000);
//                }
//                else if (this.HitPoints < 10 && pattern == 2)
//                {
//                    pattern++;
//                    this.shootingPattern = new BurstShooting(new BurstBulletHandler(), 12, 2000, 2000);
//                }
//                else if (this.HitPoints < 5 && pattern == 3)
//                {
//                    pattern++;
//                    this.shootingPattern = new BurstShooting(new BurstBulletHandler(), 10, 350, 10000);
//                }
//            }
//        }

//    }
//}
