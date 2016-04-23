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
                        double term = a;
                        double sum = 0;

                        for (int i = 0; i < n; i++)
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
        }

        [TestMethod]
        public void TestPayment()
        {
            for(double p = 0; p < 1000000; p += 10000)
            {
                for(double i = 0.01; i < 0.2; i += 0.01)
                {
                    for(int n = 1; n < 30; n++)
                    {
                        double balance = p;
                        double payment = Functions.Payment(p, i, n);

                        for(int j = 1; j <= n; j++)
                        {
                            balance *= (1.0 + i);
                            balance -= payment;
                        }

                        Assert.IsTrue(Math.Abs(balance) < 1e-5);
                    }
                }
            }
        }

        [TestMethod]
        public void TestFutureValue()
        {
            for(double a = 0.0; a < 100; a++)
            {
                for(double i = 0.0; i < 0.2; i += 0.05)
                {
                    for(int n = 1; n < 50; n++)
                    {
                        double value = a;
                        for(int j = 1; j <= n; j++)
                            value *= (1.0 + i);

                        double expected = Functions.FutureValue(a, i, n);

                        if (value == 0)
                            Assert.IsTrue(expected == 0);
                        else
                            Assert.IsTrue(Math.Abs(expected / value - 1.0) < 1e-9);
                    }
                }
            }
        }

        [TestMethod]
        public void TestProgressiveFutureValue()
        {
            for(double a = 0; a < 100; a++)
            {
                for(double i = 0.0; i < 0.2; i += 0.05)
                {
                    for(double r = 0.0; r < 0.2; r += 0.05)
                    {
                        for(int n = 1; n < 50; n++)
                        {
                            double value = 0;
                            double payment = a;

                            for(int j = 1; j <= n; j++)
                            {
                                value *= (1.0 + r);
                                value += payment;
                                payment *= (1.0 + i);
                            }

                            double expected = Functions.ProgressiveFutureValue(a, i, r, n);

                            if (value == 0)
                                Assert.IsTrue(expected == 0);
                            else
                                Assert.IsTrue(Math.Abs(expected / value - 1.0) < 1e-9);
                        }
                    }
                }
            }
        }
    }
}
