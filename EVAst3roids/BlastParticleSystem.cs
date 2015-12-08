using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class BlastParticleSystem : ParticleSystem<Blast>
    {
    	Random _rnd = new Random();
        static readonly int[] Rings = { 1, 2, 3 };
    	static readonly int[] MinParticles = { 3, 5, 7};
    	static readonly int[] MaxParticles = { 6, 10, 13};
        public BlastParticleSystem(Game game):
            base(game, 256)
        {
            game.Services.Register(this);
        }

        public void Add(Point pos, Asteroid.Size size, int radius)
        {
            int rings = Rings[(int)size];
            for (int i = 0; i < rings; ++i)
            {
                int azimuth = Mathi.Lerp(0, 90, i, rings);
                int n = _rnd.Next(MinParticles[i], MaxParticles[i]);
                for (int j = 0; j < n; ++j)
                {
                    int angle = _rnd.Next(360);

                    Add(new Blast(pos, angle, radius / (i + 1), azimuth));
                }
            }
        }

        public override void Draw(ref Blast particle)
        {
            Point pos = particle.Position;
            Lcd.SetPixel(pos.X, pos.Y, true);
            Lcd.SetPixel(pos.X -1, pos.Y, true);
            Lcd.SetPixel(pos.X + 1, pos.Y, true);
            Lcd.SetPixel(pos.X, pos.Y-1, true);
            Lcd.SetPixel(pos.X, pos.Y+1, true);
        }
    }
}
