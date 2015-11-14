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
         static List<Animation> hats;
         static List<Vector2> hatPos;

         public static Animation playerHead
           , playerBody
           , playerArm
           , playerRunning
           , playerJump
           , playerCrouch
           , playerWalking
           , weaponAnimation
           , bulletAnimation
           , platformAnimation
           , pickupAnimation
           , backGroundAnimation
           , scoreAnimation,
           playButtonAnimation, exitButtonAnimation, coinsAnimation, clockAnimation, noteAnimation, piggybankAnimation;
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
        , healthTexture,
        playButtonTex
        ,exitButtonTex
        ,winningTex
        ,playerOneWin
        ,playerTwoWin,
        playerThreeWin,
        playerFourWin, startTex, noteTex,piggybankTex,coinsTex,clockTex,background;

      public enum GameState
      {
          Menu,
          Select,
          Playing,
          GameOver
      }
        public static List<Pickup> pickups;
        SpriteFont spriteFont;       
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
        /// 
        Animation selectIcon;
        List<Animation> selectIconsBody;
        List<Animation> selectIconsHead;
        List<Animation> selectHats;
        public void InitializeSelectScreen()
        {
           
            readyPlayers = 0;
            readyPlayerChecker = new List<bool>();
            readyPlayerChecker.Add(false);
            readyPlayerChecker.Add(false);
            readyPlayerChecker.Add(false);

            readyPlayerChecker.Add(false);
            selectHats = new List<Animation>();
            selectIconsBody = new List<Animation>();
            selectIconsHead = new List<Animation>();
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerBodyTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight * 2 / 3), 0, Color.White);
            selectIconsBody.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerBodyTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth*2 / 5, graphics.PreferredBackBufferHeight *2/ 3), 0, Color.White);
            selectIconsBody.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerBodyTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 3 / 5, graphics.PreferredBackBufferHeight * 2 / 3), 0, Color.White);
            selectIconsBody.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerBodyTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 4 / 5, graphics.PreferredBackBufferHeight * 2 / 3), 0, Color.White);
            selectIconsBody.Add(selectIcon);

            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerHeadTex.Height / 2 - playerBodyTex.Height / 2), 0, Color.White);
            selectIconsHead.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 2 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerHeadTex.Height / 2 - playerBodyTex.Height / 2), 0, Color.White);
            selectIconsHead.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 3 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerHeadTex.Height / 2 - playerBodyTex.Height / 2), 0, Color.White);
            selectIconsHead.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 4 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerHeadTex.Height / 2 - playerBodyTex.Height / 2), 0, Color.White);
            selectIconsHead.Add(selectIcon);

            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerBodyTex.Height / 2), 0, Color.White);
            selectHats.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 2 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerBodyTex.Height / 2), 0, Color.White);
            selectHats.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 3 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - playerBodyTex.Height / 2), 0, Color.White);
            selectHats.Add(selectIcon);
            selectIcon = new Animation();
            selectIcon.LoadTexture(playerHeadTex);
            selectIcon.Initialize(1, 1, new Vector2(graphics.PreferredBackBufferWidth * 4 / 5, graphics.PreferredBackBufferHeight * 2 / 3 - -playerBodyTex.Height / 2), 0, Color.White);
            selectHats.Add(selectIcon);

        }
        
        public void HatSelect(GameTime gameTime, int num)
        {
            
            if (0 <= playerHatNums[num ] && playerHatNums[num ] < hatTexs.Count)
            {
                menuTime = 1000;
                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime > menuTime)
                {
                    int plyerindex = num;
                    if ((int)(GamePad.GetState((PlayerIndex)(plyerindex)).ThumbSticks.Left.X) >= 1)
                    {
                    playerHatNums[num ] += 1;
                    
                    elapsedTime = 0;
                    }
                    if ((int)(GamePad.GetState((PlayerIndex)(plyerindex)).ThumbSticks.Left.X) <= -1)
                    {
                        playerHatNums[num] -= 1;

                        elapsedTime = 0;
                    }


                }

            }


            if (0 > playerHatNums[num])
            {
                playerHatNums[num] = 0;
                
            }
            if (playerHatNums[num] >= 13)
            {
                playerHatNums[num ] = 12;
            }
            selectIcon = new Animation();
            selectIcon = hatsHats[num][playerHatNums[num]];
            Vector2 headPos = new Vector2(graphics.PreferredBackBufferWidth * (num + 1) / 5   ,
                 graphics.PreferredBackBufferHeight * 2 / 3 - playerBodyTex.Height/2 - playerHeadTex.Height/2);
            selectIcon.Initialize(1, 1, headPos  - new Vector2(playerHeadTex.Width / 2, playerHeadTex.Height / 2) * 2 + hatPos[playerHatNums[num]], 0, Color.White);
           
            selectHats[num] = selectIcon;
            
        }
        int readyPlayers;
        List<bool> readyPlayerChecker;
        public void UpdateSelectScreen(GameTime gameTime)
        {
            foreach (Animation anmi in selectIconsHead)
            {
                anmi.Update(gameTime);
            }
            foreach (Animation anmi in selectIconsBody)
            {
                anmi.Update(gameTime);
            }
           
            foreach (Animation anmi in selectHats)
            {
                anmi.Update(gameTime);
                anmi.origin = new Vector2(0, 0);
            }
            for (int i = 0; i <4; i++)
            {
                if (!readyPlayerChecker[i])
                {
                    HatSelect(gameTime, i);
                }
                if (GamePad.GetState((PlayerIndex)i).Buttons.Start == ButtonState.Pressed && !readyPlayerChecker[i])
                {
                    readyPlayerChecker[i] = true;
                    readyPlayers += 1;
                }
                if (GamePad.GetState((PlayerIndex)i).Buttons.B == ButtonState.Pressed && readyPlayerChecker[i])
                {
                    readyPlayerChecker[i] = false;
                    readyPlayers -= 1;
                }
            }
            if (readyPlayers == 4)
            {
                StartGame();
                currentGameState = GameState.Playing;
            }
            else
                if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed)
                {

                    noPlayers = readyPlayers;
                    StartGame();
                    currentGameState = GameState.Playing;
                }


        }
        public void DrawIcons(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 4; i++)
            {
                if (!readyPlayerChecker[i])
                {
                    selectIconsBody[i].color = Color.White;
                    selectIconsBody[i].Draw(spriteBatch);
                    selectIconsHead[i].color = Color.White;
                    selectIconsHead[i].Draw(spriteBatch);

                    selectHats[i].origin = new Vector2(0, 0);
                    selectHats[i].color = Color.White;
                    selectHats[i].Draw(spriteBatch);
                    spriteBatch.Draw(startTex, new Vector2(graphics.PreferredBackBufferWidth * (i + 1) / 5 - 32,
                 graphics.PreferredBackBufferHeight * 2 / 3 + 64), Color.White);
                }
                else
                {
                    spriteBatch.Draw(startTex, new Vector2(graphics.PreferredBackBufferWidth * (i + 1) / 5 - 32,
                 graphics.PreferredBackBufferHeight * 2 / 3 + 64), Color.Green);
                    selectIconsBody[i].color = Color.Red;
                    selectIconsBody[i].Draw(spriteBatch);
                    selectIconsHead[i].color = Color.Red;
                    selectIconsHead[i].Draw(spriteBatch);
                    selectHats[i].origin = new Vector2(0, 0);
                    selectHats[i].color = Color.White;
                    selectHats[i].Draw(spriteBatch);

                }
               
            }
        }
        protected override void Initialize()
        {
           
            // TODO: Add your initialization logic here
            playerHatNums.Add(0);
            playerHatNums.Add(0);
            playerHatNums.Add(0);
            playerHatNums.Add(0);
            base.Initialize();
            if (currentGameState == GameState.Menu)
            {
                HatLoad();
                InitializeMainMenu();
            }
            if (currentGameState == GameState.Playing)
            {
                StartGame();
            }
        }
      public static List<Texture2D> hatTexs;
      public static Animation hat;
      
     public static List<int> playerHatNums = new List<int>();
      List<List<Animation>> hatsHats;
        public void HatLoad()
        {
         hatsHats = new List<List<Animation>>();
                 hatTexs = new List<Texture2D>() ;
                 hats = new List<Animation>();
                 hatPos = new List<Vector2>();
                 Texture2D hatTexture = LoadContent(this.Content, "ricehat");
                 hatTexs.Add(hatTexture);

                 hatPos.Add(new Vector2(8, 10));
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatTexture = LoadContent(this.Content, "boater");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(15, 8));
                 hatTexture = LoadContent(this.Content, "chefhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 1));
                 hatTexture = LoadContent(this.Content, "christmashat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 5));
                 hatTexture = LoadContent(this.Content, "dmed");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 6));
                 hatTexture = LoadContent(this.Content, "elfhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 3));
                 hatTexture = LoadContent(this.Content, "fez");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(19, 3));
                 hatTexture = LoadContent(this.Content, "vikinghat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 4));

                 hatTexture = LoadContent(this.Content, "pilgrimmhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 4));

                 hatTexture = LoadContent(this.Content, "piratehat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(11, 7));

                 hatTexture = LoadContent(this.Content, "russianhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 11));
                 hatTexture = LoadContent(this.Content, "stetson");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(12, 0));
                 hatTexture = LoadContent(this.Content, "wizardhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(17, 2));
                 hatsHats.Add(hats);

                 hatTexs = new List<Texture2D>();
                 hats = new List<Animation>();
                 hatPos = new List<Vector2>();
                 hatTexture = LoadContent(this.Content, "ricehat");
                 hatTexs.Add(hatTexture);

                 hatPos.Add(new Vector2(8, 10));
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatTexture = LoadContent(this.Content, "boater");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(15, 8));
                 hatTexture = LoadContent(this.Content, "chefhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 1));
                 hatTexture = LoadContent(this.Content, "christmashat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 5));
                 hatTexture = LoadContent(this.Content, "dmed");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 6));
                 hatTexture = LoadContent(this.Content, "elfhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 3));
                 hatTexture = LoadContent(this.Content, "fez");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(19, 3));
                 hatTexture = LoadContent(this.Content, "vikinghat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 4));

                 hatTexture = LoadContent(this.Content, "pilgrimmhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 4));

                 hatTexture = LoadContent(this.Content, "piratehat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(11, 7));

                 hatTexture = LoadContent(this.Content, "russianhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 11));
                 hatTexture = LoadContent(this.Content, "stetson");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(12, 0));
                 hatTexture = LoadContent(this.Content, "wizardhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(17, 2));
                 hatsHats.Add(hats);

                 hatTexs.Add(hatTexture);
                 hatTexs = new List<Texture2D>();
                 hats = new List<Animation>();
                 hatPos = new List<Vector2>();
                  hatTexture = LoadContent(this.Content, "ricehat");
                 hatTexs.Add(hatTexture);

                 hatPos.Add(new Vector2(8, 10));
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
              
                 hatTexture = LoadContent(this.Content, "boater");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(15, 8));
                 hatTexture = LoadContent(this.Content, "chefhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 1));
                 hatTexture = LoadContent(this.Content, "christmashat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 5));
                 hatTexture = LoadContent(this.Content, "dmed");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 6));
                 hatTexture = LoadContent(this.Content, "elfhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 3));
                 hatTexture = LoadContent(this.Content, "fez");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(19, 3));
                 hatTexture = LoadContent(this.Content, "vikinghat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 4));

                 hatTexture = LoadContent(this.Content, "pilgrimmhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 4));

                 hatTexture = LoadContent(this.Content, "piratehat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(11, 7));

                 hatTexture = LoadContent(this.Content, "russianhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 11));
                 hatTexture = LoadContent(this.Content, "stetson");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(12, 0));
                 hatTexture = LoadContent(this.Content, "wizardhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(17, 2));

                 hatsHats.Add(hats);
                 hatTexs.Add(hatTexture);
                 hatTexs = new List<Texture2D>();
                 hats = new List<Animation>();
                 hatPos = new List<Vector2>();
                 hatTexture = LoadContent(this.Content, "ricehat");
                 hatTexs.Add(hatTexture);
                 hatPos.Add(new Vector2(8, 10));

                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatTexture = LoadContent(this.Content, "boater");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(15, 8));
                 hatTexture = LoadContent(this.Content, "chefhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 1));
                 hatTexture = LoadContent(this.Content, "christmashat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 5));
                 hatTexture = LoadContent(this.Content, "dmed");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(14, 6));
                 hatTexture = LoadContent(this.Content, "elfhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 3));
                 hatTexture = LoadContent(this.Content, "fez");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(19, 3));
                 hatTexture = LoadContent(this.Content, "vikinghat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 4));

                 hatTexture = LoadContent(this.Content, "pilgrimmhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(18, 4));

                 hatTexture = LoadContent(this.Content, "piratehat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(11, 7));

                 hatTexture = LoadContent(this.Content, "russianhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(13, 11));
                 hatTexture = LoadContent(this.Content, "stetson");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(12, 0));
                 hatTexture = LoadContent(this.Content, "wizardhat");
                 hatTexs.Add(hatTexture);
                 hat = new Animation();
                 hat.LoadTexture(hatTexture);
                 hats.Add(hat);
                 hatPos.Add(new Vector2(17, 2));

                 hatsHats.Add(hats);


        }
        public void StartGame()
        {
            HatLoad();
            currentGameLength = TimeSpan.FromSeconds(0);
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
       static Random random = new Random();
        public void AddPickup()
        {
            Pickup pickup = new Pickup();
            pickupAnimation = new Animation();
            switch (random.Next(1, 5))
            {
                case 1:
                    pickupTex = clockTex;
                    break;
                case 2:
                    pickupTex = noteTex;
                    break;
                case 3:
                    pickupTex = coinsTex;
                    break;
                case 4:
                    pickupTex = piggybankTex;
                    break;
            }
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
            weapon.Initialize(weaponAnimation, bulletTex, new Vector2(0, 0), new Vector2(10, 0), new Vector2(6, 14), 0, true, 1f, TimeSpan.FromSeconds(0.3), 10, (PlayerIndex)playerNum);
             
            players[playerNum] = new Player();
            players[playerNum].Initialize(healthTexture, playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                   new Vector2(random.Next(1920), random.Next(1080)), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)playerNum, score, hatTexs[playerHatNums[playerNum]], hatPos[playerHatNums[playerNum]]);
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
            background = LoadContent(this.Content, "background");
            weaponTex = LoadContent(this.Content, "Pistol");
            bulletTex = LoadContent(this.Content, "Bullet");
            platformTex = LoadContent(this.Content, "platform");
            pickupTex = LoadContent(this.Content, "Good");
            backGroundTex = LoadContent(this.Content, "BackGround1");
            scoreTex = LoadContent(this.Content, "Scoreboard");
            healthTexture = LoadContent(this.Content, "healthbar");
            spriteFont = Content.Load<SpriteFont>("Font");
            playButtonTex = LoadContent(this.Content, "playButton");

            exitButtonTex = LoadContent(this.Content, "exitButton");
            playerOneWin = LoadContent(this.Content, "playerOneWin");

            playerTwoWin = LoadContent(this.Content, "playerTwoWin");

            playerThreeWin = LoadContent(this.Content, "playerThreeWin");

            playerFourWin = LoadContent(this.Content, "playerFourWin");
            startTex = LoadContent(this.Content, "start");
            noteTex = LoadContent(this.Content, "note");
            coinsTex = LoadContent(this.Content, "coins");
            piggybankTex = LoadContent(this.Content, "piggybank");
            clockTex = LoadContent(this.Content, "clock"); 
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
        int noPlayers = 4;
        public void LoadPlayers()
        {
            players = new List<Player>();
            for (int i = 0; i < noPlayers; i++)
            {     playerBody = new Animation();
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
               weapon.Initialize(weaponAnimation, bulletTex, new Vector2(0, 0), new Vector2(10, 0), new Vector2(6, 14), 0, true, 1f, TimeSpan.FromSeconds(0.3), 10, (PlayerIndex)i);
                Player player = new Player();
                player.Initialize(healthTexture,playerBody, playerRunning, playerWalking, playerCrouch, playerJump, playerHead, playerArm,
                  new Vector2(random.Next(1920), random.Next(1080)), graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height,
                    new Vector2(0, 40f), 200, new Vector2(0, 15), (PlayerIndex)i, 0, hatTexs[playerHatNums[i]], hatPos[playerHatNums[i]]);
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
      
        List<Button> menuButtons;
        GameState currentGameState = GameState.Menu;
        public void InitializeMainMenu()
        {
            playButtonAnimation = new Animation();
            playButtonAnimation.LoadTexture(playButtonTex);
            exitButtonAnimation = new Animation();
            exitButtonAnimation.LoadTexture(exitButtonTex);
            menuButtons = new List<Button>();

            
            Button button = new Button();
            button.Initialize(new Vector2((1920) / 2, 500 + 140 / 2), playButtonAnimation, "play", 1);
            menuButtons.Add(button);

            button = new Button();
            button.Initialize(new Vector2((1920) / 2, 700 + 140 / 2), exitButtonAnimation, "exit", 2);
            menuButtons.Add(button);
        }
        public void UpdateMenu(GameTime gameTime)
        {
            MenuSelect(gameTime, PlayerIndex.One);
            foreach (Button button in menuButtons)
            {
                button.Update(gameTime, currentMenuItem);
                if (button.CheckForClick())
                {

                    if (button.buttonName == "play")
                    {
                        InitializeSelectScreen();
                        currentGameState = GameState.Select;

                    }
                    if (button.buttonName == "exit")
                    {
                        Exit();
                    }
                }
            }

        }
        int currentMenuItem = 1;
        float elapsedTime;
        float menuTime = 50; 
        public void MenuSelect(GameTime gameTime, PlayerIndex num)
        {
            if (1 <= currentMenuItem && currentMenuItem <= menuButtons.Count)
            {

                elapsedTime += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedTime > menuTime)
                {
                    currentMenuItem -= (int)(GamePad.GetState(num).ThumbSticks.Left.Y);
                    elapsedTime = 0;
                }

            }


            if (1 > currentMenuItem)
            {
                currentMenuItem = 1;
            }
            if (currentMenuItem > menuButtons.Count)
            {
                currentMenuItem = menuButtons.Count;
            }
        }
        public void DrawMenu()
        {
            foreach (Button button in menuButtons)
            {
                button.Draw(spriteBatch);
            }

        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(currentGameState == GameState.Menu)
            {
                UpdateMenu(gameTime);
            }
            if (currentGameState == GameState.Playing)
            {
                currentGameLength += TimeSpan.FromSeconds(gameTime.ElapsedGameTime.TotalSeconds);
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
                GameOverCheck();
                // TODO: Add your update logic here
            }
            if(currentGameState ==  GameState.GameOver)
            {
                UpdateGameOverScreen();
            }
            if (currentGameState == GameState.Select)
            {
                UpdateSelectScreen( gameTime);
        

            }

            base.Update(gameTime);
        }
        TimeSpan currentGameLength;
        int winningPlayer;
        int lastScore;
        public void UpdateGameOverScreen()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed)
            {
                currentGameState = GameState.Menu;
                InitializeMainMenu();
            }


        }
        public void DrawGameOver()
        {

            spriteBatch.Draw(winningTex, new Vector2((1920 - winningTex.Width) / 2, (1080 - winningTex.Height) / 2), Color.Green);

        }
        public void GameOverCheck()
        {
            lastScore= 0;
            if (gameLength <=currentGameLength)
            {
                foreach (Player player in players)
                {
                    if(player.score> lastScore)
                    {
                        lastScore =player.score;
                        winningPlayer = (int)player.playerNumber;
                        switch (winningPlayer)
                        {
                            case (int)PlayerIndex.One:
                                winningTex = playerOneWin;
                                break;
                            case (int)PlayerIndex.Two:
                                winningTex = playerTwoWin;
                                break;
                            case (int)PlayerIndex.Three:
                                winningTex = playerThreeWin;
                                break;
                            case (int)PlayerIndex.Four:
                                winningTex = playerFourWin;
                                break;
                        }
                }
                      
                }
                currentGameState = GameState.GameOver;

            }
        }
      
        string output;
        Vector2 FontOrigin;
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            if(currentGameState == GameState.GameOver)
            {
                DrawGameOver();
            }
            if(currentGameState == GameState.Menu)
            {
                spriteBatch.Draw(background, new Vector2(0 ,0),Color.White);
                
                DrawMenu();
            }
            if (currentGameState == GameState.Select)
            {
                spriteBatch.Draw(background, new Vector2(0, 0), Color.White);
                
                DrawIcons(spriteBatch);
            }
            if (currentGameState == GameState.Playing)
            {
                backGroundAnimation.Draw(spriteBatch);
                scoreAnimation.Draw(spriteBatch);
                for (int i = 0; i < players.Count; i++)
                {
                    output = "Player " + players[i].playerNumber.ToString() +" " +  players[i].score.ToString();
                     FontOrigin = spriteFont.MeasureString(output) / 2;
                    spriteBatch.DrawString(spriteFont, output, new Vector2(200 * (i + 3), 30), Color.Black,
                                  0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                }

                  output = currentGameLength.ToString();
                 FontOrigin = spriteFont.MeasureString(output) / 2;
                    spriteBatch.DrawString(spriteFont, output, new Vector2(1500, 30), Color.Black,
                                  0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
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
