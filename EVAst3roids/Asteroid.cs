using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    struct Asteroid : IParticle
    {
        public enum Size
        {
            Small = 0,
            Medium = 1,
            Large = 2
        }

        static readonly Random rnd = new Random();
        public static readonly int[] MinRadius = new int[]{8,12,18};
        public static readonly int[] MaxRadius = new int[] { 10, 20, 30 };
        public static readonly int MaxVelocity = 1;

        Point _position;
        Point _direction;
        Point[] _geometry;
        int[] _radius;
        int _momentum;
        int _angle;
        Size _size;
        bool _isAlive;

        public Asteroid(Point pos, int angle, Size size)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _size = size;
            _momentum = rnd.Next(-10, 10); // deg/sec
            _angle = rnd.Next(0, 360);
            _radius = new int[8];
            for (int i = 0; i < _radius.Length; i++)
                _radius[i] = rnd.Next(MinRadius[(int)size], MaxRadius[(int)size]);
            _isAlive = true;

            //
            _geometry = new Point[8];
            UpdateGeometry();
        }

        void UpdateGeometry()
        {
            int angle = 0;
            for (int i = 0; i < _geometry.Length; i++)
            {
                _geometry[i].X = (_position.X + _radius[i] * Mathi.FixedCos(_angle + angle)) / Mathi.FixedScale;
                _geometry[i].Y = (_position.Y - _radius[i] * Mathi.FixedSin(_angle + angle)) / Mathi.FixedScale;
                angle += (360 / _geometry.Length);
            }
        }

        public bool IsAlive
        {
            get { return _isAlive; }
        }

        public void Update(int dt)
        {
            _angle += (dt * _momentum) / 1000;

            _position.X += (dt * _direction.X * MaxVelocity) / 1000;
            _position.Y -= (dt * _direction.Y * MaxVelocity) / 1000;

            UpdateGeometry();
        }

        public bool Collide(Point p)
        {
            Point center = Position;
            int dx = (p.X - center.X);
            int dy = (p.Y - center.Y);
            int radius = MinRadius[(int)_size];
            if (dx * dx + dy * dy < (radius * radius))
            {
                _isAlive = false;
                return true;
            }
            return false;
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
            get { return new Point(_position.X / Mathi.FixedScale, _position.Y / Mathi.FixedScale); }
        }
    }
}
