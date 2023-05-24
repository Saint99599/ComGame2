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
            Vector3 translation = new Vector3(-target.X - target.Width/2, -target.Y - target.Height/2, 0);
            Vector3 offset = new Vector3(Game1.screenWidth/2, Game1.screenHeight/ 2, 0);

            Transform = Matrix.CreateTranslation(translation)*Matrix.CreateTranslation(offset);

            return Transform;
        }
    }
}