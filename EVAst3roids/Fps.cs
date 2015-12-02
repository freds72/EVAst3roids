using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class Fps
    {
        int _counter = 0;
        string _fps = "n/a";
        DateTime _t0 = DateTime.Now;
        int _dt = 1000 / 60; // target ? dt

        public override string ToString()
        {
            return _fps;
        }

        public void Update()
        {
            DateTime now = DateTime.Now;
            _dt = (int)((now - _t0).TotalSeconds * 1000);
            if (_dt >= 1000)
            {
                _fps = "fps: " + _counter.ToString();
                _counter = 0;
                _t0 = now;
            }

            _counter++;
        }

        public int ElaspedMilliseconds
        {
            get
            { return _dt; }
        }
    }
}
