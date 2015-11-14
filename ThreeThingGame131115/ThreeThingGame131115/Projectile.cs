using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ThreeThingGame131115
{
  public class Projectile
    {
        
       public Animation projectileAnimation;
       public Vector2 position;
       public Vector2 prePosition;
       public Vector2 velocity;
       public Rectangle hitBox;
       public float damage;
       float speed;
       Vector2 angle;
       bool active;
       public void Initialize(Animation projectileAnimation, Vector2 position,float speed, Vector2 angle, float damage)
        {
            active = true;
            this.projectileAnimation = projectileAnimation;
            this.position = position;
            this.angle = angle;
            this.speed = speed;
            this.damage = damage;
            if (angle.X == 0 && angle.Y == 0)
            {
                angle = new Vector2(0, 1);
            }
            angle.Normalize();
            hitBox = new Rectangle((int)position.X - projectileAnimation.frameWidth / 2, (int)position.Y - projectileAnimation.frameHeight / 2, projectileAnimation.frameWidth, projectileAnimation.frameHeight);   
        }

        public void Update(GameTime gameTime)
       {
            prePosition = position;
            velocity = new Vector2(angle.X, -angle.Y) * speed * gameTime.ElapsedGameTime.Milliseconds;
            position += velocity;
            projectileAnimation.position = position;
            hitBox.X = (int)position.X - projectileAnimation.frameWidth / 2;
            hitBox.Y = (int)position.Y - projectileAnimation.frameHeight / 2;
            projectileAnimation.Update(gameTime);
            projectileTransformation =
             Matrix.CreateTranslation(new Vector3(-projectileAnimation.origin, 0.0f)) *
             Matrix.CreateScale(projectileAnimation.scale) *
             Matrix.CreateTranslation(new Vector3(projectileAnimation.position, 0.0f));
            Matrix.CreateTranslation(new Vector3(projectileAnimation.position, 0.0f));

        }
      public Matrix projectileTransformation;

        public void Draw(SpriteBatch sb)
        {
            if (active)
            {
                projectileAnimation.Draw(sb);
            }
        }
    }
}
