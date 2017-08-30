using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Reflection;
using ShoppingCartLibrary.Models;

namespace ShoppingCartLibrary
{
    public static class Calculator
    {
        public static double GetSum<T>(
            this IEnumerable<T> shoppingList,Func<T, double> selector)
        {
            return shoppingList.Sum(selector);
        }
    }
}
