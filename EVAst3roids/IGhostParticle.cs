using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    interface IGhostParticle
    {
        Point[] GhostPositions { get; }
    }
}
