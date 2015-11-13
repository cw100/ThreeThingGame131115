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
        
        List<Player> players;
        Animation playerHead
        , playerBody
        , playerArm
        , playerRunning
        , playerJump
        ,playerCrouch
        , playerWalking;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            

            base.Initialize();
           
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
          
            LoadPlayers();
            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
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
                playerRunning.LoadContent(this.Content, "StickRunning");
                playerBody.LoadContent(this.Content, "Stick");
                playerHead.LoadContent(this.Content, "StickHead");
                playerArm.LoadContent(this.Content, "StickArm");
                playerJump.LoadContent(this.Content, "StickJump");
                playerCrouch.LoadContent(this.Content, "StickLand");
                playerWalking.LoadContent(this.Content, "StickWalking");
                Player player = new Player();
                player.Initialize(playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                    new Vector2(200*i, 200), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)i);
                players.Add(player);
            }
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            foreach (Player plyer in players)
            {
                plyer.Update(gameTime);
            }
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
            foreach (Player plyer in players)
            {
                plyer.Draw(spriteBatch);
            }
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
