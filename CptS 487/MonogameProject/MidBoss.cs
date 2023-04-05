//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.Graphics;

//namespace BulletsGoBrrr
//{
//    public class MidBoss : BossEnemy
//    {


//        public MidBoss(Texture2D) : base(new DiagonalMovement(), PhasedShooting)
//        {
//        }

//        private static Texture2D texture;

//        public override Vector2 FiringLocation => new Vector2(Position.X + Bounds.Width / 2, Bounds.Bottom);


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
//                else if (this.HitPoints < 11)
//                {
//                    this.shootingPattern = ;
//                }
//            }
//        }

//    }
//}
