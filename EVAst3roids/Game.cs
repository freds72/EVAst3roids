using MonoBrickFirmware.UserInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EVAst3roids
{
    abstract class Game
    {
        public readonly GameServiceContainer Services = new GameServiceContainer();
        public readonly MessageDispatcher Dispatcher = new MessageDispatcher();
        ButtonEvents _buttons = new ButtonEvents();

        private GameTime _fps = new GameTime();
        int _refreshRate = 0;
        bool _run = true;
        public int TargetRefreshRate {
            get
            {
                return _refreshRate;
            }
            set
            {
                _refreshRate = value;
            }
        }

        public Game()
        {
            _buttons.EscapePressed += () =>
            {
                _run = false;
            };
        }

        public void Stop()
        {
            _run = false;
        }

        public void ResetElapsedTime()
        {
            _fps.Reset();
        }

        public virtual void Initialize()
        {

        }

        public void Run()
        {
            EventWaitHandle stopped = new ManualResetEvent(false);
            while (_run)
            {
                _fps.Update();

                Dispatcher.Update();
                Update(_fps);

                Draw(_fps);

                stopped.WaitOne(_refreshRate);
            }
        }

        protected abstract void Update(GameTime gameTime);
        protected abstract void Draw(GameTime gameTime);
    }
}
