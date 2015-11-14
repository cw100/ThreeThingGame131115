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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
       public static List<Projectile> projectiles;
        List<Player> players;
      
        Animation playerHead
        , playerBody
        , playerArm
        , playerRunning
        , playerJump
        ,playerCrouch
        , playerWalking
        ,weaponAnimation
        , bulletAnimation
        ,platformAnimation;
        Texture2D
            playerHeadTex
        , playerBodyTex
        , playerArmTex
        , playerRunningTex
        , playerJumpTex
        , playerCrouchTex
        , playerWalkingTex
        ,weaponTex
        ,bulletTex
        ,platformTex;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = true;
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
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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
            base.Initialize();
            LoadPlayers();
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
        public void AddPlatform()
        {
            platformRectangles = new List<Rectangle>();
            platforms = new List<Platform>();
            Platform platform = new Platform();
            platform.Initialize(new Vector2(0, 500));
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
                player.Initialize(playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                    new Vector2(200*i, 200), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)i);
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

                           
                            players[i].health -=  projectiles[j].damage;
                            projectiles.RemoveAt(j);
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
                        players[i].health -= projectiles[j].damage;
                        projectiles.RemoveAt(j);
                    }
                }
            }

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (Player plyer in players)
            {
                plyer.Update(gameTime);
            }
            foreach(Projectile projectile in projectiles)
            {
                projectile.Update(gameTime);
            }
            foreach( Platform platform in platforms)
            {
                platform.Update(gameTime);
            }
            BulletPlayerCollision();
            // TODO: Add your update logic here

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
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(spriteBatch);
            }
            foreach (Player plyer in players)
            {
                plyer.Draw(spriteBatch);
            }
          
            foreach (Platform platform in platforms)
            {
                platform.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
