using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameTime
    {
        int _counter = 0;
        string _fps = "n/a";
        DateTime _t0 = DateTime.Now;
        int _dt = 1000 / 60; // target ? dt
        int _fpsTimer = 0;
        int _totalTime = 0;
        
        public override string ToString()
        {
            return _fps;
        }

        public void Reset()
        {
            _t0 = DateTime.Now;
            _dt = 1000 / 60;
            _totalTime = 0;
            _fpsTimer = 0;
            _fps = "n/a";
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            _dt = (int)((now - _t0).TotalSeconds * 1000);
            _fpsTimer += _dt;
            if (_fpsTimer >= 1000)
            {
                _fps = "fps: " + _counter.ToString();
                _fpsTimer = 0;
                _counter = 0;
            }
            _totalTime += _dt;
            _counter++;

            _t0 = now;
        }

        public int ElapsedGameTime
        {
            get { return _dt; }
        }

        public int TotalGameTime { get { return _totalTime; } }
    }
}
