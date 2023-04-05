using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulletsGoBrrr
{
    public class PowerUps : Damageable
    {
        private float timer;
        private static Texture2D powerUp;
        public static Random rng = new Random();


        //public override Texture2D Texture => powerUp;

        public static Random random;

        public override Vector2 Position { get; protected set; }



        public PowerUps(Movement movement, int newHP,Texture2D texture): base(movement, newHP, texture)
        {
            HitPoints = newHP;
            Position = new Vector2(rng.Next(300), 3);
        }

        public override void Die()
        {
            GameManager.CurrentGame.Powerups.Remove(this);
            GameManager.CurrentGame.player.PowerUpHpManager();
        }
        public void Update()
        {
            this.Move();
        }
        }
    }
