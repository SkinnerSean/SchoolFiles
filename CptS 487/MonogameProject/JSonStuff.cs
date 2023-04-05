using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;

namespace BulletsGoBrrr
{
    class JSonStuff
    {
        private class Config
        {
            public WaveConfig[] waves { get; set; }
        }
        private class WaveConfig
        {
            public string enemyType { get; set; }
            public string bulletType { get; set; }
            public int startTime { get; set; }

            public int count { get; set; }
            public int spawnInterval { get; set; }

            public int duration { get; set; }

            public bool isBoss { get; set; }

            public int move { get; set; }

            public int shootpattern { get; set; }
        }
        /// <summary>
        /// Saves our player's info to our JSon file.
        /// </summary>
        /// <param name="inList">Input list of player score, lives, and wave number.</param>
        public static void SaveToFile(List<int> inList)
        {
            string json = JsonSerializer.Serialize(inList);
            File.WriteAllText(Environment.CurrentDirectory + "playerSave.json", json);
        }

        /// <summary>
        /// Loads the player's save from JSon file.
        /// </summary>
        public static void LoadPlayerSave()
        {
            /*
            using (StreamReader r = new StreamReader("playerSave"))
            {
                dynamic json = r.ReadToEnd();
                foreach (var item in json)
                {
                    Console.WriteLine(item);
                }
            }*/
        }

        public static List<AbstractWave> LoadWaves(TextureManager textures)
        {
            using (var configFile = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("BulletsGoBrrr.json.waves.json")))
            {
                return LoadWaves(configFile.ReadToEnd(), textures);
            }
        }
        public static List<AbstractWave> LoadWaves(string jsonString, TextureManager textures)
        {
            Console.WriteLine(jsonString)
;            Config config = JsonSerializer.Deserialize<Config>(jsonString);
            return config.waves.Select<WaveConfig,AbstractWave>(waveConfig =>
            {
                Movement m;
                ShootingPattern s;

                switch (waveConfig.shootpattern)
                {
                    case 1:
                        s = new TimedShots(new BasicBulletHandler(textures[waveConfig.bulletType], new ConstantMovement(Vector2.UnitY, 4)), 1000);
                        break;
                    case 2:
                        s = BossEnemy.MidbossShooting(textures);
                        break;
                    case 3:
                        s = BossEnemy.FinalBossShooting(textures);
                        break;
                    default:
                        s = new TimedShots(new BasicBulletHandler(textures[waveConfig.bulletType], new ConstantMovement(Vector2.UnitY, 4)), 1000);
                        break;
                }

                switch (waveConfig.move)
                {
                    case 1:
                        m = new BasicMovement();
                        break;
                    case 2:
                        m = new DiagonalMovement();
                        break;
                    case 3:
                        m = new InputMovement();
                        break;
                    default:
                        m = new BasicMovement();
                        break;
                }

                if (waveConfig.isBoss)
                {
                    return new BasicEnemyWave(
                       m,
                       s,
                       textures[waveConfig.enemyType],
                       20,
                       waveConfig.count, waveConfig.startTime, waveConfig.spawnInterval, waveConfig.duration
                    );

                }
                else
                {
                    return new BasicEnemyWave(
                       m,
                       s,
                       textures[waveConfig.enemyType],
                       1,
                       waveConfig.count, waveConfig.startTime, waveConfig.spawnInterval, waveConfig.duration
                    );
                }
            }).ToList();
        }

        /// <summary>
        /// Loads the wave from the JSon file.
        /// </summary>
        /// <param name="waveNum">Wave number.</param>
        //public AbstractWave LoadWave(int waveNum)
        //{
        //    List<int> jsonList = new List<int>();
        //    using (StreamReader r = new StreamReader(Environment.CurrentDirectory + "wave" + waveNum.ToString() + ".json"))
        //    {
        //        dynamic json = r.ReadToEnd();
        //        jsonList = JsonSerializer.Deserialize<List<int>>(json);
                
        //        // For first two waves
        //        if (waveNum == 1 || waveNum == 2)
        //        {
        //            return new EnemyAWave(jsonList[0], jsonList[1], jsonList[2]);
        //        } // Mid boss wave
        //        else if (waveNum == 3)
        //        {
        //            return new MidBossWave(jsonList[0], jsonList[1]);
        //        } // Next two waves
        //        else if (waveNum == 4 || waveNum == 5)
        //        {
        //            return new EnemyBWave(jsonList[0], jsonList[1], jsonList[2]);
        //        } // Final boss wave
        //        else
        //        {
        //            return new FinalBossWave(jsonList[0], jsonList[1]);
        //        }
        //    }
        //}
    }
}