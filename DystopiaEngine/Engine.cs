using DystopiaEngine.Components;
using DystopiaEngine.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Entities;
using System;

namespace DystopiaEngine
{
    public class Engine : Game
    {
        private readonly GraphicsDeviceManager _graphicsDeviceManager;
        private readonly FramesPerSecondCounter _framesPerSecondCounter = new FramesPerSecondCounter();
        private readonly Random _random = new Random();

        private EntityFactory entityFactory;
        private SpriteBatch spriteBatch;
        private BitmapFont font;
        private World _world;

        public Texture2D Texture;
        public Vector2 Position;

        public Texture2D HudTexture;
        public Vector2 HudPosition;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Engine()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Window.AllowUserResizing = false;
            IsFixedTimeStep = true;

            _graphicsDeviceManager = new GraphicsDeviceManager(this)
            {
                IsFullScreen = false,
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600,
                PreferredBackBufferFormat = SurfaceFormat.Color,
                PreferMultiSampling = false,
                PreferredDepthStencilFormat = DepthFormat.None,
                SynchronizeWithVerticalRetrace = true
            };

            Position = new Vector2(0, 0);
            HudPosition = new Vector2(ScreenWidth / 2 - 64, ScreenHeight - 128);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            entityFactory = new EntityFactory();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<BitmapFont>("montserrat-32");

            _world = new WorldBuilder()
                .AddSystem(new HealthBarRenderSystem(spriteBatch, font))
                .AddSystem(new HudRenderSystem(GraphicsDevice, spriteBatch, font))
                .AddSystem(new PlayerControlSystem(entityFactory))
                .AddSystem(new RenderSystem(spriteBatch, Content))
                .Build();

            entityFactory.World = _world;

            InitializePlayer();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Escape))
                Exit();

            _framesPerSecondCounter.Update(gameTime);
            _world.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _framesPerSecondCounter.Draw(gameTime);
            var fps = $"FPS: {_framesPerSecondCounter.FramesPerSecond}";

            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            _world.Draw(gameTime);

            spriteBatch.DrawString(font, fps, new Vector2(16, 16), Color.White);

            spriteBatch.End();
        }

        private void InitializePlayer()
        {
            var viewport = GraphicsDevice.Viewport;

            var entity = _world.CreateEntity();
            entity.Attach(new Transform2(x: viewport.Width * 0.5f, y: viewport.Height - 50f));
            entity.Attach(new SpatialFormComponent { SpatialFormFile = "Player" });
            entity.Attach(new PlayerComponent());
        }
    }
}
