using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CapitalGStudios.MonoGame.Examples
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        private Vector3 m_oPosition = new Vector3(0, 0, 100000.0f);
        private float m_fZoom = 1.0f;

        // Input
        private KeyboardState m_oPreviousKeyboardState = Keyboard.GetState();
        private KeyboardState m_oCurrentKeyboardState = Keyboard.GetState();
        private MouseState m_oPreviousMouseState = Mouse.GetState();
        private MouseState m_oCurrentMouseState = Mouse.GetState();

        public Camera(Game oGame) : base(oGame)
        {

        }

        #region MonoGame Pipeline
        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime oGameTime)
        {
            m_oCurrentKeyboardState = Keyboard.GetState();
            m_oCurrentMouseState = Mouse.GetState();

            // Horizontal Panning
            if (m_oCurrentKeyboardState.IsKeyDown(Keys.D))
            {
                m_oPosition.X -= 100;
            }

            if (m_oCurrentKeyboardState.IsKeyDown(Keys.A))
            {
                m_oPosition.X += 100;
            }

            // Vertical Panning
            if (m_oCurrentKeyboardState.IsKeyDown(Keys.W))
            {
                m_oPosition.Y += 100;
            }

            if (m_oCurrentKeyboardState.IsKeyDown(Keys.S))
            {
                m_oPosition.Y -= 100;
            }

            // Zooming
            if (m_oCurrentKeyboardState.IsKeyDown(Keys.Q))
            {
                m_oPosition.Z += 1000;
            }

            if (m_oCurrentKeyboardState.IsKeyDown(Keys.E))
            {
                m_oPosition.Z -= 500;
            }

            m_oPreviousKeyboardState = m_oCurrentKeyboardState;
            m_oPreviousMouseState = m_oCurrentMouseState;

            base.Update(oGameTime);
        }
        #endregion

        #region Public Properties
        public Matrix View
        {
            get { return Matrix.CreateLookAt(new Vector3(-m_oPosition.X, m_oPosition.Y, m_oPosition.Z), new Vector3(-m_oPosition.X, m_oPosition.Y, 0), Vector3.Up) * Matrix.CreateScale(m_fZoom); }
        }
        #endregion
    }
}