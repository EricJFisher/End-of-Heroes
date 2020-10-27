using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EndOfHeroes
{
    public class Desktop : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Texture2D Texture;
        public Vector2 Position;

        public Texture2D HudTexture;
        public Vector2 HudPosition;

        public static int ScreenWidth;
        public static int ScreenHeight;

        public Desktop()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Position = new Vector2(0, 0);

            ScreenWidth = _graphics.PreferredBackBufferWidth;
            ScreenHeight = _graphics.PreferredBackBufferHeight;

            HudPosition = new Vector2(ScreenWidth/2-64,ScreenHeight-128);
        }

        protected override void Initialize()
        {
            base.Initialize();
            Position = new Vector2(_graphics.GraphicsDevice.Viewport.Width /2,
                _graphics.GraphicsDevice.Viewport.Height /2);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Texture = Content.Load<Texture2D>("placeholder");
            HudTexture = Content.Load<Texture2D>("vitals");
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

            _spriteBatch.Begin();
            _spriteBatch.Draw(Texture, Position, Color.White);
            _spriteBatch.Draw(HudTexture, HudPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
