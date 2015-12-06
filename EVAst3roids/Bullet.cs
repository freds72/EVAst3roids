using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    struct Bullet : IParticle, IGhostParticle
    {
        static readonly Random rnd = new Random();
        public static readonly int Length = 6;
        public static readonly int Velocity = 75;

        Point _position;
        Point[] _geometry;
        Point[] _ghosts;
        Point _direction;
        public int LifeTime;

        public Bullet(Point pos, int angle)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _geometry = new Point[2];
            _ghosts = new Point[8];
            LifeTime = rnd.Next(3500,4000);
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

            _position.X += (dt * _direction.X * Velocity) / 1000;
            _position.Y -= (dt * _direction.Y * Velocity) / 1000;

            Renderer.Wrap(ref _position);

            int x = _position.X / Mathi.FixedScale;
            int y = _position.Y / Mathi.FixedScale;

            _geometry[0].X = x;
            _geometry[0].Y = y;

            _geometry[1].X = x - (Length * _direction.X) / Mathi.FixedScale;
            _geometry[1].Y = y + (Length * _direction.Y) / Mathi.FixedScale;

            Renderer.Ghostify(_geometry[0], _ghosts);
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
        	LifeTime = 0;
        }

        #region Implements IGhostParticle
        public Point[] GhostPositions
        {
            get { return _ghosts; }
        }
        #endregion
    }
}
