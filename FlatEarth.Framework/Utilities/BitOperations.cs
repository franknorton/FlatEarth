using System;
using System.Collections.Generic;
using System.Text;

namespace FlatEarth.Utilities
{
    static class BitOperations
    {
        public static bool Equals(byte bits, byte mask)
        {
            return bits == mask;
        }

        public static bool ContainsAny(byte bits, byte mask)
        {
            return (bits & mask) != 0;
        }

        public static bool ContainsAll(byte bits, byte mask)
        {
            return (bits & mask) == mask;
        }
    }
}
