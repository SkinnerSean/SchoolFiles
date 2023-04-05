using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BulletsGoBrrr
{
    class InputHandler
    {
        private static InputHandler instance = new InputHandler();
        public static InputHandler Instance => instance;

        float speed;

        public bool IsShooting { get; protected set; } = false;
        public bool RequestingExit { get; protected set; } = false;
        public Vector2 Velocity { get; protected set; } = Vector2.Zero;

        public void Upate(GameTime gametime)
        {
            var kstate = Keyboard.GetState();

            IsShooting = kstate.IsKeyDown(Keys.Space) ;
            RequestingExit = Keyboard.GetState().IsKeyDown(Keys.Escape);

            Vector2 velocity = Vector2.Zero;

            if (kstate.IsKeyDown(Keys.Up))
                velocity.Y -= 2;

            if (kstate.IsKeyDown(Keys.Down))
                velocity.Y += 2;

            if (kstate.IsKeyDown(Keys.Left))
                velocity.X -= 2;

            if (kstate.IsKeyDown(Keys.Right))
                velocity.X += 2;

            // Let right control toggle slow mode for player
            if (kstate.IsKeyDown(Keys.RightControl))
                {
                    speed = 100f;
                }
                else
                {
                    speed = 200f;
                }

            velocity *= speed * (float)gametime.ElapsedGameTime.TotalSeconds;
            Velocity = velocity;
        }
    }
}
