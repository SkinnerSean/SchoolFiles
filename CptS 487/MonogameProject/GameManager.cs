using System;
using System.Windows.Input;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace BulletsGoBrrr
{

    public class GameManager : Game
    {
        public enum GameState
        {
            Running,
            Lost,
            Won
        }

        /// <summary>
        /// All projectiles in the game
        /// </summary>
        public HashSet<Projectile> PlayerProjectiles { get; } = new HashSet<Projectile>();
        public HashSet<Projectile> EnemyProjectiles { get; } = new HashSet<Projectile>();
        public List<PowerUps> Powerups { get; } = new List<PowerUps>();

        public List<AbstractWave> Waves;

        public int CurrentWaveIndex = 0;
        public int WaveStart = 0;

        public static GameManager CurrentGame;

        public double TotalMilliseconds;
        public int StartTime;
        public KeyboardState KeyboardState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private String menuString;

        bool GameOver = false;
        bool StartMenu = true;
        GameState CurrentState = GameState.Running;
        Song song;

        public int ScreenWidth => graphics.GraphicsDevice.Viewport.Width;
        public int ScreenHeight => graphics.GraphicsDevice.Viewport.Height;

        public Vector2 ScreenSize => new Vector2(ScreenWidth, ScreenHeight);

        public Texture2D backgroundSprite;

        public List<Enemy> AllEnemies = new List<Enemy>();
        public SpriteFont gameFont;

        public Player player;

        CollisionManager collisionManager;
        TextureManager textureManager;
        public GameManager()
        {
            graphics = new GraphicsDeviceManager(this);
            textureManager = new TextureManager(Content);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            CurrentGame = this;
            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            collisionManager = new CollisionManager();
            menuString = "1. Start game.\n2. Exit game.";

            base.Initialize();

            //JSonStuff.SaveToFile(new List<int>{player.HitPoints});

            //System.Environment.Exit(0);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundSprite = textureManager.Load("sky");
            textureManager.LoadAll(new string[]{
                "bullet", // BasicBullet
                "enemybullet", // BasicEnemyBullet
                "bossbullet", // BossBullet
                "BossEnemy", // MidBoss
                "bigboss", // FinalBoss
                "enemya", // EnemyA
                "enemyb", // EnemyB
                "player", // Player
                "healthpowerup", // PowerUps
            });
            Console.WriteLine(textureManager);
            PowerUps powerball = new PowerUps(new BasicMovement(), 1,textureManager["healthpowerup"]);
            Powerups.Add(powerball);
            player = new Player(textureManager["player"], textureManager["bullet"]);
            Waves = JSonStuff.LoadWaves(textureManager);
            gameFont = Content.Load<SpriteFont>("galleryFont");
            this.song = Content.Load<Song>("Fk Failure! Time to Prevail with Gusto!");
            MediaPlayer.Play(song);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
        }

        void MediaPlayer_MediaStateChanged(object sender, System.
                                           EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }


        public void EndGame(GameState state)
        {
            //JSonStuff.SaveToFile(new List<int> { this.player.hitpoints });
            GameOver = true;
            CurrentState = state;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.P))
                MediaPlayer.Volume -= 0.1f;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.O))
                MediaPlayer.Volume += 0.1f;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || InputHandler.Instance.RequestingExit)
                Exit();

            if (!StartMenu)
            {
                TotalMilliseconds = gameTime.TotalGameTime.TotalMilliseconds - StartTime;
                InputHandler.Instance.Upate(gameTime);

                // TODO: Add your update logic here
                KeyboardState = Keyboard.GetState();

                foreach (Enemy enemy in AllEnemies)
                {
                    var enemyProjectiles = enemy.Update((int)TotalMilliseconds);
                    AddEnemyProjectiles(enemyProjectiles);
                }


                var playerBullet = player.Update((int)TotalMilliseconds);
                AddPlayerProjectiles(playerBullet);

                foreach (Projectile projectile in PlayerProjectiles)
                {
                    projectile.Update();
                }
                foreach (Projectile projectile in EnemyProjectiles)
                {
                    projectile.Update();
                }            
                foreach (PowerUps powerBall in Powerups)
                {
                    powerBall.Update();
                }

                collisionManager.EnemyProjectileCollisions();
                collisionManager.PowerballProjectileCollisions();
                collisionManager.PlayerProjectileCollisions();

                PlayerProjectiles.RemoveWhere(p => OutOfBounds(p.Bounds));
                EnemyProjectiles.RemoveWhere(p => OutOfBounds(p.Bounds));
                AllEnemies.RemoveAll(e => OutOfBounds(e.Bounds));

                UpdateWave(gameTime);
            }
            else
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D1))
                {
                    StartMenu = false;
                    StartTime = gameTime.TotalGameTime.Seconds;
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D2))
                {
                    GameOver = true;
                }
            }

            base.Update(gameTime);
        }

        private bool OutOfBounds(Rectangle r)
        {
            return (
                -r.Width >= r.X || r.X >= ScreenWidth ||
                -r.Height >= r.Y || r.Y >= ScreenHeight
           );
        }

        private void UpdateWave(GameTime gameTime)
        {
            int time = (int)gameTime.TotalGameTime.TotalSeconds - StartTime;
            if (CurrentWaveIndex < Waves.Count)
            {
                //Console.WriteLine("{0}(-{1})", gameTime.TotalGameTime.Seconds, WaveStart);
                AbstractWave wave = Waves[CurrentWaveIndex];
                Enemy enemy = wave.TrySpawn(time);
                if (enemy != null)
                    AllEnemies.Add(enemy);
                if (wave.IsDone())
                {
                    //Console.WriteLine("NewWave", gameTime.TotalGameTime.Seconds);
                    CurrentWaveIndex++;
                }
            }

            else if (AllEnemies.Count == 0)
            {
                EndGame(GameState.Won);
            }

            foreach (AbstractWave wave in Waves)
            {
                wave.Update(time);
            }
            
        }

        private void AddEnemyProjectiles(ICollection<Projectile> bullets)
        {
            if (bullets != null)
            {
                EnemyProjectiles.UnionWith(bullets);
            }
        }
        private void AddPlayerProjectiles(ICollection<Projectile> bullets)
        {
            if (bullets != null)
            {
                PlayerProjectiles.UnionWith(bullets);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundSprite, new Vector2(0, 0), Color.White);
            if (GameOver)
            {
                string stateString = CurrentState == GameState.Lost?"Game Over.":"You won!";
                spriteBatch.DrawString(gameFont, $"{stateString} ESC to exit", new Vector2(10, 10), Color.Black);
            }
            else
            {
                if (!StartMenu)
                {
                    foreach (Projectile projectile in PlayerProjectiles)
                    {
                        projectile.Draw(spriteBatch);
                    }

                    foreach (Projectile projectile in EnemyProjectiles)
                    {
                        projectile.Draw(spriteBatch);
                    }

                    foreach (Enemy enemy in AllEnemies)
                    {
                        enemy.Draw(spriteBatch);
                    }


                    foreach (PowerUps powerup in Powerups)
                    {
                        powerup.Draw(spriteBatch);
                    }

                    player.Draw(spriteBatch);

                    string healthString = "Player HP: " + player.hitpoints.ToString();
                    /*string debugString = $"DEBUG: PlayerBullets: {PlayerProjectiles.Count} || " +
                        $"EnemyBullets: {EnemyProjectiles.Count} ||" +
                        $"Enemies: {AllEnemies.Count}";*/
                    spriteBatch.DrawString(gameFont, healthString, new Vector2(10, 10), Color.Black);
                }
                else
                {
                    spriteBatch.DrawString(gameFont, "Bullets go Brrr", new Vector2(300, 50), Color.Black);
                    spriteBatch.DrawString(gameFont, menuString, new Vector2(300, 100), Color.Black);
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
