using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    class DiagonalMovement : Movement
    {
        Vector2 velo = new Vector2(2, 2);
        
        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
        {
            if (position.X <= 0 || position.X >= (game.ScreenWidth) - graphic.Width)
            {
                velo.X *= -1;
            }

            if (position.Y <= 0 || position.Y >= (game.ScreenHeight / 2) - graphic.Height)
            {
                velo.Y *= -1;
            }

            return position+velo;
        }
    }
}
