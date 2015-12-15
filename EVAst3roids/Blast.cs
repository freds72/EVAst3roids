using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    struct Blast : IParticle
    {
        static readonly Random rnd = new Random();
        static readonly int MaxVelocity = 35;
        
        Point _position;
        Point[] _geometry;
        Point _direction;
        public int LifeTime;
        int _velocity;

        public Blast(Point pos, int angle, int radius, int azimuth)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _position.X += radius * _direction.X;
            _position.Y -= radius * _direction.Y;
            _geometry = new Point[1];
            LifeTime = rnd.Next(750, 1000);
            _velocity = (Mathi.FixedCos(azimuth) * MaxVelocity) / Mathi.FixedScale;
        }

        public bool IsAlive
        {
            get { return LifeTime > 0; }
        }

        public void Update(int dt)
        {
            LifeTime -= dt;
            if (dt <= 0)
                return;

            _position.X += (dt * _direction.X * _velocity) / 1000;
            _position.Y -= (dt * _direction.Y * _velocity) / 1000;

            int x = _position.X / Mathi.FixedScale;
            int y = _position.Y / Mathi.FixedScale;
            _geometry[0].X = x;
            _geometry[0].Y = y;
        }

        public Point[] Geometry
        {
            get
            {
                return _geometry;
            }
        }

        public Point Position
        {
            get { return _geometry[0]; }
        }
        
        public void Hit()
        {
        }
    }
}
