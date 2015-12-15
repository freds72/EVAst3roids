using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class MainGameScreen : GameScreen
    {
        BulletParticleSystem _bps;
        SmokeParticleSystem _sps;
        AsteroidParticleSystem _aps;
        BlastParticleSystem _blastPS;
        Ship _ship;
        readonly int _burstWidth;
        readonly int _burstX;

        public MainGameScreen(GameScreenManager manager):
            base(manager)
        {
            _burstWidth = Font.SmallFont.TextSize("BURST").X;
            _burstX = Lcd.Width - _burstWidth;
        }

        public override void LoadAssets()
        {
            Point center = new Point(Lcd.Width / 2, Lcd.Height / 2);

            _bps = new BulletParticleSystem(this.Game, 50);
            _sps = new SmokeParticleSystem(this.Game);
            _blastPS = new BlastParticleSystem(this.Game);
            _aps = new AsteroidParticleSystem(this.Game);

            // spawn asteroids
            for (int i = 0; i < 4; i++)
                _aps.Add(Asteroid.Size.Large);

            _ship = new Ship(this.Game, center);

            Lcd.IsWrapMode = true;
        }

        public override void UnloadAssets()
        {
            Lcd.IsWrapMode = false;
            Game.Services.Clear();
            base.UnloadAssets();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // dispatch inputs
            _ship.Input(Game.Gamepad.Angle, Game.Gamepad.IsPressedLong, Game.Gamepad.IsPressed);

            // update game state
            _ship.Update(gameTime.ElapsedGameTime);

            _bps.Update(gameTime.ElapsedGameTime);
            _sps.Update(gameTime.ElapsedGameTime);
            _aps.Update(gameTime.ElapsedGameTime);
            _blastPS.Update(gameTime.ElapsedGameTime);           

            _aps.ResolveCollision(_bps);
        }

        // int _movieTimer = 3000;
        public override void Draw(GameTime gameTime)
        {
            Lcd.Clear();

            // draw
            Renderer.DrawGeometry(_ship.Geometry, true);
            _bps.Draw(gameTime.ElapsedGameTime);
            _sps.Draw(gameTime.ElapsedGameTime);
            _aps.Draw(gameTime.ElapsedGameTime);
            _blastPS.Draw(gameTime.ElapsedGameTime);
                      
            Lcd.WriteText(Font.SmallFont, new Point(0, 0), gameTime.ToString(), true);
            
            int burstY = (int)Font.SmallFont.maxHeight/2;
            if (Game.Gamepad.IsBeforePressedLong == false)
                Lcd.DrawRectangle(new Rectangle(new Point(_burstX, burstY - 4), new Point(_burstX + (_burstWidth * Game.Gamepad.PressDuration) / Gamepad.LongPressDuration, burstY + 4)), true, true);
            else if ((gameTime.TotalGameTime % 2) == 0 )
                Lcd.WriteText(Font.SmallFont, new Point(_burstX, 0), "BURST", true);
            
            Lcd.Update();

            /*
            _movieTimer -= gameTime.ElapsedGameTime;
            if ( _movieTimer > 0 && (gameTime.TotalGameTime % 8) == 0)
            {
                Lcd.TakeScreenShot();
            }
             * */
        }
    }
}
