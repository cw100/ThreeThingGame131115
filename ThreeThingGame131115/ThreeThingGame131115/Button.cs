using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace ThreeThingGame131115
{
    class Button
    {
        int menuNum;
        public bool selected = false;
        public string buttonName;
        public string fileName;
        Vector2 position;
        public Rectangle hitBox;
        public Animation buttonAnimation;
        public bool pressed = false;
     
        public void Initialize(Vector2 pos, Animation buttonAnimation, string buttonname, int menunum)
        {
            menuNum = menunum;
            this.buttonName = buttonname;
            this.buttonAnimation = buttonAnimation;
            buttonAnimation.Initialize(1, 1, pos, 0f, Color.White, true);
            position = pos;
        }
        public bool CheckForClick()
        {
       
            if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                if (selected)
                {
                    pressed = true;
                    return true;
                }
            }
            pressed = false;
            return false;

        }

        public void Update(GameTime gameTime, int currentnum)
        {
            if  (menuNum == currentnum)
            {
                selected = true;
            }
            else
            {
                selected = false;
            }
            if (selected == true)
            {
                buttonAnimation.scale = 1.2f;
                buttonAnimation.color = Color.White;
            }
            else
            {
                buttonAnimation.scale = 1f;
                buttonAnimation.flip = SpriteEffects.None;
                buttonAnimation.color = Color.White;

            }

            buttonAnimation.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {

            buttonAnimation.Draw(spriteBatch);
        }



    }
}