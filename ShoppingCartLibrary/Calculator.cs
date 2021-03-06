﻿using ShoppingCartLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ShoppingCartLibrary
{
    public static class Calculator
    {
        /// <summary>
        /// 計算商品沒有折扣的總價格
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

        ///  <summary>
        /// 計算商品在折扣後的總價格
        ///  </summary>
        ///  <typeparam name="T"></typeparam>
        ///  <param name="shoppings">購買清單</param>
        ///  <param name="discountRules">折扣規則集合</param>
        ///  <param name="priceSelector">購買物品的價格屬性</param>
        ///  <param name="groupBySelector">對購買物品分群的屬性</param>
        /// <returns></returns>
        private static double GetPrice<T>(
            this IEnumerable<T> shoppings, IEnumerable<DiscountRule> discountRules, Func<T, double> priceSelector, Func<T, string> groupBySelector)
        {
            var resultList = new List<T>();
            var shoppingsList = shoppings.ToList();
            if (shoppings == null || shoppings.Count() == 0)
            {
                return 0;
            }
            //依序套用 Discount
            foreach (var rule in discountRules)
            {
                while (shoppingsList.GroupBy(groupBySelector).Count() >= rule.DifferentItemNumber)
                {
                    var diffList = shoppingsList.GroupBy(groupBySelector).SelectMany(g => g.Take(1)).Take(rule.DifferentItemNumber);
                    if (diffList.Count() >= rule.DifferentItemNumber)
                    {
                        foreach (var item in diffList)
                        {
                            PropertyInfo discountProperty = item.GetType().GetProperty("Discount");
                            discountProperty.SetValue(item, Convert.ChangeType(rule.Discount, discountProperty.PropertyType), null);
                            resultList.Add(item);
                            shoppingsList.Remove(item);
                        }
                    }
                }
            }
            return resultList.Concat(shoppingsList).Sum(priceSelector);
        }

        /// <summary>
        ///計算書本在折扣後的總價格
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="shoppings">購買清單</param>
        /// <param name="discountRules">折扣規則集合</param>
        /// <returns></returns>
        public static double GetBookPrice(
            this IEnumerable<Book> shoppings, IEnumerable<DiscountRule> discountRules)
        {
            return GetPrice<Book>(
                shoppings, discountRules, book => book.Price, book => book.Name);
        }

        /// <summary>
        /// 計算書本最優惠總價格
        /// </summary>
        /// <param name="shoppings"></param>
        /// <param name="discountRules"></param>
        /// <returns></returns>
        public static double GetBookBestPrice(this IEnumerable<Book> shoppings, IEnumerable<DiscountRule> discountRules)
        {
            var bestprice = new List<double>();
            for (var item = 0; item < shoppings.Count(); item++)
            {
                bestprice.Add(GetBookPrice(shoppings, discountRules.Skip(item)));
            }
            return bestprice.Min();
        }
    }
}