using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class AsteroidParticleSystem : ParticleSystem<Asteroid>
    {
        static readonly Random rnd = new Random();

        public AsteroidParticleSystem():
            base(16)
        { }

        public void Add(Asteroid.Size size)
        {
            int angle = rnd.Next(0, 360);
            int x = Lcd.Width / 2 + (Lcd.Width * Mathi.FixedCos(angle)) / Mathi.FixedScale / 2;
            int y = Lcd.Height /2 - (Lcd.Height * Mathi.FixedSin(angle)) / Mathi.FixedScale / 2;
            angle += 180;
            base.Add(new Asteroid(new Point(x, y), angle, Asteroid.Size.Large));
        }

        public override void Draw(ref Asteroid particle)
        {
            Renderer.DrawGeometry(particle.Geometry, true);
        }

        public void ResolveCollision<T>(ParticleSystem<T> particles, List<Point> collisions) where T:struct, IParticle
        {
            int n = ActiveParticles;
            for (int i = 0; i < n; i++)
            {
                int N = particles.ActiveParticles;
                for (int k = 0; k < N; k++)
                {
                    if (Particles[i].Collide(particles.Particles[k].Position))
                    {
                        collisions.Add(particles.Particles[k].Position);
                    }
                }
            }
        }
    }
}
