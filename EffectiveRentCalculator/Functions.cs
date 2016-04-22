using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveRentCalculator
{
    public static class Functions
    {
        public static double Sum(double a, double r, int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than or equal to zero", nameof(n)));

            if (r < 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than or equal to zero", nameof(r)));

            if (r == 1.0)
                return a * n;

            return a * (Math.Pow(r, n) - 1) / (r - 1);
        }
    }
}
