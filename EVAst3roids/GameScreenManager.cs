using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameScreenManager : Game
    {
        GameScreen _activeScreen;
        bool _isScreenReady = false;
        GameScreen _nextScreen;

        public GameScreenManager()
        {             
        }

        public void Add(GameScreen screen)
        {
            if (screen == null)
                throw new ArgumentNullException("screen");

            IsActive = true;
            _nextScreen = screen;
        }

        public bool IsActive
        {
            get;
            private set;
        }

        protected override void Update(GameTime gameTime)
        {
            if (_activeScreen == null)
            {
                _activeScreen = _nextScreen;
                _nextScreen = null;
            }

            if (_activeScreen != null)
            {
                if (!_isScreenReady)
                {
                    _activeScreen.LoadAssets();
                    _isScreenReady = true;
                }
                _activeScreen.Update(gameTime);

                if (!_activeScreen.IsActive)
                {
                    _activeScreen.UnloadAssets();
                    _activeScreen = _nextScreen;
                    _isScreenReady = false;
                }
            }
            else
            {
                Stop();
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            if (_isScreenReady)
                _activeScreen.Draw(gameTime);
        }
    }
}