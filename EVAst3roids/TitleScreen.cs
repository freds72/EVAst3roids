using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace EVAst3roids
{
    class TitleScreen : GameScreen
    {
        Bitmap _image;
        public TitleScreen(GameScreenManager manager):
            base(manager)
        {
        }

        public override void LoadAssets()
        {
            base.LoadAssets();
            _image = Bitmap.FromResource(Assembly.GetExecutingAssembly(), "EVAst3roids.Resources.title.bmp");
            this.Game.Gamepad.Buttons.EnterReleased += NextScreen;
        }

        private void NextScreen()
        {
            ScreenManager.Add(new MainGameScreen(ScreenManager));
            IsActive = false;
            // TODO: exit
        }

        public override void UnloadAssets()
        {
            _image = null;
            this.Game.Gamepad.Buttons.EnterReleased -= NextScreen;
            base.UnloadAssets();
        }

        public override void Draw(GameTime gameTime)
        {
            Lcd.Clear();
            Lcd.DrawBitmap(_image, Point.Zero);
            Lcd.Update();
        }
    }
}
