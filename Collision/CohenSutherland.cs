using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Collision
{
    [Flags]
    public enum Sector : Byte
    {
        CENTER = 0,
        LEFT = 1,
        RIGHT = 2,
        BOTTOM = 4,
        TOP = 8
    }

    public class CohenSutherland
    {
        private static Sector GetSector(float rectLeftX, float rectLeftY, float rectRightX, float rectRightY, float x, float y)
        {
            var sector = Sector.CENTER;
            if (x < rectLeftX)
                sector |= Sector.LEFT;
            else if (x > rectRightX)
                sector |= Sector.RIGHT;
            else if (y < rectLeftY)
                sector |= Sector.BOTTOM;
            else if (y > rectRightY)
                sector |= Sector.TOP;
            return sector;
        }
        public static bool Intersects(float rectX, float rectY, float width, float height, float lineStartX, float lineStartY, float lineEndX, float lineEndY)
        {
            var lineStartSector = GetSector(rectX, rectY, rectX + width, rectY + height, lineStartX, lineStartY);
            var lineEndSector = GetSector(rectX, rectY, rectX + width, rectY + height, lineEndX, lineEndY);

            if ((lineStartSector | lineEndSector) == 0 || lineStartSector == Sector.CENTER || lineEndSector == Sector.CENTER) //Line is (partially) inside rectangle
                return true;

            if ((lineStartSector & lineEndSector) != 0)
                return false;

            var combinedSector = lineStartSector | lineEndSector;
            float fromX = 0, fromY = 0, toX = 0, toY = 0;

            if((combinedSector & Sector.TOP) != 0)
            {
                fromX = rectX;
                fromY = rectY;
                toX = rectX + width;
                toY = rectY;
                return Collide.LineToLine(fromX, fromY, toX, toY, lineStartX, lineStartY, lineEndX, lineEndY);
            }
            else if((combinedSector & Sector.BOTTOM) != 0)
            {
                fromX = rectX;
                fromY = rectY + height;
                toX = rectX + width;
                toY = rectY + height;
                return Collide.LineToLine(fromX, fromY, toX, toY, lineStartX, lineStartY, lineEndX, lineEndY);
            }
            else if((combinedSector & Sector.LEFT) != 0)
            {
                fromX = rectX;
                fromY = rectY;
                toX = rectX;
                toY = rectY + height;
                return Collide.LineToLine(fromX, fromY, toX, toY, lineStartX, lineStartY, lineEndX, lineEndY);
            }
            else if((combinedSector & Sector.RIGHT) != 0)
            {
                fromX = rectX + width;
                fromY = rectY;
                toX = rectX + width;
                toY = rectY + height;
                return Collide.LineToLine(fromX, fromY, toX, toY, lineStartX, lineStartY, lineEndX, lineEndY);
            }

            return false;
        }
        public static bool Intersects(Rectangle rect, Vector2 lineStart, Vector2 lineEnd) { return Intersects(rect.X, rect.Y, rect.Width, rect.Height, lineStart.X, lineStart.Y, lineEnd.X, lineEnd.Y); }
        public static bool Intersects(RectCollider rect, Vector2 lineStart, Vector2 lineEnd) { return Intersects(rect.Position.X, rect.Position.Y, rect.Width, rect.Height, lineStart.X, lineStart.Y, lineEnd.X, lineEnd.Y); }
    }
}
