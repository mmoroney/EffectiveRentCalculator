using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveRentCalculator
{
    internal static class Utilities
    {
        public static void VerifyGreaterThanZero(string name, double value)
        {
            if (value <= 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than zero. Value: {1}", name, value));
        }

        public static void VerifyNotNegative(string name, double value)
        {
            if (value < 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} cannot be negative. Value: {1}", name, value));
        }

        public static void VerifyGreaterThanZero(string name, int value)
        {
            if (value <= 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than zero. Value: {1}", name, value));
        }

        public static void VerifyNotNegative(string name, int value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} cannot be negative. Value: {1}", name, value));
        }
    }
}
