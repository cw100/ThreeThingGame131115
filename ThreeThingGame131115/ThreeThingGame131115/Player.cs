using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ThreeThingGame131115
{
    public class Player
    {
        public bool ready = false;
        public int score;
        Vector2 weaponPosition;
        public float health;
        public Animation hatAnimation;
        public Vector2 hatPos;
        public Animation activeAnimation;
        Animation playerRunning;
        Animation playerBody;
        public Animation playerHead;
        Animation playerArm;
        Animation playerJump;
        Animation playerCrouch;
        Animation playerWalking;
        public Vector2 position, prePosition;
        Vector2 gravity;
        Vector2 headPosition;
        Vector2 armPosition;
        public Vector2 velocity;
        Vector2 preJumpPosition;
        Vector2 jumpSpeed;
        public Rectangle mainHitbox, headHitbox, footBox;
        Vector2 armAngleVector;
        float armAngle;
        float windowWidth, windowHeight;
        public PlayerIndex playerNumber;
        bool flipped;
        float playerSpeed;
        GamePadState gamePadState;
        Vector2 stickInputRight, stickInputLeft;
        List<float> jumpList = new List<float>();
        public Weapon activeWeapon;
      public  bool active;
        public enum State
        {
            Jumping,
            Falling,
            OnGround
        }
        public enum MoveState
        {
            Standing,
            Running,
            Crouch,
            Walking
        }
        Texture2D healthtexture;
        State currentState = State.OnGround;
        MoveState currentMoveState = MoveState.Standing;
        public void Initialize(Texture2D healthtexture,Animation playerBody, Animation playerRunning, Animation playerWalking, Animation playerCrouch, Animation playerJump, Animation playerHead, Animation playerArm, Vector2 position, float windowWidth, float windowHeight, Vector2 gravity,
          float playerSpeed, Vector2 jumpSpeed, PlayerIndex playerNumber, int score,Texture2D hatTex, Vector2 hatPos )
        {
            this.hatAnimation =new Animation();
            this.healthtexture = healthtexture;
            this.hatPos = hatPos;
            State currentState = State.OnGround;
            MoveState currentMoveState = MoveState.Standing;
            active = true;
            this.score = score;
            health = 100;
            this.playerWalking = playerWalking;
            this.playerCrouch = playerCrouch;
            this.playerJump = playerJump;
            this.playerRunning = playerRunning;
            this.jumpSpeed = jumpSpeed;
            this.gravity = gravity;
            this.playerNumber = playerNumber;
            this.playerSpeed = playerSpeed;
            this.playerBody = playerBody;
            this.playerHead = playerHead;
            this.playerArm = playerArm;
            this.position = position;
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;
            headPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight + playerHead.frameHeight) / 2);
            armPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight) / 2);
            playerBody.Initialize(1, 1, position, 0, Color.White);
            playerWalking.Initialize(20, 0.5f, position, 0, Color.White);
            playerRunning.Initialize(20, 0.4f, position, 0, Color.White);
            playerJump.Initialize(true, 5,0.2f, position, 0, Color.White);
            playerCrouch.Initialize(true, 5, 0.2f, position, 0, Color.White);
            playerRunning.active = false;
            playerCrouch.active = false;
            playerJump.active = false;
            playerWalking.active = false;
            playerHead.Initialize(1, 1, headPosition, 0, Color.White);
            playerArm.Initialize(1, 1, armPosition, 0, Color.White);
            mainHitbox = new Rectangle((int)position.X - playerBody.frameWidth / 2, (int)position.Y - playerBody.frameHeight / 2, playerBody.frameWidth, playerBody.frameHeight);
            headHitbox = new Rectangle((int)headPosition.X - playerHead.frameWidth / 2, (int)headPosition.Y - playerHead.frameHeight / 2, playerHead.frameWidth, playerHead.frameHeight);
            footBox = new Rectangle((int)position.X - playerBody.frameWidth / 2, (int)position.Y + playerBody.frameHeight / 2 -10, playerBody.frameWidth, 10);
            jumpList.Add(26);
            jumpList.Add(22);
            jumpList.Add(16);
            jumpList.Add(9);
            jumpList.Add(0);
            activeAnimation = playerBody;
            hatAnimation.LoadTexture(hatTex);
            hatAnimation.Initialize(1, 1, new Vector2(0, 0), 0, Color.White);
          
        }
        public void GetInputRight()
        {
          
            float deadzone = 0.25f;
            stickInputRight = new Vector2(gamePadState.ThumbSticks.Right.X, gamePadState.ThumbSticks.Right.Y);
              if (stickInputRight.Length() < deadzone)
                {
                    stickInputRight = Vector2.Zero;
                }
                 else
                {
                    Vector2 normalizedInput = stickInputRight;
                    normalizedInput.Normalize();
                    stickInputRight = normalizedInput * ((stickInputRight.Length() - deadzone) / (1 - deadzone));
                }
        }
        public void GetInputLeft()
        {
           
            float deadzone = 0.25f;
            stickInputLeft = new Vector2(gamePadState.ThumbSticks.Left.X, gamePadState.ThumbSticks.Left.Y);
            if (stickInputLeft.Length() < deadzone)
            {
                stickInputLeft = Vector2.Zero;
            }
            else
            {
                Vector2 normalizedInput = stickInputLeft;
                normalizedInput.Normalize();
                stickInputLeft = normalizedInput * ((stickInputLeft.Length() - deadzone) / (1 - deadzone));
            }
        }
        public void DirectionCheck()
        {
            if (stickInputRight.X != 0)
            {
                if (stickInputRight.X < 0)
                {
                    playerWalking.flip = SpriteEffects.FlipHorizontally;                   
                    playerArm.flip = SpriteEffects.FlipHorizontally;                   
                    playerHead.flip = SpriteEffects.FlipHorizontally;
                    playerBody.flip = SpriteEffects.FlipHorizontally;
                    playerRunning.flip = SpriteEffects.FlipHorizontally;
                    playerJump.flip = SpriteEffects.FlipHorizontally;
                    playerCrouch.flip = SpriteEffects.FlipHorizontally;
                    if (activeWeapon != null)
                    {
                        activeWeapon.weaponAnimation.flip = SpriteEffects.FlipHorizontally;
                    }
                    activeWeapon.flipped = true;
                    flipped = true;
                }
                else
                {
                    if (activeWeapon != null)
                    {
                        activeWeapon.weaponAnimation.flip = SpriteEffects.None;
                    }
                    playerWalking.flip = SpriteEffects.None;
                    playerCrouch.flip = SpriteEffects.None;
                    playerJump.flip = SpriteEffects.None;
                    playerRunning.flip = SpriteEffects.None;
                    playerHead.flip = SpriteEffects.None;
                    playerArm.flip = SpriteEffects.None;
                    playerBody.flip = SpriteEffects.None;
                    activeWeapon.flipped = false;
                    flipped = false;
                }
            }

        }
       
        public void GetAngle()
        {

         
            if (stickInputRight.X != 0 || stickInputRight.Y != 0)
            {
                armAngleVector = stickInputRight;
               
                if (flipped)
                {
                    armAngle = (float)(Math.Atan2(armAngleVector.X, armAngleVector.Y) + Math.PI / 2);
                }
                else
                {
                    armAngle = (float)(Math.Atan2(armAngleVector.X, armAngleVector.Y) - Math.PI / 2);
                }
                armAngle = MathHelper.Clamp(armAngle, -1, 1); 
            }
        }
           public void ControllerMove(GameTime gameTime)
        {

            velocity.X += stickInputLeft.X * playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity.X = MathHelper.Clamp(velocity.X, 4 * -playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds, 4 * playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            if (velocity.X > (2.5f*playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds)|| velocity.X < -(2.5f*playerSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds) )
            {
                currentMoveState = MoveState.Running;
            } 
            else
                if (stickInputLeft.X != 0)
               {
                   currentMoveState = MoveState.Walking;
               }
               else
               {
                   currentMoveState = MoveState.Standing;
               }


        }
           public void ApplyFriction(GameTime gameTime)
           {
               velocity.X *= 0.8f;
           }
           public void ApplyGravity(GameTime gameTime)
           {

               velocity.Y += gravity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
               velocity.Y = MathHelper.Clamp(velocity.Y, -1000, gravity.Y);
           }
           public void Jump(GameTime gameTime)
           {
               if (currentState != State.Jumping && currentState != State.Falling)
               {
                   if (gamePadState.IsButtonDown(Buttons.A))
                   {

                       preJumpPosition = position;
                       currentState = State.Jumping;
                       velocity.Y = -jumpSpeed.Y;
                       velocity.Y = MathHelper.Clamp(velocity.Y, -1000, gravity.Y);
                   }

               }
               if (currentState == State.Jumping)
               {

                   if (velocity.Y>0)
                   {

                       currentState = State.Falling;
                   }

               }
           }
           public void StateManager()
           {
               if (position.Y + (playerBody.frameHeight / 2) >= windowHeight)
               {
                   currentState = State.OnGround;
               }
               
           }
           Vector2 Rotate(Vector2 vector, float angle, Vector2 origin)
           {
               vector.X = (float)Math.Cos(angle) * (vector.X - origin.X) - (float)Math.Cos(angle) * (vector.Y - origin.Y) + origin.X;
               vector.Y = (float)Math.Sin(angle) * (vector.X - origin.X) + (float)Math.Sin(angle) * (vector.Y - origin.Y) + origin.Y;
               return new Vector2(vector.X, vector.Y);
           }
        public void PlatformCollision()
           {
               foreach (Rectangle platform in Game1.platformRectangles)
               {

                   if (currentMoveState != MoveState.Crouch)
                   {
                       if (Collision.RectangleCollisionTop(mainHitbox, platform, velocity))
                       {
                           position.Y = (platform.Top - activeAnimation.frameHeight / 2);
                           velocity.Y = 0;
                           mainHitbox.X = (int)(position.X - activeAnimation.frameWidth / 2);
                           mainHitbox.Y = (int)(position.Y - activeAnimation.frameHeight / 2);
                           if (currentState != State.Jumping || currentState != State.Falling)
                           {
                               currentState = State.OnGround;
                               currentMoveState = MoveState.Standing;
                           }
                       }
                   }
                   



               }
           }
        public void StairCollision()
        {
            foreach (Rectangle platform in Game1.stairRectangles)
            {
                if (currentMoveState != MoveState.Crouch)
                {
                    if (footBox.Left < platform.Right)
                    {
                        if (Collision.RectangleCollisionLeft(footBox, platform, velocity))
                        {
                            position.Y = (platform.Top - activeAnimation.frameHeight / 2);
                            footBox.X = (int)(position.X - activeAnimation.frameWidth / 2);
                            footBox.Y = (int)(position.Y - activeAnimation.frameHeight / 2);
                        }

                    }

                    if (Collision.RectangleCollisionTop(mainHitbox, platform, velocity))
                    {
                        position.Y = (platform.Top - activeAnimation.frameHeight / 2);
                        velocity.Y = 0;
                        mainHitbox.X = (int)(position.X - activeAnimation.frameWidth / 2);
                        mainHitbox.Y = (int)(position.Y - activeAnimation.frameHeight / 2);
                        if (currentState != State.Jumping || currentState != State.Falling)
                        {
                            currentState = State.OnGround;
                            currentMoveState = MoveState.Standing;
                        }
                    }
                }



            }
        }
        public void Update(GameTime gameTime)
        {
            if (active)
            {
                prePosition = position;
                gamePadState = GamePad.GetState(playerNumber, GamePadDeadZone.None);
                GetInputLeft();
                GetInputRight();
                GetAngle();
                ApplyFriction(gameTime);
                ControllerMove(gameTime);
                StateManager();

                ApplyGravity(gameTime);
                Jump(gameTime);


                DirectionCheck();

                position += velocity;
                position.X = MathHelper.Clamp(position.X, playerBody.frameWidth / 2, windowWidth - playerBody.frameWidth / 2);
                position.Y = MathHelper.Clamp(position.Y, (playerBody.frameHeight + playerHead.frameHeight) / 2, windowHeight - playerBody.frameHeight / 2);

                if (currentMoveState == MoveState.Standing)
                {
                    playerWalking.active = false;
                    playerBody.active = true;
                    playerRunning.active = false;
                    playerJump.active = false;
                    playerCrouch.active = false;
                    activeAnimation = playerBody;
                    headPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                    armPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight) / 2);

                }
                if (currentMoveState == MoveState.Walking)
                {
                    playerWalking.active = true;
                    playerBody.active = false;
                    playerRunning.active = false;
                    playerJump.active = false;
                    playerCrouch.active = false;
                    activeAnimation = playerWalking;
                    headPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                    armPosition = new Vector2(position.X, position.Y - (playerBody.frameHeight) / 2);

                }
                if (currentMoveState == MoveState.Running && (currentState != State.Jumping && currentState != State.Falling))
                {
                    playerWalking.active = false;
                    playerBody.active = false;
                    playerRunning.active = true;
                    playerJump.active = false;
                    playerCrouch.active = false;
                    activeAnimation = playerRunning;
                    if (!flipped)
                    {
                        headPosition = new Vector2(position.X + 25, position.Y + 9 - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                        armPosition = new Vector2(position.X + 18, position.Y + 8 - (playerBody.frameHeight) / 2);
                        if (velocity.X < 0)
                        {
                            playerWalking.reversed = true;
                            playerRunning.reversed = true;
                        }
                        else
                        {
                            playerWalking.reversed = false;
                            playerRunning.reversed = false;
                        }
                    }
                    else
                    {
                        if (velocity.X > 0)
                        {
                            playerWalking.reversed = true;
                            playerRunning.reversed = true;
                        }
                        else
                        {
                            playerWalking.reversed = false;
                            playerRunning.reversed = false;
                        }
                        headPosition = new Vector2(position.X - 25, position.Y + 9 - (playerBody.frameHeight + playerHead.frameHeight) / 2);

                        armPosition = new Vector2(position.X - 18, position.Y + 8 - (playerBody.frameHeight) / 2);
                    }
                }

                if (gamePadState.IsButtonDown(Buttons.X) || gamePadState.IsButtonDown(Buttons.B))
                {
                    if (currentMoveState == MoveState.Standing)
                    {
                        playerWalking.active = false;
                        playerBody.active = false;
                        playerRunning.active = false;
                        playerJump.active = false;
                        playerCrouch.active = true;
                        activeAnimation = playerJump;

                        if (!flipped)
                        {
                            headPosition = new Vector2(position.X - 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                            armPosition = new Vector2(position.X - 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight) / 2);
                        }
                        else
                        {
                            headPosition = new Vector2(position.X + 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                            armPosition = new Vector2(position.X + 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight) / 2);
                        }
                    }
                    if (gamePadState.IsButtonDown(Buttons.B))
                    {
                        currentMoveState = MoveState.Crouch;
                    }
                }


                if (currentState == State.Jumping || currentState == State.Falling)
                {
                    playerWalking.active = false;
                    playerJump.active = true;
                    playerBody.active = false;
                    playerRunning.active = false;
                    playerCrouch.active = false;
                    activeAnimation = playerJump;
                    headPosition = new Vector2(position.X, position.Y + jumpList[playerJump.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                    armPosition = new Vector2(position.X, position.Y + jumpList[playerJump.frameIndex] - (playerBody.frameHeight) / 2);

                }

                playerWalking.position = position;
                playerCrouch.position = position;
                playerJump.position = position;
                playerRunning.position = position;
                playerBody.position = position;
                playerHead.position = headPosition;
                playerArm.position = armPosition;
                footBox = new Rectangle((int)position.X - playerBody.frameWidth / 2, (int)position.Y + (playerBody.frameHeight / 2) - 20, playerBody.frameWidth, 20);

                mainHitbox = new Rectangle((int)position.X - activeAnimation.frameWidth / 2, (int)position.Y - activeAnimation.frameHeight / 2, activeAnimation.frameWidth, activeAnimation.frameHeight);
                PlatformCollision();
                StairCollision();
                playerJump.Update(gameTime);
                playerBody.Update(gameTime);
                playerHead.Update(gameTime);
                playerWalking.Update(gameTime);
                playerArm.angle = armAngle;
                playerArm.Update(gameTime);
                playerCrouch.Update(gameTime);
                playerRunning.Update(gameTime);
                activeAnimation.Update(gameTime);
                
                if (flipped)
                {
                    playerArm.origin = new Vector2(playerArm.frameWidth, 0);

                }
                else
                {
                    playerArm.origin = new Vector2(0, 0);
                }

                if (flipped)
                {

                    weaponPosition = armPosition;
                }
                else
                {
                    weaponPosition = armPosition;
                }
                if (activeWeapon != null)
                {
                    activeWeapon.position = weaponPosition;
                    activeWeapon.gunAngle = armAngle;

                }
                if (activeWeapon != null)
                {
                    activeWeapon.Update(gameTime);
                }

                headHitbox = new Rectangle((int)headPosition.X - playerHead.frameWidth / 2, (int)headPosition.Y - playerHead.frameHeight / 2, playerHead.frameWidth, playerHead.frameHeight);
                playerTransformation =
                           Matrix.CreateTranslation(new Vector3(-activeAnimation.origin, 0.0f)) *
                           Matrix.CreateScale(activeAnimation.scale) *
                           Matrix.CreateTranslation(new Vector3(activeAnimation.position, 0.0f));
                Matrix.CreateTranslation(new Vector3(activeAnimation.position, 0.0f));

                headTransformation = Matrix.CreateTranslation(new Vector3(-playerHead.origin, 0.0f)) *
                           Matrix.CreateScale(playerHead.scale) *
                           Matrix.CreateTranslation(new Vector3(playerHead.position, 0.0f));
                Matrix.CreateTranslation(new Vector3(playerHead.position, 0.0f));

                 hatAnimation.position = headPosition - new Vector2((playerHead.frameWidth / 2), playerHead.frameHeight / 2) * 2 + hatPos;

                
               
                
                hatAnimation.Update(gameTime);
               
                    hatAnimation.origin = new Vector2(0, 0);
                    
               
              
                if(health <=0)
                {
                    PlayerDeath();
                    active = false;
                }
            }

        }
        Random random = new Random();
        public void PlayerDeath()
        {
            if(score >10)
            {
                for(int i = 0;i<score/20;i++)
                {
                    Game1.AddPickupPlayerDeath(position.X, position.Y, new Vector2(0, 40), new Vector2(random.Next(-10, 10), random.Next(-1, 0)));
                   
                }
                score -= score / 2;
            }
            Game1.RespawnPlayer((int)playerNumber);
        }
        public Matrix playerTransformation;
        public Matrix headTransformation;
        public void Draw(SpriteBatch sb)
        {
            if (active)
            {
               
                float healthPercentage = health / 100; ;
                float visibleWidth = (float)healthtexture.Width * healthPercentage;

                Rectangle healthRectangle = new Rectangle((int)position.X - activeAnimation.frameWidth / 2,
                                               (int)position.Y + activeAnimation.frameHeight / 2 +10,
                                               (int)(visibleWidth),
                                               healthtexture.Height);

                sb.Draw(healthtexture, healthRectangle, Color.Green);
                playerWalking.Draw(sb);
                playerCrouch.Draw(sb);
                playerJump.Draw(sb);
                playerRunning.Draw(sb);
                playerHead.Draw(sb);
                playerBody.Draw(sb);
                playerArm.Draw(sb);
                if (activeWeapon != null)
                {
                    activeWeapon.Draw(sb);
                }
                hatAnimation.Draw(sb);
            }
        }

    }
}
