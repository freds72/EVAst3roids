﻿using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    struct Bullet : IParticle
    {
        static readonly Random rnd = new Random();
        public static readonly int Length = 5;
        public static readonly int Velocity = 25;

        Point _position;
        Point[] _geometry;
        Point _direction;
        public int LifeTime;

        public Bullet(Point pos, int angle)
        {
            _position = new Point(Mathi.FixedScale * pos.X, Mathi.FixedScale * pos.Y);
            _direction = new Point(Mathi.FixedCos(angle), Mathi.FixedSin(angle));
            _geometry = new Point[2];
            LifeTime = rnd.Next(2500,3000);
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

            int x = _position.X / Mathi.FixedScale;
            int y = _position.Y / Mathi.FixedScale;
            _geometry[0].X = x;
            _geometry[0].Y = y;

            _geometry[1].X = x - (Length * _direction.X) / Mathi.FixedScale;
            _geometry[1].Y = y + (Length * _direction.Y) / Mathi.FixedScale;
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
    }
}