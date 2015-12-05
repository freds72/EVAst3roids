using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameServiceContainer
    {
        Dictionary<Type, object> _components = new Dictionary<Type,object>();

        public void Register(object component)
        {
            _components.Add(component.GetType(), component);
        }

        public void Register(Type type, object component)
        {
            _components.Add(type, component);
        }

        public T TryFind<T>() where T : class
        {
            object o = null;
            _components.TryGetValue(typeof(T), out o);
            return o as T;
        }
    }
}