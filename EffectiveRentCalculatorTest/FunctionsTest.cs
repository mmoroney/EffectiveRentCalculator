using System;
using EffectiveRentCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EffectiveRentCalculatorTest
{
    [TestClass]
    public class FunctionsTests
    {
        [TestMethod]
        public void TestSum()
        {
            for(double a = 0; a < 100; a++)
            {
                for(double r = 0; r < 2; r += 0.1)
                {
                    for(int n = 0; n < 100; n++)
                    {
                        FunctionsTests.TestSum(a, r, n);
                    }
                }
            }
        }

        private static void TestSum(double a, double r, int n)
        {
            double term = a;
            double sum = 0;

            for(int i = 0; i < n; i++)
            {
                sum += term;
                term *= r;
            }

            double expected = Functions.Sum(a, r, n);

            if (sum == 0)
                Assert.IsTrue(expected == 0);
            else
                Assert.IsTrue(Math.Abs(expected / sum - 1.0) < 1e-9);
        }
    }
}
