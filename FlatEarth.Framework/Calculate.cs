using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth
{
    public static class Calculate
    {
        public static float Angle(Vector2 from, Vector2 to)
        {
            return (float)Math.Atan2(to.Y - from.Y, to.X - from.X);
        }
        public static Vector2 AngleToVector(float radians, float length)
        {
            return new Vector2((float)Math.Cos(radians) * length, (float)Math.Sin(radians) * length);
        }
        public static float VectorToAngle(Vector2 vectorDirection)
        {
            return (float)Math.Atan2(vectorDirection.Y, vectorDirection.X);
        }
        public static Vector2 Perpendicular(this Vector2 vector)
        {
            return new Vector2(-vector.Y, vector.X);
        }
    }
}
