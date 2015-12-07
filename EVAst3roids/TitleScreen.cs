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
        public override void LoadAssets()
        {
            base.LoadAssets();
            _image = Bitmap.FromResouce(Assembly.GetExecutingAssembly(), "Assets/title.bmp");
        }

        public override void Draw(GameTime gameTime)
        {
            Lcd.Clear();
            Lcd.DrawBitmap(_image, Point.Zero);
            Lcd.Update();
        }
    }
}
