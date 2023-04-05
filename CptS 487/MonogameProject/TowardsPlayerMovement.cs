using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
	public class TowardsPlayerMovement : ConstantMovement
	{
		public TowardsPlayerMovement(Vector2 position, float speed):
			base(GameManager.CurrentGame.player.Position-position,speed)
		{}
    }
}

