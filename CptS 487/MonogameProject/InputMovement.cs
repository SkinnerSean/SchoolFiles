using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    class InputMovement : Movement
    {
        private static InputMovement instance = new InputMovement();
        public static InputMovement Instance => instance;


        public Vector2 Move(Vector2 position, Texture2D graphic, GameManager game)
        {
            return (Vector2.Clamp(position + InputHandler.Instance.Velocity, Vector2.Zero, game.ScreenSize - new Vector2(graphic.Width, graphic.Height)));
        }
    }
}
