using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EffectiveRentCalculator
{
    public static class HousePurchase
    {
        public static double EffectiveRent(
            double purchasePrice,
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
            double downpayment = purchasePrice * percentDown;
            double principal = purchasePrice - downpayment;
            double mortgagePayment = Functions.Payment(principal, Functions.ToMonthlyRate(interestRate), term * 12);

            double totalOpportunityCost = HousePurchase.DownpaymentOpportunityCost(downpayment, returnOnInvestments, timeHorizon);
            totalOpportunityCost += HousePurchase.ClosingOpportunityCost(closingCosts, returnOnInvestments, timeHorizon);
            totalOpportunityCost += HousePurchase.MortagePaymentOpportunityCost(mortgagePayment, term, returnOnInvestments, timeHorizon);
            totalOpportunityCost += HousePurchase.MonthlyExpensesOpportunityCost(monthlyExpenses, inflationRate, returnOnInvestments, timeHorizon);
            totalOpportunityCost += HousePurchase.PropertyTaxOpportunityCost(purchasePrice, propertyTaxRate, appreciationRate, returnOnInvestments, timeHorizon);
            totalOpportunityCost += HousePurchase.RemainingPrincipal(mortgagePayment, principal, interestRate, term, timeHorizon);
            totalOpportunityCost -= HousePurchase.HouseValue(purchasePrice, appreciationRate, timeHorizon);
            totalOpportunityCost -= HousePurchase.TaxRefundValue(principal, mortgagePayment, interestRate, term, marginalTaxRate, returnOnInvestments, timeHorizon);

            return HousePurchase.EffectiveMonthlyCost(totalOpportunityCost, inflationRate, returnOnInvestments, timeHorizon);
        }

        private static double DownpaymentOpportunityCost(double downpayment, double returnOnInvestments, int timeHorizon)
        {
            return Functions.FutureValue(downpayment, returnOnInvestments, timeHorizon);
        }

        private static double ClosingOpportunityCost(double closingCosts, double returnOnInvestments, int timeHorizon)
        {
            return Functions.FutureValue(closingCosts, returnOnInvestments, timeHorizon);
        }

        private static double MortagePaymentOpportunityCost(double mortgagePayment, int term, double returnOnInvestments, int timeHorizon)
        {
            double mortgagePaymentOpportunityCost = Functions.Sum(mortgagePayment, Functions.ToMonthlyRate(returnOnInvestments) + 1.0, 12 * Math.Min(term, timeHorizon));
            if (timeHorizon > term)
                mortgagePaymentOpportunityCost = Functions.FutureValue(mortgagePaymentOpportunityCost, returnOnInvestments, timeHorizon - term);

            return mortgagePaymentOpportunityCost;
        }

        private static double MonthlyExpensesOpportunityCost(double monthlyExpenses, double inflationRate, double returnOnInvestments, int timeHorizon)
        {
            return Functions.ProgressiveFutureValue(monthlyExpenses, Functions.ToMonthlyRate(inflationRate), Functions.ToMonthlyRate(returnOnInvestments), timeHorizon * 12);
        }

        private static double PropertyTaxOpportunityCost(double purchasePrice, double propertyTaxRate, double appreciationRate, double returnOnInvestments, int timeHorizon)
        {
            return Functions.ProgressiveFutureValue(purchasePrice * propertyTaxRate * (1.0 + appreciationRate), appreciationRate, returnOnInvestments, timeHorizon);
        }

        private static double RemainingPrincipal(double mortgagePayment, double principal, double interestRate, int term, int timeHorizon)
        {
            if (timeHorizon >= term)
                return 0;

            return Functions.FutureValue(principal, Functions.ToMonthlyRate(interestRate), 12 * timeHorizon) - Functions.Sum(mortgagePayment, 1.0 + Functions.ToMonthlyRate(interestRate), 12 * timeHorizon);
        }

        private static double HouseValue(double purchasePrice, double appreciationRate, int timeHorizon)
        {
            return Functions.FutureValue(purchasePrice, appreciationRate, timeHorizon);
        }

        private static double TaxRefundValue(double principal, double mortgagePayment, double interestRate, int term, double marginalTaxRate, double returnOnInvestments, int timeHorizon)
        {
            int n = Math.Min(term, timeHorizon);
  
            double value = (Functions.Sum(12 * mortgagePayment, 1.0 + returnOnInvestments, n) + Functions.ProgressiveFutureValue((principal - mortgagePayment / Functions.ToMonthlyRate(interestRate)) * interestRate, interestRate, returnOnInvestments, n)) * marginalTaxRate;

            if(timeHorizon > term)
                value = Functions.FutureValue(value, returnOnInvestments, timeHorizon - term);

            return value;
        }

        private static double EffectiveMonthlyCost(double opportunityCost, double inflationRate, double returnOnInvestments, int timeHorizon)
        {
            return opportunityCost / (Functions.ProgressiveFutureValue(1.0 + Functions.ToMonthlyRate(inflationRate), Functions.ToMonthlyRate(inflationRate), Functions.ToMonthlyRate(returnOnInvestments), 12 * timeHorizon));
        }
    }
}
