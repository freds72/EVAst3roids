using MonoBrickFirmware.Display;
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
            Point a = particle.Position;
            Point b = a;
            a.X -= 2;
            a.Y -= 2;
            b.X += 2;
            b.Y += 2;

            Lcd.DrawRectangle(new Rectangle(a, b), true, true);            
        }
    }
}
