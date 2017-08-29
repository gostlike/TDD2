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
                new Book("Harry Potter Episode 1",100)
            };       
            var expected = 100;
            var actual = pottershoppingcart.GetSum(book => book.Price);
            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}
