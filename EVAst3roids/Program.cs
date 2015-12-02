using System;
using System.Threading;
using MonoBrickFirmware.Display;
using MonoBrickFirmware.UserInput;
using System.Collections.Generic;
using System.IO;

namespace EVAst3roids
{
    class Program
    {
        static int _line = 0;
        static void Log(string log)
        {
            Lcd.WriteText(Font.SmallFont, new Point(0, _line), log, true);
            Lcd.Update();
            _line += (int)Font.SmallFont.maxHeight;
            if (_line > Lcd.Height)
            {
                Lcd.Clear();
                _line = 0;
            }
        }

        static void Main(string[] args)
        {
            EventWaitHandle stopped = new ManualResetEvent(false);
            Point center = new Point(Lcd.Width / 2, Lcd.Height / 2);
            int refreshRate = 16;
            bool run = true;

            ButtonEvents buts = new ButtonEvents();
            Gamepad input = new Gamepad();
            buts.EscapePressed += () =>
            {
                stopped.Set();
                run = false;
            };
            buts.UpReleased += () =>
                {
                    refreshRate++;
                };
            buts.DownReleased += () =>
                {
                    refreshRate--;
                    if (refreshRate < 0)
                        refreshRate = 0;
                };

            Fps fps = new Fps();

            int acc = 0;
            BulletParticleSystem bps = new BulletParticleSystem(50);
            SmokeParticleSystem sps = new SmokeParticleSystem();
            AsteroidParticleSystem aps = new AsteroidParticleSystem();
            // spawn asteroids
            for (int i = 0; i < 4; i++)
                aps.Add(Asteroid.Size.Large);

            Ship ship = new Ship(center, sps);

            Lcd.IsWrapMode = true;
            while (run)
            {
                // collect inputs
                input.Update(fps.ElaspedMilliseconds);

                // dispatch inputs
                ship.Input(input.Angle, input.IsPressed);

                // update game state
                ship.Update(fps.ElaspedMilliseconds);
                acc += fps.ElaspedMilliseconds;
                if (acc >= 750)
                {
                    acc = 0;
                    bps.Add(new Bullet(ship.TipPosition, ship.Angle));
                }

                bps.Update(fps.ElaspedMilliseconds);
                sps.Update(fps.ElaspedMilliseconds);
                aps.Update(fps.ElaspedMilliseconds);
                fps.Update();

                Lcd.Clear();

                // draw
                Renderer.DrawGeometry(ship.Geometry, true);
                bps.Draw(fps.ElaspedMilliseconds);
                sps.Draw(fps.ElaspedMilliseconds);
                aps.Draw(fps.ElaspedMilliseconds);

                Lcd.WriteText(Font.SmallFont, new Point(0, 0), fps.ToString(), true);
                Lcd.WriteText(Font.SmallFont, new Point(0, (int)Font.SmallFont.maxHeight), "Asteroids: " + aps.ActiveParticles, true);
                Lcd.Update();

                stopped.WaitOne(refreshRate);
            }
        }
    }
}
