using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlatEarth.DataStructures
{
    public struct Percentage
    {
        public float ZeroToOneValue;
        public float OneToOneHundredValue;

        public Percentage(float value)
        {
            if (value < 0 | value > 100)
                throw new ArgumentException("Percentage must be between 0 and 100.");

            OneToOneHundredValue = value;
            ZeroToOneValue = OneToOneHundredValue / 100;
        }

        public static int operator *(Percentage percentage, int number) => (int)(number * percentage.ZeroToOneValue);
        public static int operator *(int number, Percentage percentage) => (int)(number * percentage.ZeroToOneValue);
        public static float operator *(Percentage percentage, float number) => number * percentage.ZeroToOneValue;
        public static float operator *(float number, Percentage percentage) => number * percentage.ZeroToOneValue;
        public static double operator *(Percentage percentage, double number) => number * percentage.ZeroToOneValue;
        public static double operator *(double number, Percentage percentage) => number * percentage.ZeroToOneValue;
    }
}
