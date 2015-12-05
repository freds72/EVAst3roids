using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class BulletParticleSystem : ParticleSystem<Bullet>
    {
        public BulletParticleSystem(Game game, int max):
            base(game, max)
        {
            game.Services.Register(this);
        }

        public override void Draw(ref Bullet particle)
        {
            Renderer.DrawGeometry(particle.Geometry, false);
        }
    }
}
