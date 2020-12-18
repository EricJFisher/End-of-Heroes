using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace DystopiaEngine
{
    public class Engine : Game
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly Random _random = new Random();

        private SpriteBatch spriteBatch;

        private Texture2D texture;
        private Vector2 position;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Engine()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;

            _graphicsDeviceManager = new GraphicsDeviceManager(this);
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            this.Window.Title = "End of Heroes";
            base.OnActivated(sender, args);
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.Window.Title = "(PAUSED)End of Heroes";
            base.OnDeactivated(sender, args);
        }

        protected override void Initialize()
        {
            position = new Vector2(0, 0);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            texture = this.Content.Load<Texture2D>("ui-life-icon");
        }

        protected override void UnloadContent()
        {
            texture.Dispose();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (!IsActive) return; // pauses game logic when not in focus
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            position.X += 60f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.X > this.GraphicsDevice.Viewport.Width)
                position.X = 0;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var framerate = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = framerate.ToString();

            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
