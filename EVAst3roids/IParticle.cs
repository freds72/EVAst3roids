using System;
using System.Collections.Generic;
using System.Text;
using MonoBrickFirmware.Display;

namespace EVAst3roids
{
    interface IParticle
    {
        bool IsAlive { get; }
        Point Position { get; }
        void Update(int dt);    
    }
}
