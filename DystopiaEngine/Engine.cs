using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DystopiaEngine
{
    public class Engine : Game
    {
        public static GraphicsDeviceManager Graphics;
        public static SpriteBatch SpriteBatch;

        public Texture2D Texture;
        public Vector2 Position;

        public Texture2D HudTexture;
        public Vector2 HudPosition;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Engine()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            ScreenWidth = Graphics.PreferredBackBufferWidth;
            ScreenHeight = Graphics.PreferredBackBufferHeight;

            Position = new Vector2(0, 0);
            HudPosition = new Vector2(ScreenWidth / 2 - 64, ScreenHeight - 128);
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Texture = Content.Load<Texture2D>("placeholder");
            HudTexture = Content.Load<Texture2D>("vitals");

            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Escape))
                Exit();

            if (state.IsKeyDown(Keys.Right))
                Position.X += 10;
            if (state.IsKeyDown(Keys.Left))
                Position.X -= 10;
            if (state.IsKeyDown(Keys.Up))
                Position.Y -= 10;
            if (state.IsKeyDown(Keys.Down))
                Position.Y += 10;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch.Begin();
            SpriteBatch.Draw(Texture, Position, Color.White);
            SpriteBatch.Draw(HudTexture, HudPosition, Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
