using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    abstract class ParticleSystem<T> where T:struct, IParticle
    {
        T[] _particles;
        int _activeParticles;
        int _maxParticles;
        public ParticleSystem(int max)
        {
            _maxParticles = max;
            _activeParticles = 0;
            _particles = new T[max];
        }

        public T[] Particles { get { return _particles; } }
        public int ActiveParticles { get { return _activeParticles; } }
        public void Update(int dt)
        {
            int n = _activeParticles;
            _activeParticles = 0;
            for (int i = 0; i < n; i++)
            {
                _particles[i].Update(dt);
                if (_particles[i].IsAlive)
                {
                    _particles[_activeParticles++] = _particles[i];
                }
            }
        }

        public void Add(T particle)
        {
            if ( _activeParticles < _maxParticles)
                _particles[_activeParticles++] = particle;
        }

        public void Draw(int dt)
        {
             for (int i = 0; i < _activeParticles; i++)
             {
                 Draw(ref _particles[i]);
             }
        }

        abstract public void Draw(ref T particle);
    }
}
