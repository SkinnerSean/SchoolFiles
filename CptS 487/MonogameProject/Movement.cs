using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace BulletsGoBrrr
{
    public interface Movement
    {
        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game);
    }
}
