using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    static class Mathi
    {
        public const int FixedScale = 128;

        static readonly int[] _cos, _sin;
        static Mathi()
        {
            _cos = new int[360];
            _sin = new int[360];
            for(int i=0;i<360;i++)
            {
                _cos[i] = (int)(FixedScale *  Math.Cos(2 * Math.PI * i / 360.0));
                _sin[i] = (int)(FixedScale * Math.Sin(2 * Math.PI * i / 360.0));
            }
        }

        public static int FixedCos(int a)
        {
            a %= 360;
            if ( a < 0 )
                a += 360;
            return _cos[a];
        }

        public static int FixedSin(int a)
        {
            a %= 360;
            if (a < 0)
                a += 360;
            return _sin[a];
        }

        public static int EaseOutCubic(int t, int start, int change, int duration) {
	        t /= duration;
	        t--;
            return change * (t * t * t + 1) + start;
        }
    }
}

