using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class BulletParticleSystem : ParticleSystem<Bullet>
    {
        public BulletParticleSystem(int max):
            base(max)
        {
        }

        public override void Draw(ref Bullet particle)
        {
            Renderer.DrawGeometry(particle.Geometry, false);
        }
    }
}
