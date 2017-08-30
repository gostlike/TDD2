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
            double expected = 100;
            var actual = pottershoppingcart.GetSum(book => book.Price);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一集買一本_第二集買一本_滿足95折優惠_價格為190()
        {
           Assert.Fail("尚未實作");
        }
    }
}
