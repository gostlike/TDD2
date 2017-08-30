using ShoppingCartLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
            this IEnumerable<T> shoppingList, Func<T, double> selector)
        {
            return shoppingList.Sum(selector);
        }
        /// <summary>
        ///計算折扣後的總價格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shoppings">購買清單</param>
        /// <param name="discountRules">折扣規則集合</param>
        /// <param name="priceSelector">購買物品的價格屬性</param>
        /// <param name="groupBySelector">對購買物品分群的屬性</param>
        /// <returns></returns>
        public static double GetDiscountSum<T>(
            this IEnumerable<T> shoppings, IEnumerable<DiscountRule> discountRules, Func<T, double> priceSelector,
            Func<T, string> groupBySelector)
        {
            var resultList = new List<T>();
            var shoppingsList = shoppings.ToList();
            foreach (var rule in discountRules)
            {
                while (shoppingsList.GroupBy(groupBySelector).Count() >= rule.DifferentItemNumber)
                {
                    var diffList = shoppingsList.GroupBy(groupBySelector).SelectMany(g => g.Take(1)).Take(rule.DifferentItemNumber);
                    if (diffList.Count() >= rule.DifferentItemNumber)
                    {
                        foreach (var item in diffList)
                        {
                            PropertyInfo propertyInfo = item.GetType().GetProperty("Discount");
                            propertyInfo.SetValue(item, Convert.ChangeType(rule.Discount, propertyInfo.PropertyType), null);
                            resultList.Add(item);
                            shoppingsList.Remove(item);
                        }
                    }
                }
            }
            return resultList.Concat(shoppingsList).Sum(priceSelector);
        }
    }
}