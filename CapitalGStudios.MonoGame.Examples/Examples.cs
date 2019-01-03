using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CapitalGStudios.MonoGame.Examples
{
    public class Examples : Game
    {
        public static SpriteBatch SpriteBatch = null;
        public static SpriteFont SpriteFont = null;

        // Constants
        private const int QuadPixelSize = 32;       
        private const int QuadTotalVertices = 6;
        
        // Our quad is composed of:
        // - Position - Vector3 - float x 3
        // - Color - RGBA - byte x 4
        // - Texture - Vector2 - float x 2
        private const int SizeOfQuadVertexInBytes = (sizeof(float) * 3) + 4 + (sizeof(float) * 2);

        // Quad Data
        private int m_iWidth = 500;
        private int m_iHeight = 500;
        private int m_iQuadTotal = 0;

        public Random Random = new Random();
        private GraphicsDeviceManager m_oGraphicDevice = null;

        // Textures
        private Texture2D m_oTextureQuad = null;

        // Effects
        private BasicEffect m_oBasicEffect = null;

        // Vertex Buffers
        private VertexBuffer m_oVertexBuffer = null;

        // Input
        public bool DrawingVertexBuffer = true;
        private KeyboardState m_oPreviousKeyboardState = Keyboard.GetState();

        // Components
        private FrameRateCounter m_oFrameRateCounter = null;
        private Camera m_oCamera = null;

        public Examples()
        {
            m_oGraphicDevice = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            m_oFrameRateCounter = new FrameRateCounter(this);
            m_oCamera = new Camera(this);

            Components.Add(m_oFrameRateCounter);
            Components.Add(m_oCamera);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Textures
            m_oTextureQuad = Content.Load<Texture2D>("quad");

            // Load fonts
            SpriteFont = Content.Load<SpriteFont>("arial");

            // Effects
            m_oBasicEffect = new BasicEffect(GraphicsDevice);
            m_oBasicEffect.TextureEnabled = true;
            m_oBasicEffect.Texture = m_oTextureQuad;
            m_oBasicEffect.VertexColorEnabled = true;

            Vector3 oCameraPosition = new Vector3(0.0f, 0.0f, 100000.0f);
            Vector3 oCameraTarget = new Vector3(0.0f, 0.0f, 0.0f); // Look back at the origin

            float fFovAngle = MathHelper.ToRadians(45);  // convert 45 degrees to radians
            float fAspectRatio = GraphicsDevice.Viewport.Width / GraphicsDevice.Viewport.Height;
            float fNear = 0.01f; // the near clipping plane distance
            float fFar = 1500000f; // the far clipping plane distance

            Matrix oWorld = Matrix.CreateTranslation(0.0f, 0.0f, 0.0f);
            Matrix oView = Matrix.CreateLookAt(oCameraPosition, oCameraTarget, Vector3.Up);
            Matrix oProjection = Matrix.CreatePerspectiveFieldOfView(fFovAngle, fAspectRatio, fNear, fFar);
            m_oBasicEffect.World = oWorld;
            m_oBasicEffect.View = oView;
            m_oBasicEffect.Projection = oProjection;

            CreateQuads();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState oCurrentKeyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            if(m_oPreviousKeyboardState.IsKeyDown(Keys.F1) && oCurrentKeyboardState.IsKeyUp(Keys.F1))
            {
                DrawingVertexBuffer = !DrawingVertexBuffer;
            }

            if (m_oPreviousKeyboardState.IsKeyDown(Keys.Up) && oCurrentKeyboardState.IsKeyUp(Keys.Up))
            {
                m_iWidth += 5;
                m_iHeight += 5;

                CreateQuads();
            }

            if (m_oPreviousKeyboardState.IsKeyDown(Keys.Down) && oCurrentKeyboardState.IsKeyUp(Keys.Down))
            {
                if (m_iWidth - 5 > 0)
                {
                    m_iWidth -= 5;
                }

                if (m_iHeight - 5 > 0)
                {
                    m_iHeight -= 5;
                }

                CreateQuads();
            }

            m_oPreviousKeyboardState = oCurrentKeyboardState;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            m_oBasicEffect.View = m_oCamera.View;

            string sMode = string.Empty;
            if (DrawingVertexBuffer)
            {
                sMode = "VertexBuffer";

                GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
                GraphicsDevice.SetVertexBuffer(m_oVertexBuffer);
                foreach (EffectPass oEffectPass in m_oBasicEffect.CurrentTechnique.Passes)
                {
                    oEffectPass.Apply();

                    GraphicsDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, m_iQuadTotal * 2);
                }
            }
            else
            {
                sMode = "SpriteBatch";

                SpriteBatch.Begin(SpriteSortMode.BackToFront);
                for (int iX = 0; iX < m_iWidth; iX++)
                {
                    for (int iY = 0; iY < m_iHeight; iY++)
                    {
                        SpriteBatch.Draw(m_oTextureQuad, new Vector2(iX * 32, iY * 32), Color.White);
                    }
                }
                SpriteBatch.End();
            }

            SpriteBatch.Begin();
            SpriteBatch.DrawString(SpriteFont, $"Rendering {m_iQuadTotal} quads with {sMode}", new Vector2(100, 5), Color.Black);
            SpriteBatch.DrawString(SpriteFont, $"Rendering {m_iQuadTotal} quads with {sMode}", new Vector2(99, 4), Color.White);
            SpriteBatch.End();

            base.Draw(gameTime);
        }

        #region Private Methods
        private void CreateQuads()
        {
            m_iQuadTotal = m_iWidth * m_iHeight;

            // Generate vertex buffer
            VertexPositionColorTexture[] oVertices = new VertexPositionColorTexture[m_iQuadTotal * QuadTotalVertices];
            m_oVertexBuffer = new VertexBuffer(GraphicsDevice, typeof(VertexPositionColorTexture), oVertices.Length, BufferUsage.WriteOnly);

            int iX = 0;
            int iY = 0;
            for (int iCell = 0; iCell < m_iQuadTotal; iCell++)
            {
                int iCellIndex = iCell * 6;
                Color oColor = new Color(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));

                // Vertex Position
                // - Triangle 1
                oVertices[iCellIndex + 0].Position = new Vector3((iX * 32), (iY * 32), 0);
                oVertices[iCellIndex + 1].Position = new Vector3((iX * 32), 32 + (iY * 32), 0);
                oVertices[iCellIndex + 2].Position = new Vector3(32 + (iX * 32), (iY * 32), 0);
                // - Triangle 2
                oVertices[iCellIndex + 3].Position = oVertices[iCellIndex + 1].Position;
                oVertices[iCellIndex + 4].Position = new Vector3(32 + (iX * 32), 32 + (iY * 32), 0);
                oVertices[iCellIndex + 5].Position = oVertices[iCellIndex + 2].Position;

                // Vertex Color
                // - Triangle 1
                oVertices[iCellIndex + 0].Color = oColor;
                oVertices[iCellIndex + 1].Color = oColor;
                oVertices[iCellIndex + 2].Color = oColor;
                // - Triangle 2
                oVertices[iCellIndex + 3].Color = oColor;
                oVertices[iCellIndex + 4].Color = oColor;
                oVertices[iCellIndex + 5].Color = oColor;

                // Vertex Texture
                // - Triangle 1
                oVertices[iCellIndex + 0].TextureCoordinate = new Vector2(0, 1);
                oVertices[iCellIndex + 1].TextureCoordinate = new Vector2(0, 0);
                oVertices[iCellIndex + 2].TextureCoordinate = new Vector2(1, 1);
                // - Triangle 2
                oVertices[iCellIndex + 3].TextureCoordinate = oVertices[iCellIndex + 1].TextureCoordinate;
                oVertices[iCellIndex + 4].TextureCoordinate = new Vector2(1, 0);
                oVertices[iCellIndex + 5].TextureCoordinate = oVertices[iCellIndex + 2].TextureCoordinate;

                if (iX < (m_iWidth - 1))
                {
                    iX++;
                }
                else
                {
                    iX = 0;
                    iY++;
                }
            }

            m_oVertexBuffer.SetData(oVertices);
        }
        #endregion
    }
}