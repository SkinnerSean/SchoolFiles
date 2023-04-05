using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    public class Player: Shooter
    {
        public Player(Texture2D texture,Texture2D bullet) : base(
            InputMovement.Instance,
            new InputShooting(new BasicBulletHandler(bullet,new ConstantMovement(-Vector2.UnitY,6))),
            2000,
            texture
        )
        {
            Position = new Vector2(250, 200);
        }

        public override Vector2 FiringLocation => new Vector2(Position.X + Bounds.Width / 2, Bounds.Top);

        public override void Die()
        {
            GameManager.CurrentGame.EndGame(GameManager.GameState.Lost);
        }
        public void PowerUpHpManager()
        {
            HitPoints = HitPoints * 2;
        }
    }
}