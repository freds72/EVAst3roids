using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    struct Smoke : IParticle
    {
        static readonly Random rnd = new Random();
        public static readonly int MaxRadius = 3;
        
        Point[] _geometry;
        public int LifeTime;

        public Smoke(Point p)
        {
            _geometry = new Point[] { new Point(rnd.Next(p.X - 3,p.X+3), rnd.Next(p.Y -3, p.Y + 3)) };
            LifeTime = rnd.Next(2500, 4500); //ms
        }

        public int Radius
        {
            get { return Math.Max(1, (MaxRadius * LifeTime) / 3000); }
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
