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

        const int FixedWidth = 178 * Mathi.FixedScale;
        const int FixedHeight = 128 * Mathi.FixedScale;
        static public void Wrap(ref Point pos)
        {
            pos.X %= FixedWidth;
            if (pos.X < 0)
                pos.X += FixedWidth;
            pos.Y %= FixedHeight;
            if (pos.Y < 0)
                pos.Y += FixedHeight;
        }

        static Point[] corners = new Point[] { 
            new Point(-Lcd.Width, -Lcd.Height),
            new Point(0, -Lcd.Height),
            new Point(Lcd.Width, -Lcd.Height),
            new Point(Lcd.Width, 0),
            new Point(Lcd.Width, Lcd.Height),
            new Point(0, Lcd.Height),
            new Point(-Lcd.Width, Lcd.Height),
            new Point(-Lcd.Width, 0)
        };
        static public void Ghostify(Point center, Point[] ghosts)
        {
            for(int i=0;i<8;++i)
            {
                ghosts[i].X = center.X + corners[i].X;
                ghosts[i].Y = center.Y + corners[i].Y;
            }
        }
    }
}
