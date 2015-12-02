using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class SmokeParticleSystem : ParticleSystem<Smoke>
    {
        public SmokeParticleSystem():
            base(256)
        { 
        }

        public void Add(Point pos)
        {
            this.Add(new Smoke(pos));
        }

        public override void Draw(ref Smoke particle)
        {
            Point a = particle.Position;
            Point b = a;
            a.X -= particle.Radius;
            a.Y -= particle.Radius;
            b.X += particle.Radius;
            b.Y += particle.Radius;

            Lcd.DrawRectangle(new Rectangle(a, b), true, true);
        }
    }
}
