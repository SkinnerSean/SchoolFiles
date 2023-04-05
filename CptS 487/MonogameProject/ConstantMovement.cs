using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
	public class ConstantMovement: Movement
	{
        Vector2 Velocity;

        public ConstantMovement(Vector2 direction, float speed)
		{
            this.Velocity = direction;
            this.Velocity.Normalize();
            this.Velocity *= speed;
		}
        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
        {
            return position + Velocity;
        }
    }
}

