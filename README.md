DiscountRefactoringDemo
=======================

A refactoring project to help a student to see alternative ways of the object-oriented world.

My idea was to:
- have code that is better readable
- make a change f.i. adding a new 'discount' very easy
- which means: be open for extension, but closed for modification
- DRY
- avoid if statments

I didn't split the classes in seperate files (normally I would) for presentation reasons.


This was the original [file](https://github.com/cBashTN/DiscountRefactoringDemo/blob/master/DiscountDemo/OriginalProgram.cs):
~~~csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ch._4_Project
{
    class Program
    {
        static void Main(string[] args)
        {//Declaring variables int units; double retailCost; double totalCost; double discount_T1; double discount_T2; double discount_T3; double discount_T4;
            //initializing variables
            retailCost = 99;
            discount_T1 = .8;
            discount_T2 = .7;
            discount_T3 = .6;
            discount_T4 = .5;

            //creating modules to run tasks
            units = getUnits();
            totalCost = getTotalCost(units, retailCost, discount_T1, discount_T2, discount_T3, discount_T4);

            if (units <= 9)
            {
                Console.WriteLine("You have completed your purchase!\nThe total for this transaction is {0:C}.", totalCost);
            }
            if (units <= 19 && units >= 10)
            {
                Console.WriteLine("Congratulations, you were eligible for a 20% discount!\nYour total comes to: {0:C}.", totalCost);
            }
            if (units <= 49 && units >= 20)
            {
                Console.WriteLine("Congratulations, you were eligible for a 30% discount!\nYour total comes to: {0:C}.", totalCost);
            }
            if (units <= 99 && units >= 50)
            {
                Console.WriteLine("Congratulations, you were eligible for a 40% discount!\nYour total comes to: {0:C}.", totalCost);
            }
            if (units >= 100)
            {
                Console.WriteLine("You were eligible for our best discount of 50%!\nYour total purchase comes to: {0:C}", totalCost);
            }

        }//end Main

        //next we're going to get the number of units the customer would like to purchase.
        static int getUnits()
        {
            int valueEntered;
            Console.WriteLine("Please enter the number of copies of our life-changing and super-inexpensive\nsoftware you would like to purchase:");
            while (!int.TryParse(Console.ReadLine(), out valueEntered)) ;
            return valueEntered;
        }

        //We're going to have our number of copies wanted by the user now.  
        //Following their entry, we now need to see if they're eligible for a discount.
        static double getTotalCost(int units, double retailCost, double discount_T1, double discount_T2, double discount_T3, double discount_T4)
        {
            double cost;
            cost = 0;
            if (units <= 9)//Here is where we calculate the cost when the units are less than 10, so there is no discount applied.
            {
                cost = units * retailCost;
            }
            if (units <= 19 && units >= 10)//Calculate first tier discount @ 20% for unit quantity 10-19
            {
                cost = (units * retailCost) * discount_T1;
            }
            if (units <= 49 && units >= 20)//Calculate first tier discount @ 30% for unit quantity 20-49
            {
                cost = (units * retailCost) * discount_T2;
            }
            if (units <= 99 && units >= 50)//Calculate first tier discount @ 40% for unit quantity 50-99
            {
                cost = (units * retailCost) * discount_T3;
            }
            if (units >= 100)//Calculate first tier discount @ 50% for unit quantity higher than 100
            {
                cost = (units * retailCost) * discount_T4;
            }
            return cost;//return the cost dependent on which tier the user was qualified for.
        }

    }
}
~~~
