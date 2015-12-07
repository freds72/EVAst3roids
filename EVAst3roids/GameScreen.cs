using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    class GameScreen
    {
        public bool IsActive = true;

        public GameScreen()
        {
        }

        public virtual void LoadAssets() { }
        public virtual void Update(GameTime gameTime) { }
        public virtual void Draw(GameTime gameTime) { }
        public virtual void UnloadAssets() { }
    }
}
