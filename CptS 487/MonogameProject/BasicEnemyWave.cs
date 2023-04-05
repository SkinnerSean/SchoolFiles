using Microsoft.Xna.Framework.Graphics;

namespace BulletsGoBrrr
{
    public class BasicEnemyWave : AbstractWave
    {
        Movement movement;
        ShootingPattern shootingPattern;
        Texture2D texture;
        int initialHp;
        public BasicEnemyWave(Movement movement, ShootingPattern shootingPattern, Texture2D texture,int initialHp, int enemyCount, int startTime, int spawnInterval,int duration) :
            base(enemyCount, startTime, spawnInterval, duration)
        {
            this.movement = movement;
            this.shootingPattern = shootingPattern;
            this.texture = texture;
            this.initialHp = initialHp;
        }

        protected override Enemy CreateEnemy()
        {
            return new BasicEnemy(movement,shootingPattern,texture,initialHp);
        }
    }
}
