using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Ch4_Project
{
    public abstract class Discount
    {
        public double DiscountFactor;
        public double MinimumUnitCount;
        public string PurchaseCompletionText;

        protected const string NoDiscountCongratulationText = "You have completed your purchase!"; 
        protected const string DefaultDiscountCongratulationText = "Congratulations, you were eligible for a discount of ";
        protected const string BestDiscountCongratulationText = "You were eligible for our best discount of ";
        protected const string TotalSummaryText = "Your total comes to: ";
        protected string DiscountFactorInPercentage{ get { return (100 - DiscountFactor*100) + "% "; }}
        protected string PercentageAndTotalSummaryText{get { return string.Concat(DiscountFactorInPercentage, Environment.NewLine, TotalSummaryText); }}
    }

    class DiscountTier0 : Discount
    {
        public DiscountTier0()
        {
            DiscountFactor = 1.0;
            MinimumUnitCount = 0;
            PurchaseCompletionText = string.Concat(
                NoDiscountCongratulationText,
                Environment.NewLine,
                TotalSummaryText);   
        }
    }

    class DiscountTier1 : Discount
    {
        public DiscountTier1()
        {
            DiscountFactor = 0.8;
            MinimumUnitCount = 10;
            PurchaseCompletionText = string.Concat(DefaultDiscountCongratulationText, PercentageAndTotalSummaryText);     ;
        }
    }

    class DiscountTier2 : Discount
    {
        public DiscountTier2()
        {
            DiscountFactor = 0.7;
            MinimumUnitCount = 20;
            PurchaseCompletionText = string.Concat(DefaultDiscountCongratulationText, PercentageAndTotalSummaryText);
        }
    }

    class DiscountTier3 : Discount
    {
        public DiscountTier3()
        {
            DiscountFactor = 0.6;
            MinimumUnitCount = 50;
            PurchaseCompletionText = string.Concat(DefaultDiscountCongratulationText, PercentageAndTotalSummaryText);
        }
    }

    class DiscountTier4 : Discount
    {
        public DiscountTier4()
        {
            DiscountFactor = 0.5;
            MinimumUnitCount = 100;
            PurchaseCompletionText = string.Concat(BestDiscountCongratulationText, PercentageAndTotalSummaryText);
        }
    }

    public class Purchase
    {
        public int SingleRetailCost = 99;
        private Discount _discount;

        public Discount Discount { get { return _discount; } set { _discount = value; } }

        private readonly int _unitCount;

        private static List<Discount> AllDiscounts
        {
            get
            {
                var allDiscounts = new List<Discount>
                {
                    new DiscountTier0(),
                    new DiscountTier1(),
                    new DiscountTier3(),
                    new DiscountTier2(),
                    new DiscountTier4()
                };
                return allDiscounts;
            }
        }

        public Purchase(int unitCount)
        {
            _unitCount = unitCount;
            _discount = GetDiscountFor(unitCount);
        }

        private static Discount GetDiscountFor(int unitCount)
        {
            Discount discount = null;

            // Doesn't have to be sorted..
            var allDiscounts = AllDiscounts;

            //..as we sort it anyway
            var allDiscountsSorted = allDiscounts.OrderByDescending(x => x.MinimumUnitCount);

            foreach (Discount d in allDiscountsSorted)
            {
                if (HasDiscountTheCorrectMinimumUnitCount(d.MinimumUnitCount,unitCount))
                {
                    discount = d;
                    break;
                }
            }

            //TODO: Check if there are two MinimumUnitCount with the same value -> CodeContracts?
 
            return discount ?? (new DiscountTier0());
        }

        private static bool HasDiscountTheCorrectMinimumUnitCount(double minUnitCount, int unitCount)
        {
            return unitCount > minUnitCount;
        }


        public decimal GetTotalCost()
        {
            return _unitCount * SingleRetailCost * (decimal)_discount.DiscountFactor;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            int unitCount = GetUnitCount();
            var purchase = new Purchase(unitCount);
            decimal totalCost = purchase.GetTotalCost();

            Console.WriteLine(purchase.Discount.PurchaseCompletionText +"{0:C}", totalCost);
            /*Console.WriteLine("unitCount: " + unitCount + " Normal price: " + unitCount * purchase.SingleRetailCost +
                              " _discount: " +
                              100 * ((unitCount * purchase.SingleRetailCost) - (totalCost)) /
                              (unitCount * purchase.SingleRetailCost) + " %");
             */
            Console.ReadLine();
        }

        private static int GetUnitCount()
        {
            int valueEntered;
            Console.WriteLine("Please enter the number of copies of our life-changing and super-inexpensive\nsoftware you would like to purchase:");
            while (!int.TryParse(Console.ReadLine(), out valueEntered) || valueEntered<1)
            {
                Console.WriteLine("Please enter a number higher than 0!");
            }
            return valueEntered;
        }
    }
}