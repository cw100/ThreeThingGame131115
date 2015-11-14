using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThreeThingGame131115
{

      public class Pickup
        {

            public float velocityBuff;
            public Vector2 position;
            public Rectangle hitBox;
            public Vector2 gravity;
            public Vector2 velocity;
            public bool active;
            public float buffTime;
            public Animation pickupAnimation;
            public int score;
            int windowWidth, windowHeight;
            public void Initialize(Animation pickupAnimation, Vector2 intpos, Vector2 gravity,Vector2 intialVel, int score)
            {
                this.velocity = intialVel;
                active = true;
               this.gravity = gravity;
                position = intpos;
                this.score = score;
                this.pickupAnimation = pickupAnimation;
                pickupAnimation.Initialize(1, 1, intpos, 0f, Color.White);
                hitBox = new Rectangle((int)position.X - pickupAnimation.frameWidth / 2, (int)position.Y - pickupAnimation.frameHeight / 2, pickupAnimation.frameWidth, pickupAnimation.frameHeight);   
            }
            public void PlatformCollision()
            {
                foreach (Rectangle platform in Game1.platformRectangles)
                {


                    if (Collision.RectangleCollisionTop(hitBox, platform, velocity))
                    {
                        position.Y = (platform.Top - pickupAnimation.frameHeight / 2);
                        velocity.Y = 0;
                        hitBox.X = (int)(position.X - pickupAnimation.frameWidth / 2);
                        hitBox.Y = (int)(position.Y - pickupAnimation.frameHeight / 2);

                    }

                }
            }
            public void ApplyFriction()
            {
                velocity.X *= 0.9f;
            }
            public void Update(GameTime gameTime, int windowWidth, int windowHeight)
            {
                if (active)
                {
                    this.windowWidth = windowWidth;
                    this.windowHeight = windowHeight;
                    position += velocity;
                    ApplyFriction();
                    velocity.Y += gravity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    velocity.Y = MathHelper.Clamp(velocity.Y, -1000, velocity.Y);
                    position.X = MathHelper.Clamp(position.X, pickupAnimation.frameWidth / 2, windowWidth - pickupAnimation.frameWidth / 2);
                    position.Y = MathHelper.Clamp(position.Y, (pickupAnimation.frameHeight + pickupAnimation.frameHeight) / 2, windowHeight - pickupAnimation.frameHeight / 2);
                    hitBox.X = (int)position.X - pickupAnimation.frameWidth / 2; ;
                    hitBox.Y = (int)position.Y - pickupAnimation.frameHeight / 2; ;
                    PlatformCollision();
                    
                   
                    pickupAnimation.position = position;
                    pickupAnimation.Update(gameTime);
                }
            }
            public void Draw(SpriteBatch spriteBatch)
            {
                if (active)
                {
                    pickupAnimation.Draw(spriteBatch);
                }
            }




        }
    
}
