using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.Utilities
{
    class MathF
    {
        public static void Normalize(ref float vectorX, ref float vectorY)
        {
            if (vectorX == 0 && vectorY == 0)
                return;

            var distance = (float)Math.Sqrt(vectorX * vectorX + vectorY * vectorY);

            vectorX /= distance;
            vectorY /= distance;
        }

        public static float Dot(float vector1X, float vector1Y, float vector2X, float vector2Y)
        {
            return vector1X * vector2X + vector1Y * vector2Y;
        }
    }
}
