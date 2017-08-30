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
        /// <summary>
        /// 計算價格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shoppingList"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static double GetSum<T>(
            this IEnumerable<T> shoppingList,Func<T, double> selector)
        {
            return shoppingList.Sum(selector);
        }

        /// <summary>
        /// 計算折扣後的總價格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shoppings">購買清單</param>
        /// <param name="discountRule">折扣規則</param>
        /// <param name="priceSelector">購買物品的價格屬性</param>
        /// <param name="groupBySelector">對購買物品分群的屬性</param>
        /// <returns></returns>
        public static double GetDiscountSum<T>(
            this IEnumerable<T> shoppings, DiscountRule discountRule ,Func<T, double> priceSelector,
            Func<T,string> groupBySelector)
        {
            var resultList = new List<T>();
            var shoppingsList = shoppings.ToList();
            while (shoppingsList.GroupBy(groupBySelector).Count()>= discountRule.DifferentItemNumber)
            {
               var diffList = shoppingsList.GroupBy(groupBySelector).SelectMany(g => g.Take(1)).Take(discountRule.DifferentItemNumber);
               if (diffList.Count() >= discountRule.DifferentItemNumber)
               {   
                    foreach (var item in diffList)
                    {
                        PropertyInfo propertyInfo = item.GetType().GetProperty("Discount");
                        propertyInfo.SetValue(item, Convert.ChangeType(discountRule.Discount, propertyInfo.PropertyType),null);
                        resultList.Add(item);
                        shoppingsList.Remove(item);
                    }
               }
            }
            return resultList.Concat(shoppingsList).Sum(priceSelector);
        }
    }
}
