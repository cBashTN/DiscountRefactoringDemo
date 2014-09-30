using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ch4_Project.Tests
{
    [TestClass()]
    public class PurchaseTests
    {
        [TestMethod()]
        public void PurchaseTest()
        {
            int[] unitCounts = {  -1,   0,   1,   2,  10,  11,  20,  21,  50,  51, 100, 101, int.MaxValue};
            double[] expected= {1.0, 1.0, 1.0, 1.0, 1.0, 0.8, 0.8, 0.7, 0.7, 0.6, 0.6, 0.5, 0.5 };
            
            var actual = new double[unitCounts.Length];

            for (var i = 0; i < unitCounts.Length; i++)
            {
                var purchase = new Purchase(unitCounts[i]);
                actual[i] = purchase.Discount.DiscountFactor;
            }


            CollectionAssert.AreEqual(expected,actual);
           
        }

    }
}

