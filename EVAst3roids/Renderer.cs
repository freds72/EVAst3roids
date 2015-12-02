using MonoBrickFirmware.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EVAst3roids
{
    static class Renderer
    {
        static public void DrawGeometry(Point[] geometry, bool closed)
        {
            int lastIndex = geometry.Length - 1;
            // nothing to do?
            if (lastIndex <= 0)
                return;
            for (int i = 0; i < lastIndex; i++)
                Lcd.DrawLine(geometry[i], geometry[i + 1], true);
            if (closed)
                Lcd.DrawLine(geometry[lastIndex], geometry[0], true);
        }
    }
}
