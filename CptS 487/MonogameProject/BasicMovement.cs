using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public class BasicMovement : Movement
    {
        Vector2 velo = new Vector2(2,0);

        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
        {
            if (position.X >= game.ScreenWidth - graphic.Width || position.X <= 0)
                velo.X *= -1;

            if (position.Y >= game.ScreenHeight - graphic.Height || position.Y <= 0)
                velo.Y *= -1;

            return position + velo;
        }
    }
}