﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1.src
{
    public class Player:Entity
    {
        public Vector2 velocity;

        public float playerSpeed = 4;
        public float fallSpeed = 10;
        public bool isFalling = true;
        public bool isIntersecting = false;

        public Animation[] playerAnimation;
        public currentAnimation playerAnimationController;

        public Player(Texture2D idleSprite, Texture2D runSprite) 
        {
            playerAnimation = new Animation[2];

            position = new Vector2();
            velocity = new Vector2();

            playerAnimation[0] = new Animation(idleSprite);
            playerAnimation[1] = new Animation(runSprite);
            hitbox = new Rectangle((int)position.X, (int)position.Y, 32, 32);
        }

        public override void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            playerAnimationController = currentAnimation.Idle;
                velocity.Y += fallSpeed;
            if (keyboard.IsKeyDown(Keys.A)) 
            {
                velocity.X -= playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                velocity.X += playerSpeed;
                playerAnimationController = currentAnimation.Run;
            }

            position = velocity;
            hitbox.X = (int)position.X;
            hitbox.Y = (int)position.Y;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            switch (playerAnimationController)
            {
                case currentAnimation.Idle:
                    playerAnimation[0].Draw(spriteBatch, position, gameTime, 100);
                    break;
                case currentAnimation.Run:
                    playerAnimation[1].Draw(spriteBatch, position, gameTime, 100);
                    break;
            }
            //playerAnimation.Draw(spriteBatch, postion, gameTime, 100);
            //spriteBatch.Draw(spritesheet, postion, Color.White);
        }
    }
}
