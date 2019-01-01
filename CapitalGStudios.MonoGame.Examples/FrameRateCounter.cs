using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CapitalGStudios.MonoGame.Examples
{
    public class FrameRateCounter : DrawableGameComponent
    {
        private int m_iFrameRate = 0;
        private int m_iFrameCounter = 0;
        private string m_sMessage = string.Empty;
        private TimeSpan m_oElapsedTime = TimeSpan.Zero;

        public FrameRateCounter(Game oGame) : base(oGame)
        {

        }

        protected override void LoadContent()
        {

        }

        public override void Update(GameTime oGameTime)
        {
            m_oElapsedTime += oGameTime.ElapsedGameTime;

            if (m_oElapsedTime > TimeSpan.FromSeconds(1))
            {
                m_oElapsedTime -= TimeSpan.FromSeconds(1);
                m_iFrameRate = m_iFrameCounter;
                m_iFrameCounter = 0;
            }

            m_sMessage = $"FPS: {m_iFrameRate}";
        }

        public override void Draw(GameTime oGameTime)
        {
            m_iFrameCounter++;

            Examples.SpriteBatch.Begin();
            Examples.SpriteBatch.DrawString(Examples.SpriteFont, m_sMessage, new Vector2(6, 5), Color.Black);
            Examples.SpriteBatch.DrawString(Examples.SpriteFont, m_sMessage, new Vector2(5, 4), Color.White);
            Examples.SpriteBatch.End();
        }
    }
}