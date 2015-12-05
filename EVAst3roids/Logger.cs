using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    static class Logger
    {
        static int _line = 0;
        public static void Info(string log)
        {
            Lcd.WriteText(Font.SmallFont, new Point(0, _line), log, true);
            Lcd.Update();
            _line += (int)Font.SmallFont.maxHeight;
            if (_line > Lcd.Height)
            {
                Lcd.Clear();
                _line = 0;
            }
        }

        public static void Error(string log)
        {
            Lcd.Clear();
            Lcd.WriteText(Font.SmallFont, new Point(0, 0), log, true);
            Lcd.Update();
        }

        public static void Error(Exception e)
        {
            Lcd.Clear();
            int y = 0;
            while (e != null)
            {
                Lcd.WriteText(Font.SmallFont, new Point(0, y), e.StackTrace, true);
                y += (int)Font.SmallFont.maxHeight;
                e = e.InnerException;
            }
            Lcd.Update();
        }
    }
}
