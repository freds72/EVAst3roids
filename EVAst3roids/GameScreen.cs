using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameScreen
    {
        public bool IsActive = true;
        bool _isExiting = false;
        protected GameScreenManager ScreenManager { get; private set; }
        protected Game Game { get { return ScreenManager; } }
        public GameScreen(GameScreenManager manager)
        {
            this.ScreenManager = manager;
        }

        private void ExitHandler()
        {
            _isExiting = true;
        }

        public virtual void OnExit()
        {
        }

        public virtual void LoadAssets() 
        {
            Game.Gamepad.Buttons.EscapeReleased += ExitHandler;
        }

        public virtual void Update(GameTime gameTime) 
        {
            if (_isExiting)
            {
                OnExit();
                IsActive = false;
                return;
            }
        }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() 
        {
            Game.Gamepad.Buttons.EscapeReleased -= ExitHandler;
        }
    }
}
