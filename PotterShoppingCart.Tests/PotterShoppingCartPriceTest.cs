using System.Collections.Generic;
using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartLibrary;
using ShoppingCartLibrary.Models;

namespace PotterShoppingCart.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod] 
        public void PotterShoppingCart價格計算測試_第一集買一本_無滿足任何優惠_價格為100()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1)
            };
            var discountrule =
                new DiscountRule() { Discount = 0.95, DifferentItemNumber = 2 };
            double expected = 100;
            //var actual = pottershoppingcart.GetSum(book => book.Price);
            var actual = pottershoppingcart.GetDiscountSum<Book>(discountrule, sumBy => sumBy.Price, groupby => groupby.Name);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一集買一本_第二集買一本_滿足95折優惠_價格為190()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1)
            };
            var discountrule =
                new DiscountRule() { Discount = 0.95, DifferentItemNumber = 2 };

            double expected = 190;
            var actual = pottershoppingcart.GetDiscountSum<Book>(discountrule, sumBy => sumBy.Price, groupby => groupby.Name);
            expected.ToExpectedObject().ShouldEqual(actual);
        }
        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三集各買一本_滿足9折優惠_價格為270()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1)
            };
            var discountrule =
                new DiscountRule() { Discount = 0.9, DifferentItemNumber = 3 };

            double expected = 270;
            var actual = pottershoppingcart.GetDiscountSum<Book>(discountrule, sumBy => sumBy.Price, groupby => groupby.Name);
            expected.ToExpectedObject().ShouldEqual(actual);
            //Assert.Fail("尚未實作");
        }
    }
}
