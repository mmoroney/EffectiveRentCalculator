using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveRentCalculator
{
    public static class Functions
    {
        /// <summary>
        /// Calculates the sum of a geometric series
        /// </summary>
        /// <param name="a">The first term in the series.</param>
        /// <param name="r">The common ratio between successive terms.</param>
        /// <param name="n">The number of terms in the series.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Calculates the payment for a mortgage.
        /// </summary>
        /// <param name="p">Initial principal of the loan.</param>
        /// <param name="i">The interest rate per period.</param>
        /// <param name="n">The number of periods.</param>
        /// <returns></returns>
        public static double Payment(double p, double i, int n)
        {
            if (n <= 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than zero", nameof(n)));

            if (i <= 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than zero", nameof(i)));

            return p * i / (1 - Math.Pow(1 + i, -n));
        }

        /// <summary>
        /// Calculates the future value of an asset.
        /// </summary>
        /// <param name="a">Initial value of the asset.</param>
        /// <param name="i">The rate at which the asset increases per period.</param>
        /// <param name="n">The number of periods.</param>
        /// <returns></returns>
        public static double FutureValue(double a, double i, int n)
        {
            if (n < 0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than or equal to zero", nameof(n)));

            if (i < 0.0)
                throw new ArgumentOutOfRangeException(string.Format("{0} must be greater than or equal to zero", nameof(i)));

            return a * Math.Pow(1 + i, n);
        }

        /// <summary>
        /// Calculates the future value of a series of increasing payments
        /// </summary>
        /// <param name="a">Value of the initial payment.</param>
        /// <param name="i">Rate at which the amount of the payment increases per period.</param>
        /// <param name="r">Rate at which the value of all money paid increases per period.</param>
        /// <param name="n">The number of periods.</param>
        /// <returns></returns>
        public static double ProgressiveFutureValue(double a, double i, double r, int n)
        {
            return Functions.Sum(a * Math.Pow(1.0 + r, n - 1), (1.0 + i) / (1.0 + r), n);
        }
    }
}
