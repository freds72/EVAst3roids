using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class ThisGame : Game
    {
        BulletParticleSystem _bps;
        SmokeParticleSystem _sps;
        AsteroidParticleSystem _aps;
        Ship _ship;
        Gamepad _input = new Gamepad();
        int _burstX = Lcd.Width - Font.SmallFont.TextSize("BURST").X;

        public ThisGame()
        {
        }

        public override void Initialize()
        {
 	        base.Initialize();

            Point center = new Point(Lcd.Width / 2, Lcd.Height / 2);

            _bps = new BulletParticleSystem(this, 50);
            _sps = new SmokeParticleSystem(this);
            _aps = new AsteroidParticleSystem(this);

            // spawn asteroids
            for (int i = 0; i < 4; i++)
                _aps.Add(Asteroid.Size.Large);

            _ship = new Ship(this, center);

            Lcd.IsWrapMode = true;
        }

        protected override void Update(GameTime gameTime)
        {
            // collect inputs
            _input.Update(gameTime.ElapsedGameTime);

            // dispatch inputs
            _ship.Input(_input.Angle, _input.IsPressedLong, _input.IsPressed);

            // update game state
            _ship.Update(gameTime.ElapsedGameTime);

            _bps.Update(gameTime.ElapsedGameTime);
            _sps.Update(gameTime.ElapsedGameTime);
            _aps.Update(gameTime.ElapsedGameTime);           

            _aps.ResolveCollision(_bps);
        }

        protected override void Draw(GameTime gameTime)
        {
            Lcd.Clear();

            // draw
            Renderer.DrawGeometry(_ship.Geometry, true);
            _bps.Draw(gameTime.ElapsedGameTime);
            _sps.Draw(gameTime.ElapsedGameTime);
            _aps.Draw(gameTime.ElapsedGameTime);

            Lcd.WriteText(Font.SmallFont, new Point(0, 0), gameTime.ToString(), true);

            if ( _input.IsBeforePressedLong && (gameTime.ElapsedGameTime % 2) == 0 )
                Lcd.WriteText(Font.SmallFont, new Point(_burstX, 0), "BURST", true);

            Lcd.Update();
        }
    }
}
