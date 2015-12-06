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

        SmokeParticleSystem _sps;
        public AsteroidParticleSystem(Game game):
            base(game, 16)
        {
            game.Services.Register(this);
            _sps = game.Services.TryFind<SmokeParticleSystem>();
        }

        public void Add(Asteroid.Size size)
        {
            int angle = rnd.Next(0, 360);
            int x = Lcd.Width / 2 + (Lcd.Width * Mathi.FixedCos(angle)) / Mathi.FixedScale / 2;
            int y = Lcd.Height /2 - (Lcd.Height * Mathi.FixedSin(angle)) / Mathi.FixedScale / 2;
            angle += 180;
            base.Add(new Asteroid(new Point(x, y), angle, (int)Asteroid.Size.Large));
        }

        public override void Draw(ref Asteroid particle)
        {
            Renderer.DrawGeometry(particle.Geometry, true);
        }
        
        static readonly int[] MinSplits = new int[]{ 0, 2, 3};
        static readonly int[] MaxSplits = new int[]{ 0, 3, 5};
        void Split(Point pos, int size)
        {
            int splitCount = rnd.Next(
                MinSplits[size],
                MaxSplits[size]);
            if (splitCount == 0)
                return;

            int angleOffset = rnd.Next(360);
            int angle = 0;
            int radius = rnd.Next(Asteroid.MinRadius[size],Asteroid.MaxRadius[size]) / 2;
            for (int i = 0; i < splitCount; i++)
            {
                int x = pos.X + (radius * Mathi.FixedCos(angle + angleOffset)) / Mathi.FixedScale;
                int y = pos.Y - (radius * Mathi.FixedSin(angle + angleOffset)) / Mathi.FixedScale;
                base.Add(new Asteroid(new Point(x, y), angle, size-1));
                angle += 360 / splitCount;
            }
            // create 1 smoke "line" per split
            angleOffset += 360 / splitCount / 2;
            angle = 0;
            for (int k = 0; k < splitCount; k++)
            {
                int dustCount = rnd.Next(8);
                for (int i = 0; i < dustCount; i++)
                {
                    int x = pos.X + (radius * Mathi.FixedCos(angle + angleOffset)) / Mathi.FixedScale;
                    int y = pos.Y - (radius * Mathi.FixedSin(angle + angleOffset)) / Mathi.FixedScale;

                    _sps.Add(new Point(
                        Mathi.Lerp(pos.X, x, i, dustCount),
                        Mathi.Lerp(pos.Y, y, i, dustCount)));
                }
                angle += 360 / splitCount;
            }
        }
        
        void ResolveCollision<T>(ref Asteroid me, ref T other) where T:struct, IParticle
        {
            if (me.Collide(other))
            {
                me.Hit();
                other.Hit();
                if (!me.IsAlive)
                {
                    Point pos = me.Position;
                    int radius = me.Radius;
                    Game.Dispatcher.Post(() =>
                    {
                        Split(pos, radius);
                    });
                }
            }
        }
        public void ResolveCollision<T>(ParticleSystem<T> others) where T:struct, IParticle
        {
            int n = ActiveParticles;
            for (int i = 0; i < n; i++)
            {
                int N = others.ActiveParticles;
                for (int k = 0; k < N; k++)
                {
                    ResolveCollision(ref Particles[i], ref others.Particles[k]);
                }
            }
        }
    }
}
