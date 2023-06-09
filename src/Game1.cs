﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TiledSharp;

namespace Project1.src
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static float screenWidth;
        public static float screenHeight;

        private Matrix scaleMatrix;

        #region Tilemaps
        private TmxMap map;
        private TilemapManager tilemapManager;
        private Texture2D tileset;
        private List<Rectangle> collisionRects;
        private Rectangle startRect;
        private Rectangle endRect;
        #endregion

        #region Player
        private Player player;
        #endregion

        #region Camera
        private Camera camera;
        private Matrix transformMatrix;
        #endregion

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.ApplyChanges();
            screenWidth = _graphics.PreferredBackBufferWidth;
            screenHeight = _graphics.PreferredBackBufferHeight;
            var WindowSize = new Vector2(screenWidth, screenHeight);
            var mapSize = new Vector2(250, 450);
            var scale = WindowSize / mapSize;
            scaleMatrix = Matrix.CreateScale(scale.X, scale.Y, 1f);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            #region Tilemaps
            map = new TmxMap("Content/Map.tmx");
            tileset = Content.Load<Texture2D>("assets/" + map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int tilesetTileWidth = tileset.Width / tileWidth;

            tilemapManager = new TilemapManager(map, tileset, tilesetTileWidth, tileWidth, tileHeight);
            #endregion

            collisionRects = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                if (o.Name == "")
                {
                    collisionRects.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                    Console.WriteLine("X: " + o.X + " Y: " + o.Y + " W: " +o.Width + " H: " + o.Height);
                }
            }

            #region Player
            player = new Player(
                Content.Load<Texture2D>("Movement\\Idle"),
                Content.Load<Texture2D>("Movement\\Run")
            );
            #endregion

            #region Camera
            camera = new Camera();
            #endregion
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var initPos = player.position;
            player.Update();

            #region Player CollisionRects
            //Y axis
            foreach (var rect in collisionRects)
            {
                player.isFalling = true;
                if (rect.Intersects(player.playerFallRect))
                {
                    player.isFalling = false;
                    break;
                }
            }

            //X axis
            foreach (var rect in collisionRects)
            {
                Console.WriteLine("R - X: " + rect.X + " Y: " + rect.Y + " W: " +rect.Width + " H: " + rect.Height);
                Console.WriteLine("P - X: " + player.position.X + " Y: " + player.position.Y);
                Console.WriteLine("PH- X: " + player.hitbox.X + " Y: " + player.hitbox.Y + " W: " + player.hitbox.Width + " H: " + player.hitbox.Height);
                if (rect.Intersects(player.hitbox))
                {
                    player.position.X = initPos.X;
                    player.velocity.X = initPos.X;
                    break;
                }
            }
            #endregion

            #region Camera update
            transformMatrix = camera.Follow(player.hitbox);
            #endregion

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: transformMatrix);
            tilemapManager.Draw(_spriteBatch);
            player.Draw(_spriteBatch, gameTime);
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}