#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace ThreeThingGame131115
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       public static List<Projectile> projectiles;
       public static List<Player> players;
        bool gameActive;
        
      public static Animation playerHead
        , playerBody
        , playerArm
        , playerRunning
        , playerJump
        ,playerCrouch
        , playerWalking
        ,weaponAnimation
        , bulletAnimation
        ,platformAnimation
        ,pickupAnimation
        ,backGroundAnimation
        ,scoreAnimation;
      public static Texture2D
            playerHeadTex
        , playerBodyTex
        , playerArmTex
        , playerRunningTex
        , playerJumpTex
        , playerCrouchTex
        , playerWalkingTex
        ,weaponTex
        ,bulletTex
        ,platformTex
        ,pickupTex
        , backGroundTex
        ,scoreTex
        , healthTexture;
      

        public static List<Pickup> pickups;
        SpriteFont spriteFont;       
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 1080;

            graphics.PreferredBackBufferWidth = 1920;
        }
        public static Texture2D LoadContent(ContentManager content, String texString)
        {
           return content.Load<Texture2D>(texString);
        }
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        protected override void Initialize()
        {
           
            // TODO: Add your initialization logic here

            base.Initialize();
            StartGame();
        }
        public void StartGame()
        {
            projectiles = new List<Projectile>();
            playerBody = new Animation();
            playerHead = new Animation();
            playerArm = new Animation();
            playerRunning = new Animation();
            playerJump = new Animation();
            playerWalking = new Animation();
            playerCrouch = new Animation();
            weaponAnimation = new Animation();
            bulletAnimation = new Animation();
            platformAnimation = new Animation();
            backGroundAnimation = new Animation();
            scoreAnimation = new Animation();
            platformRectangles = new List<Rectangle>();
            pickups = new List<Pickup>();
            stairRectangles = new List<Rectangle>();
            scoreAnimation.LoadTexture(scoreTex);
            backGroundAnimation.LoadTexture(backGroundTex);
            scoreAnimation.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth / 2, 0), 0, Color.White);
           
            backGroundAnimation.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth / 2, graphics.PreferredBackBufferHeight / 2), 0, Color.White);
            LoadPlayers();
            AddPlatform();
            AddStairs();
        }
        Random random = new Random();
        public void AddPickup()
        {
            Pickup pickup = new Pickup();
            pickupAnimation = new Animation();
            pickupAnimation.LoadTexture(pickupTex);
            pickup.Initialize(pickupAnimation, new Vector2(random.Next(1920), random.Next(1080)), new Vector2(0, 40f),new Vector2 (0,0), 10);
            pickups.Add(pickup);
        }
        public static void AddPickupPlayerDeath(float x,float y, Vector2 vel,Vector2 vel2)
        {
            Pickup pickup = new Pickup();
            pickupAnimation = new Animation();
            pickupAnimation.LoadTexture(pickupTex);
            pickup.Initialize(pickupAnimation, new Vector2(x, y), vel, vel2, 10);
       
            pickups.Add(pickup);
        }
        public static void RespawnPlayer(int playerNum)
        {
            int score = players[playerNum].score;
            playerBody = new Animation();
            playerHead = new Animation();
            playerArm = new Animation();
            playerRunning = new Animation();
            playerJump = new Animation();
            playerWalking = new Animation();
            playerCrouch = new Animation();
            weaponAnimation = new Animation();
            bulletAnimation = new Animation();
            playerBody.LoadTexture(playerBodyTex);
            playerRunning.LoadTexture(playerRunningTex);
            playerHead.LoadTexture(playerHeadTex);
            playerArm.LoadTexture(playerArmTex);
            playerJump.LoadTexture(playerJumpTex);
            playerCrouch.LoadTexture(playerCrouchTex);
            playerWalking.LoadTexture(playerWalkingTex);
            weaponAnimation.LoadTexture(weaponTex);
            Weapon weapon = new Weapon();
            weapon.Initialize(weaponAnimation, bulletTex, new Vector2(0, 0), new Vector2(25, 0), new Vector2(4, 14), 0, true, 1f, TimeSpan.FromSeconds(0.05), 10, (PlayerIndex)playerNum);
            players[playerNum] = new Player();
            players[playerNum].Initialize(healthTexture, playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                    new Vector2(200 * playerNum, 200), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)playerNum, score);
            players[playerNum].activeWeapon = weapon;
        }
            
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            playerRunningTex = LoadContent(this.Content, "StickRunning");
            playerBodyTex = LoadContent(this.Content, "Stick");
            playerHeadTex = LoadContent(this.Content, "StickHead");
            playerArmTex = LoadContent(this.Content, "StickArm");
            playerJumpTex = LoadContent(this.Content, "StickJump");
            playerCrouchTex = LoadContent(this.Content, "StickLand");
            playerWalkingTex = LoadContent(this.Content, "StickWalking");
            weaponTex = LoadContent(this.Content, "gun");
            bulletTex = LoadContent(this.Content, "Bullet");
            platformTex = LoadContent(this.Content, "platform");
            pickupTex = LoadContent(this.Content, "Good");
            backGroundTex = LoadContent(this.Content, "BackGround1");
            scoreTex = LoadContent(this.Content, "Scoreboard");
            healthTexture = LoadContent(this.Content, "healthbar");
            spriteFont = Content.Load<SpriteFont>("Font");        

        }
        

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        List<Platform> platforms;
        List<Platform> stairs;
        public static List<Rectangle> stairRectangles;
       
        
        public void AddStairs()
        {
            stairs = new List<Platform>();
            for (int i = 0; i < 12; i++)
            {
                Platform stair = new Platform();

                stair.Initialize(new Vector2(855 + i * 35, 1040 - i * 25), 35, 25);
                stairs.Add(stair);
                stairRectangles.Add(stair.hitBox);
            }
            for (int i = 0; i < 12; i++)
            {
                Platform stair = new Platform();

                stair.Initialize(new Vector2(1465 + i * 35, 719 - i * 25), 35, 30);
                stairs.Add(stair);
                stairRectangles.Add(stair.hitBox);
            }

        }
        public void AddPlatform()
        {
           
            platforms = new List<Platform>();
            Platform platform = new Platform();

            platform.Initialize(new Vector2(196, 3025),197,30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

             platform = new Platform();

            platform.Initialize(new Vector2(72, 943), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(313, 943), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(816, 938), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(1534, 944), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(1212,646), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(1586, 981), 337, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(188, 601),325, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(787, 573), 156, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(816, 512), 100, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(1268, 320), 80, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(-10, 369), 349, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(-10, 288), 363, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(1534, 371), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(-10, 765), 2100, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);

            platform = new Platform();

            platform.Initialize(new Vector2(-10, 448), 2100, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);
            platform = new Platform();

            platform.Initialize(new Vector2(861, 371), 200, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);
            platform = new Platform();

            platform.Initialize(new Vector2(-10, 1070), 2100, 30);
            platforms.Add(platform);
            platformRectangles.Add(platform.hitBox);
        }
        public void LoadPlayers()
        {
            players = new List<Player>();
            for (int i = 0; i < 4; i++)
            {
                playerBody = new Animation();
                playerHead = new Animation();
                playerArm = new Animation();
                playerRunning = new Animation();
                playerJump = new Animation();
                playerWalking = new Animation();
                playerCrouch = new Animation();
                weaponAnimation = new Animation();
                bulletAnimation = new Animation();
                playerBody.LoadTexture(playerBodyTex);
                playerRunning.LoadTexture(playerRunningTex);
                playerHead.LoadTexture(playerHeadTex);
                playerArm.LoadTexture(playerArmTex);
                playerJump.LoadTexture(playerJumpTex);
                playerCrouch.LoadTexture(playerCrouchTex);
                playerWalking.LoadTexture(playerWalkingTex);
                weaponAnimation.LoadTexture(weaponTex);
                
               Weapon weapon = new Weapon();
               weapon.Initialize(weaponAnimation, bulletTex, new Vector2(0, 0), new Vector2(25, 0),new Vector2(4,14), 0, true, 1f, TimeSpan.FromSeconds(0.05), 10, (PlayerIndex)i);
                Player player = new Player();
                player.Initialize(healthTexture,playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                    new Vector2(200*i, 200), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)i,0);
                player.activeWeapon = weapon;
                players.Add(player);
            }
        }
       public static List<Rectangle> platformRectangles;
       
        
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
       public void BulletPlayerCollision()
        {

            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if (players[i].mainHitbox.Intersects(projectiles[j].hitBox))
                    {
                        if (Collision.CollidesWith(players[i].activeAnimation,
                        projectiles[j].projectileAnimation, players[i].playerTransformation,
                        projectiles[j].projectileTransformation))
                        {
                            if (players[i].active)
                            {


                                players[i].health -= projectiles[j].damage;
                                projectiles.RemoveAt(j);
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if (players[i].headHitbox.Intersects(projectiles[j].hitBox))
                    {
                        if (Collision.CollidesWith(players[i].playerHead,
                        projectiles[j].projectileAnimation, players[i].headTransformation,
                        projectiles[j].projectileTransformation))
                        {
                            if (players[i].active)
                            {


                            players[i].health -= projectiles[j].damage*2;
                            projectiles.RemoveAt(j);
}
                        }
                    }
                }
            }
            for (int i = 0; i < players.Count; i++)
            {
                for (int j = 0; j < projectiles.Count; j++)
                {
                    if (players[i].mainHitbox.Intersects(projectiles[j].hitBox)&&(projectiles[j].prePosition.X < players[i].position.X && projectiles[j].position.X > players[i].position.X || 
                        projectiles[j].prePosition.X > players[i].position.X && projectiles[j].position.X < players[i].position.X ||
                        projectiles[j].position.X > players[i].prePosition.X && projectiles[j].position.X < players[i].position.X ||
                        projectiles[j].position.X < players[i].prePosition.X && projectiles[j].position.X > players[i].position.X))
                    {
                        if (players[i].active)
                        {

                            players[i].health -= projectiles[j].damage;
                            projectiles.RemoveAt(j);
                        }
                    }
                }
            }

        }
          public void PickupPlayerCollision()
       {

           for (int i = 0; i < players.Count; i++)
           {
               for (int j = 0; j < pickups.Count; j++)
               {
                   if (players[i].mainHitbox.Intersects(pickups[j].hitBox))
                   {
                       if (players[i].active)
                       {

                           players[i].score += pickups[j].score;
                           pickups.RemoveAt(j);
                       }
                   }

               }
           }
       }
       TimeSpan previousSpawnTime;
       TimeSpan spawnRate = TimeSpan.FromSeconds(1);
       TimeSpan gameLength = TimeSpan.FromSeconds(60);
        public enum GameState
        {
            Menu,
            Select,
            Playing
        }
        GameState curentGameState = GameState.Playing;
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (curentGameState == GameState.Playing)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].Update(gameTime);
                }
                foreach (Projectile projectile in projectiles)
                {
                    projectile.Update(gameTime);
                }

                if (gameTime.TotalGameTime - previousSpawnTime > spawnRate)
                {
                    previousSpawnTime = gameTime.TotalGameTime;
                    AddPickup();
                }
                foreach (Pickup pickup in pickups)
                {
                    pickup.Update(gameTime, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
                }
                backGroundAnimation.Update(gameTime);
                scoreAnimation.Update(gameTime);
                BulletPlayerCollision();
                PickupPlayerCollision();
                // TODO: Add your update logic here
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if (curentGameState == GameState.Playing)
            {
                backGroundAnimation.Draw(spriteBatch);
                scoreAnimation.Draw(spriteBatch);
                for (int i = 0; i < players.Count; i++)
                {
                    string output = players[i].score.ToString();
                    Vector2 FontOrigin = spriteFont.MeasureString(output) / 2;
                    spriteBatch.DrawString(spriteFont, output, new Vector2(100 * (i + 3), 30), Color.Black,
                                  0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                }
                foreach (Projectile projectile in projectiles)
                {
                    projectile.Draw(spriteBatch);
                }
                for (int i = 0; i < players.Count; i++)
                {
                    players[i].Draw(spriteBatch);
                }


                foreach (Pickup pickup in pickups)
                {
                    pickup.Draw(spriteBatch);
                }
            }

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
