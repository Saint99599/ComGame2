using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project1.src
{
    public class Camera
    {
        public Matrix Transform;

        public Matrix Follow(Rectangle target)
        {
            target.X = MathHelper.Clamp(target.X, (int)Game1.screenWidth / 2, (int)(1000-Game1.screenHeight/ 2));
            Vector3 translation = new Vector3(-target.X - target.Width/2, -target.Y - target.Height/2, 0);

            //translation.X = MathHelper.Clamp(translation.X, Game1.screenWidth / 2, 1000 - screenWidth / 2);
            //translation.Y = Game1.screenHeight / 2;

            Vector3 offset = new Vector3(Game1.screenWidth/2, Game1.screenHeight/ 2, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset);

            return Transform;
        }
    }
}