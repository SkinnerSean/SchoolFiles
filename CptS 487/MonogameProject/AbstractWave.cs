using System;
using System.Collections.Generic;
using System.Text;

namespace BulletsGoBrrr
{
    public abstract class AbstractWave
    {
        public int EnemyCount { get; private set; }
        public int StartTime { get; private set; }
        public int SpawnInterval { get; private set; }
        public int Duration { get; private set; }

        public int NextSpawn { get; private set; }
        public List<Enemy> EnemiesSpawned { get; private set; }

        public int EndTime { get; private set; }
        public bool Exited { get; private set; } = false;

        public AbstractWave(int enemyCount, int startTime, int spawnInterval, int duration)
        {
            this.EnemyCount = enemyCount;
            this.StartTime = startTime;

            this.SpawnInterval = spawnInterval;
            this.Duration = duration;

            NextSpawn = StartTime + SpawnInterval;
            EndTime = startTime + duration;
            EnemiesSpawned = new List<Enemy>();
        }

        protected abstract Enemy CreateEnemy();

        public bool IsDone() { return EnemiesSpawned.Count >= EnemyCount; }

        public Enemy TrySpawn(int time)
        {
            if (NextSpawn<=time && !IsDone())
            {
                NextSpawn += SpawnInterval;
                Enemy newEnemy = CreateEnemy();
                EnemiesSpawned.Add(newEnemy);
                return newEnemy;
            }
            return null;
        }
        public void Update(int time)
        {
            if (EndTime<=time && !Exited)
            {
                Exited = true;
                foreach(Enemy enemy in EnemiesSpawned)
                {
                    Console.WriteLine($"Exit ${enemy}");
                    enemy.Movement = new BasicExit();
                }
            }
        }
    }
}
