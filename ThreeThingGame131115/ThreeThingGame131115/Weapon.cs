using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ThreeThingGame131115
{
    class Weapon
    {
        GamePadState gamePadState;
        Texture2D projectileTex;
        Animation projectileAnimation;
        public Animation weaponAnimation;
        public Vector2 position;
        Vector2 shootOffset;
        Vector2 shootPosition;
        public bool flipped;
        public Vector2 shootAngle;
        bool pickedUp;
        public bool active;
        TimeSpan fireRate;
        TimeSpan previousFireTime;
        public float damage, shootSpeed;
        public float gunAngle;
        PlayerIndex playerNumber;
        Rectangle hitBox;
        public Vector2 weaponHandle;
        public enum Type
        {
            Gun
        }
        public Type type = Type.Gun;
        public void Initialize(Animation weaponAnimation, Texture2D projectileTex, Vector2 position, Vector2 shootOffset, Vector2 weaponHandle, float gunAngle,
            bool pickedUp, float shootSpeed, TimeSpan fireRate, float damage, PlayerIndex playerNumber)
        {
            this.weaponHandle = weaponHandle;
            flipped = false;
            this.gunAngle = gunAngle;
            this.shootSpeed = shootSpeed;
            this.projectileTex = projectileTex;
            this.playerNumber = playerNumber;
            this.weaponAnimation = weaponAnimation;

           
            this.position = position;
            this.pickedUp = pickedUp;
            this.fireRate = fireRate;
            this.damage = damage;
            weaponAnimation.Initialize(1, 1, position, 0, Color.White);
            shootOffset = new Vector2(weaponAnimation.frameWidth + weaponHandle.X + 26 - projectileTex.Width / 2, weaponHandle.Y + projectileTex.Height / 2);
            active = true;
            hitBox = new Rectangle((int)position.X - weaponAnimation.frameWidth / 2, (int)position.Y - weaponAnimation.frameHeight / 2, weaponAnimation.frameWidth, weaponAnimation.frameHeight);   
        }
        public void Shoot(GameTime gameTime)

        {
            
            if (gameTime.TotalGameTime - previousFireTime > fireRate)
            {
                if (GamePad.GetState(playerNumber).Triggers.Right >= 0.5f)
                {
                    Projectile projectile = new Projectile();
                    projectileAnimation = new Animation();
                    projectileAnimation.LoadTexture(projectileTex);
                    projectileAnimation.Initialize(1, 1, shootPosition, 0, Color.White);
                    previousFireTime = gameTime.TotalGameTime;
                    projectile.Initialize(projectileAnimation, shootPosition,shootSpeed, shootAngle, damage);
                    Game1.projectiles.Add(projectile);
                }
            }
           
        }
        Vector2 AngleToVector(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }
        Vector2 Rotate(Vector2 vector, float angle, Vector2 origin)
        {
            vector.X = (float)Math.Cos(angle) * (vector.X - origin.X) - (float)Math.Cos(angle) * (vector.Y - origin.Y) + origin.X;
            vector.Y = (float)Math.Sin(angle) * (vector.X - origin.X) + (float)Math.Sin(angle) * (vector.Y - origin.Y) + origin.Y;
            return new Vector2(vector.X, vector.Y);
        }
        public Vector2 armOrigin;
         public void Update(GameTime gameTime)
         {

             
             
             gamePadState = GamePad.GetState(playerNumber, GamePadDeadZone.None);
             
             shootAngle = AngleToVector(-gunAngle);
             
             hitBox.X = (int)position.X - weaponAnimation.frameWidth / 2;
             hitBox.Y = (int)position.Y - weaponAnimation.frameHeight / 2;
             weaponAnimation.position = position;
             weaponAnimation.angle = gunAngle;
             weaponAnimation.Update(gameTime);
             if (flipped)
             {

                 weaponAnimation.origin = new Vector2(weaponAnimation.frameWidth + 26, -10) + new Vector2(-weaponHandle.X, weaponHandle.Y);
             }
             else
             {
                 weaponAnimation.origin = new Vector2(-26, -10) + weaponHandle;
             
             }
             shootOffset = new Vector2(weaponAnimation.frameWidth + weaponHandle.X + 26 - projectileTex.Width/2 , weaponHandle.Y - projectileTex.Height/2 +10);
             if (flipped)
             {
                 shootAngle *= -1;
                 shootPosition =position- new Vector2(shootOffset.Length() * (float)Math.Cos((double)gunAngle), shootOffset.Length() * (float)Math.Sin((double)gunAngle));
             }
             else
             {
                 shootPosition = position+new Vector2(shootOffset.Length() * (float)Math.Cos((double)gunAngle), shootOffset.Length() * (float)Math.Sin((double)gunAngle));

                
             }
             Shoot(gameTime);
             
             
         }

        public void Draw(SpriteBatch sb)
        { 
        if(active)
        {
            weaponAnimation.Draw(sb);
        }
        }


    }
}
