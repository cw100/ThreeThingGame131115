using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ThreeThingGame131115
{
    class Player
    {

        Animation playerRunning;
        Animation playerBody;
        Animation playerHead;
        Animation playerArm;
        Animation playerJump;
        Animation playerCrouch;
        Animation playerWalking;
        Vector2 position;
        Vector2 gravity;
        Vector2 headPosition;
        Vector2 armPosition;
        Vector2 velocity;
        Vector2 preJumpPosition;
        Vector2 jumpSpeed;
        Rectangle mainHitbox, headHitbox;
        Vector2 armAngleVector;
        float armAngle;
        float windowWidth, windowHeight;
        PlayerIndex playerNumber;
        bool flipped;
        float playerSpeed;
        GamePadState gamePadState;
        Vector2 stickInputRight, stickInputLeft;
        List<float> jumpList = new List<float>();
        
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
        State currentState = State.OnGround;
        MoveState currentMoveState = MoveState.Standing;
        public void Initialize(Animation playerBody, Animation playerRunning, Animation playerWalking, Animation playerCrouch, Animation playerJump, Animation playerHead, Animation playerArm, Vector2 position, float windowWidth, float windowHeight, Vector2 gravity,
          float playerSpeed, Vector2 jumpSpeed, PlayerIndex playerNumber)
        {
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
            playerRunning.Initialize(20, 0.2f, position, 0, Color.White);
            playerJump.Initialize(true, 5,0.2f, position, 0, Color.White);
            playerCrouch.Initialize(true, 5, 0.2f, position, 0, Color.White);
            playerRunning.active = false;
            playerCrouch.active = false;
            playerJump.active = false;
            playerWalking.active = false;
            playerHead.Initialize(1, 1, headPosition, 0, Color.White);
            playerArm.Initialize(1, 1, armPosition, 0, Color.White);
            mainHitbox = new Rectangle((int)position.X, (int)position.Y, playerBody.frameWidth, playerBody.frameHeight);   
            jumpList.Add(26);
            jumpList.Add(22);
            jumpList.Add(16);
            jumpList.Add(9);
            jumpList.Add(0);
          
        }
        public void GetInputRight()
        {
            gamePadState = GamePad.GetState(playerNumber, GamePadDeadZone.None);
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
            gamePadState = GamePad.GetState(playerNumber, GamePadDeadZone.None);
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
                    flipped = true;
                }
                else
                {
                    playerWalking.flip = SpriteEffects.None;
                    playerCrouch.flip = SpriteEffects.None;
                    playerJump.flip = SpriteEffects.None;
                    playerRunning.flip = SpriteEffects.None;
                    playerHead.flip = SpriteEffects.None;
                    playerArm.flip = SpriteEffects.None;
                    playerBody.flip = SpriteEffects.None;
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
       
        public void Update(GameTime gameTime)
        {
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
            position.Y = MathHelper.Clamp(position.Y, (playerBody.frameHeight + playerHead.frameHeight) / 2, windowHeight - playerBody.frameHeight  / 2);
            
            if (currentMoveState == MoveState.Standing)
            {
                playerWalking.active = false;
                playerBody.active = true;
                playerRunning.active = false;
                playerJump.active = false;
                playerCrouch.active = false;
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
            if (currentMoveState == MoveState.Standing)
            {
                if (gamePadState.IsButtonDown(Buttons.X))
                {
                    playerWalking.active = false;
                    playerBody.active = false;
                    playerRunning.active = false;
                    playerJump.active = false;
                    playerCrouch.active = true;
                    if (!flipped)
                    {
                        headPosition = new Vector2(position.X-5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                        armPosition = new Vector2(position.X - 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight) / 2);
                    }
                    else
                    {
                        headPosition = new Vector2(position.X + 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                        armPosition = new Vector2(position.X + 5, position.Y + jumpList[4 - playerCrouch.frameIndex] - (playerBody.frameHeight) / 2);
                    }
                }
            }
            if (currentState == State.Jumping || currentState == State.Falling)
            {
                playerWalking.active = false;
                playerJump.active = true;
                playerBody.active = false;
                playerRunning.active = false;
                playerCrouch.active = false;

                headPosition = new Vector2(position.X, position.Y+jumpList[playerJump.frameIndex] - (playerBody.frameHeight + playerHead.frameHeight) / 2);
                armPosition = new Vector2(position.X, position.Y + jumpList[playerJump.frameIndex] - (playerBody.frameHeight) / 2);
                
            }
            playerWalking.position = position;
            playerCrouch.position = position;
            playerJump.position = position;
            playerRunning.position = position;
            playerBody.position = position;
            playerHead.position = headPosition;
            playerArm.position = armPosition;
            playerJump.Update(gameTime);
            playerBody.Update(gameTime);
            playerHead.Update(gameTime);
            playerWalking.Update(gameTime);
            playerArm.angle = armAngle;
            playerArm.Update(gameTime);
            playerCrouch.Update(gameTime);
            playerRunning.Update(gameTime);
            
            if(flipped)
            {
                playerArm.origin = new Vector2(playerArm.frameWidth , 0);
               
            }
            else
            {
                playerArm.origin = new Vector2(0,0);
            }
        }
        public void Draw(SpriteBatch sb)
        {
            playerWalking.Draw(sb);
            playerCrouch.Draw(sb);
            playerJump.Draw(sb);
            playerRunning.Draw(sb);
           playerHead.Draw(sb);
           playerBody.Draw(sb);
           playerArm.Draw(sb);
        }

    }
}
