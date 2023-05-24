using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.src
{
    public class Animation
    {
        Texture2D spritesheet;
        int frames;
        int row = 0;
        int c = 0;
        float timeSinceLastFrame = 0;
        public Animation(Texture2D spritesheet, float width = 48, float height = 48)
        {
            this.spritesheet = spritesheet;
            frames = (int)(spritesheet.Width / width);
            Console.WriteLine(frames);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime,float milisecondsperframes = 500)
        {
            if (c < frames)
            {
                var rect = new Rectangle(48 * c, row, 48, 48);
                spriteBatch.Draw(spritesheet, position, rect, Color.White);
                timeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (timeSinceLastFrame > milisecondsperframes)
                {
                    timeSinceLastFrame -= milisecondsperframes;
                    c++;
                    if (c == frames)
                    {
                        c = 0;
                    }
                }
            }
        }
    }
}
