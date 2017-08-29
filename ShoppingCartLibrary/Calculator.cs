using System;
using System.Collections.Generic;
using System.Linq;

namespace ShoppingCartLibrary
{
    public static class Calculator
    {
        public static int GetSum<T>(
            this IEnumerable<T> shoppingList,Func<T, int> selector)
        {
            return shoppingList.Sum(selector);
        }
    }
}
