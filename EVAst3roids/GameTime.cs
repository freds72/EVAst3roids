using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameTime
    {
        readonly int _dt;
        int _totalTime;
        public GameTime(int dt)
        {
            _dt = dt;
        }

        public void Next()
        {
            _totalTime += _dt;
        }

        public int ElapsedGameTime
        {
            get { return _dt; }
        }

        public int TotalGameTime { get { return _totalTime; } }
    }
}
