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
        Asteroid.Size _size;

        /// <summary>
        /// Returns the asteroid type (0: small, 1: medium, 2: large)
        /// </summary>
        public Asteroid.Size Model { get { return _size; } }

        public Asteroid(Point pos, int angle, Asteroid.Size size)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _size = size;
            _momentum = rnd.Next(-5, 5); // deg/sec
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

            Renderer.Wrap(ref _position);

            UpdateGeometry();
        }

        bool Collide(ref Point center, ref Point p)
        {
            int dx = (p.X - center.X);
            int dy = (p.Y - center.Y);
            int radius = MinRadius[(int)_size];
            if (dx * dx + dy * dy < (radius * radius))
                return true;
            return false;
        }

        public bool Collide(IParticle other)
        {
        	if (!IsAlive)
        		return false;
            Point center = Position;
            Point p = other.Position;
            if (Collide(ref center, ref p))
                return true;

            // check for "ghost" particles
            IGhostParticle ghost = other as IGhostParticle;
            if ( ghost != null )
            {
                int n = ghost.GhostPositions.Length;
                for(int i=0;i<n;++i)
                {
                    if (Collide(ref center, ref ghost.GhostPositions[i]))
                        return true;
                }
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
        
        public void Hit()
        {
            _hp = 0;
        }
    }
}
