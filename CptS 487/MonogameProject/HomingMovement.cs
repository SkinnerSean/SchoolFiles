using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
	public class HomingMovement: Movement
	{
        private float speed;

        public Vector2 Velocity { get; private set; }

        public HomingMovement(Vector2 position, float speed) :
			base()
		{
			this.speed = speed;
			this.Velocity = DirectionToPlayer(position);
		}

		Vector2 DirectionToPlayer(Vector2 position) => Vector2.Normalize(GameManager.CurrentGame.player.Position - position);


		public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
		{
			this.Velocity += DirectionToPlayer(position)/20;
			this.Velocity.Normalize();
			return position + this.Velocity * speed;
		}
	}
}

