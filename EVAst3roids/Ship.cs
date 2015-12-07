using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class Ship
    {
        Point _position;

        Point[] _geometry = new Point[3];
        int[] _angles = new int[] { 0, 120, 120+120};
        Point _velocity = new Point();

        public Point[] Geometry { get { return _geometry; } }
        public int Angle { get; private set; }

        public static readonly int Radius = 10;
        static readonly int MaxThrust = 15;
        static readonly int ThrustDuration = 2000; // ms
        static readonly int ThrustDecayRate = 250; // ms

        static readonly int FireDelay = 250; // ms
        static readonly int SmokeDelay = 125; // ms
        int _thrustTime = 0;
        int _fireTimer = -1;
        int _smokeTimer = -1;
        SmokeParticleSystem _sps;
        BulletParticleSystem _bps;

        public Ship(Game game, Point pos)
        {
            _bps = game.Services.TryFind<BulletParticleSystem>();
            _sps = game.Services.TryFind<SmokeParticleSystem>();
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y); ;
            UpdateGeometry();
        }

        void UpdateGeometry()
        {
            for(int i=0;i<3;i++)
            {
                _geometry[i].X = (_position.X + Radius * Mathi.FixedCos(Angle + _angles[i])) / Mathi.FixedScale;
                _geometry[i].Y = (_position.Y - Radius * Mathi.FixedSin(Angle + _angles[i])) / Mathi.FixedScale;
            }
        }

        public void Input(int angle, bool thrust, bool fire)
        {
            Angle = angle + 90; // tip of ship is pointing "up"
            if (thrust)
            {
                _thrustTime = ThrustDuration;                
            }
            if (fire && _fireTimer < 0)
            {
                _fireTimer = FireDelay;
                _bps.Add(new Bullet(TipPosition, Angle));
            }
        }

        public void Update(int dt)
        {
            _fireTimer -= dt;
            _thrustTime -= dt;
            _smokeTimer -= dt;
            int thrust = 0;
            if (_thrustTime > 0 )
            {
                thrust = Mathi.EaseOutCubic(_thrustTime, MaxThrust, -ThrustDecayRate, ThrustDuration);
            }

            _velocity.X = thrust * Mathi.FixedCos(Angle);
            _velocity.Y = thrust * Mathi.FixedSin(Angle);

            _position.X += (dt * _velocity.X) / 1000;
            _position.Y -= (dt * _velocity.Y) / 1000;

            Renderer.Wrap(ref _position);

            UpdateGeometry();

            if (_thrustTime > 0)
            {
                if (_smokeTimer <= 0)
                {
                    _smokeTimer = SmokeDelay;
                    _sps.Add(EnginePosition);
                }
            }
        }

        public Point TipPosition { get { return _geometry[0]; } }
        public Point EnginePosition { get { return new Point((_geometry[1].X + _geometry[2].X) / 2, (_geometry[1].Y + _geometry[2].Y) / 2); } }
    }
}
