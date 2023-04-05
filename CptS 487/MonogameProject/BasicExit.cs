using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    class BasicExit : Movement
    {
        Vector2 velo = new Vector2(4, 0);

        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
        {
            return position+velo;
        }
    }
}
