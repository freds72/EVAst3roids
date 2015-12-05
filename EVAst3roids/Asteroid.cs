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
        public static readonly int[] MinRadius = new int[] { 6, 12, 22};
        public static readonly int[] MaxRadius = new int[] { 8, 15, 30 };
        public static readonly int[] MinHP = new int[]{ 1, 3, 5 };

        Point _position;
        Point _direction;
        Point[] _geometry;
        int[] _radius;
        int _momentum;
        int _velocity;
        int _angle;
        int _hp;
        int _size;

        public int Radius { get { return _size; } }

        public Asteroid(Point pos, int angle, int size)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _size = size;
            _momentum = rnd.Next(-20, 20); // deg/sec
            _velocity = rnd.Next(5, 10);
            _angle = rnd.Next(0, 360);
            _radius = new int[8];
            for (int i = 0; i < _radius.Length; i++)
                _radius[i] = rnd.Next(MinRadius[(int)size], MaxRadius[(int)size]);
            _hp = MinHP[(int)size];

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
            get { return _hp > 0; }
        }

        public void Update(int dt)
        {
            _angle += (dt * _momentum) / 1000;

            _position.X += (dt * _direction.X * _velocity) / 1000;
            _position.Y -= (dt * _direction.Y * _velocity) / 1000;

            UpdateGeometry();
        }

        public bool Collide(Point p)
        {
        	if (!IsAlive)
        		return false;
            Point center = Position;
            int dx = (p.X - center.X);
            int dy = (p.Y - center.Y);
            int radius = MinRadius[(int)_size];
            if (dx * dx + dy * dy < (radius * radius))
                return true;

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
        
        public void Hit()
        {
        	_hp -= 1;
        }
    }
}
