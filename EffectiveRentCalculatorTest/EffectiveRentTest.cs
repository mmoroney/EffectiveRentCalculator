using System;
using EffectiveRentCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EffectiveRentCalculatorTest
{
    [TestClass]
    public class EffectiveRentTests
    {
        [TestMethod]
        public void TestEffectiveRent()
        {
            EffectiveRentTests.TestEffectiveRent(525000, 15000, 700, 0.04,
                0.03, 0.2, 0.06, 30, 0.28, 0.03, 0.06, 40);

            EffectiveRentTests.TestEffectiveRent(770000, 20000, 900, 0.05,
                0.06, 0.25, 0.07, 40, 0.28, 0.03, 0.06, 30);
        }

        private static void TestEffectiveRent(double purchasePrice,
            double closingCosts,
            double monthlyExpenses,
            double propertyTaxRate,
            double appreciationRate,
            double percentDown,
            double interestRate,
            int term,
            double marginalTaxRate,
            double inflationRate,
            double returnOnInvestments,
            int timeHorizon)
        {
            double effectiveRent = HousePurchase.EffectiveRent(purchasePrice, closingCosts, monthlyExpenses, propertyTaxRate,
                appreciationRate, percentDown, interestRate, term, marginalTaxRate, inflationRate,
                returnOnInvestments, timeHorizon);

            double effectiveOpportunityCost = 0;
            double currentRent = effectiveRent;

            double downPayment = purchasePrice * percentDown;
            double opportunityCost = downPayment + closingCosts;
            double remainingPrincipal = purchasePrice - downPayment;
            double mortgagePayment = Functions.Payment(remainingPrincipal, Functions.ToMonthlyRate(interestRate), 12 * term);

            double monthlyReturn = Functions.ToMonthlyRate(returnOnInvestments);
            double monthlyInflationRate = Functions.ToMonthlyRate(inflationRate);
            double monthlyInterestRate = Functions.ToMonthlyRate(interestRate);
            double currentMonthlyExpenses = monthlyExpenses;

            double houseValue = purchasePrice;

            for(int i = 1; i <= timeHorizon; i++)
            {
                double ytdInterest = 0;
                for(int j = 0; j < 12; j++)
                {
                    effectiveOpportunityCost *= (1.0 + monthlyReturn);
                    currentRent *= (1.0 + monthlyInflationRate);
                    effectiveOpportunityCost += currentRent;

                    opportunityCost *= (1.0 + monthlyReturn);
                    opportunityCost += currentMonthlyExpenses;

                    if (i <= term)
                    {
                        opportunityCost += mortgagePayment;
                        double interest = remainingPrincipal * monthlyInterestRate;
                        ytdInterest += interest;
                        remainingPrincipal += interest;
                        remainingPrincipal -= mortgagePayment;
                    }

                    currentMonthlyExpenses *= (1.0 + monthlyInflationRate);
                }

                houseValue *= (1.0 + appreciationRate);
                opportunityCost += houseValue * propertyTaxRate;
                opportunityCost -= ytdInterest * marginalTaxRate;
            }

            opportunityCost += remainingPrincipal;
            opportunityCost -= houseValue;

            AssertClose(effectiveOpportunityCost, opportunityCost);
        }

        private static void AssertClose(double expected, double actual)
        {
            Assert.IsTrue(Math.Abs(expected - actual) < 1e-5);
        }
    }
}
