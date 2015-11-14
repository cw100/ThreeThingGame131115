using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThreeThingGame131115
{
    class Platform
    {
        Vector2 position;
        public Rectangle hitBox;
        Animation platformAnimation;
        int width, height;
        public void Initialize(Vector2 pos,int width, int height)
        {
  
            position = pos;

            hitBox = new Rectangle((int)(position.X), (int)(position.Y), width, height);

        }
 



    }
}