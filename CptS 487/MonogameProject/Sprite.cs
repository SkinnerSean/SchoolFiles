using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    abstract public class Sprite
    {
        public Sprite(Movement movement,Texture2D texture)
        {
            this.Movement = movement;
            this.Texture = texture;
        }

        /// <summary>
        /// The position the top-left corner of this projectile.
        /// </summary>
        public abstract Vector2 Position { get; protected set; }


        /// <summary>
        /// The texture drawn for this sprite.
        /// </summary>
        public Texture2D Texture { get; protected set; }

        /// <summary>
        /// Draw the sprite to the screen.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.Texture, Position, Color.White);
        }

        public Rectangle Bounds => new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);

        public Movement Movement { get; set; }
        public void Move()
        {
            Position = Movement.Move(Position, Texture, GameManager.CurrentGame);
        }
    }
}
