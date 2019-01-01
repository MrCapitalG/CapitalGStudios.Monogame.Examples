using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CapitalGStudios.MonoGame.Examples
{
    public class Examples : Game
    {
        public static SpriteBatch SpriteBatch = null;
        public static SpriteFont SpriteFont = null;

        private GraphicsDeviceManager m_oGraphicDevice = null;

        // Textures
        private Texture2D m_oTextureQuad = null;

        // Components
        private static FrameRateCounter m_oFrameRateCounter = null;

        public Examples()
        {
            m_oGraphicDevice = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            m_oFrameRateCounter = new FrameRateCounter(this);

            Components.Add(m_oFrameRateCounter);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Textures
            m_oTextureQuad = Content.Load<Texture2D>("quad");

            // Load fonts
            SpriteFont = Content.Load<SpriteFont>("arial");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            SpriteBatch.Begin(SpriteSortMode.BackToFront);
            for (int i = 0; i < 75_000; i++)
            {
                SpriteBatch.Draw(m_oTextureQuad, Vector2.Zero, Color.White);
            }
            SpriteBatch.End();

            base.Draw(gameTime);
        }
    }
}