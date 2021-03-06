﻿using ExpectedObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShoppingCartLibrary;
using ShoppingCartLibrary.Models;
using System.Collections.Generic;

namespace PotterShoppingCart.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly List<DiscountRule> discountRules = new List<DiscountRule>
        {
            //按折扣高依序排列，index低的優先套用，index 0>1>2 ...
            new DiscountRule() {Discount = 0.75, DifferentItemNumber = 5},
            new DiscountRule() {Discount = 0.8, DifferentItemNumber = 4},
            new DiscountRule() {Discount = 0.9, DifferentItemNumber = 3},
            new DiscountRule() {Discount = 0.95, DifferentItemNumber = 2}
        };

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一集買1本_無滿足任何優惠_價格為100()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1)
            };
            double expected = 100;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一集買1本_第二集買1本_滿足95折優惠_價格為190()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1)
            };
            double expected = 190;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三集各買1本_滿足9折優惠_價格為270()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1)
            };

            double expected = 270;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三四集各買1本_滿足8折優惠_價格為320()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 4",100,1)
            };
            double expected = 320;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
            //Assert.Fail("尚未實作");
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三四五集各買1本_滿足75折優惠_價格為375()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 5",100,1)
            };
            double expected = 375;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二集各買1本_第三集買2本_其中三本滿足9折優惠_價格為370()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1)
            };
            double expected = 370;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一集買1本_第二三集各買2本_其中三本滿足9折兩本滿足95折優惠_價格為460()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1)
            };
            double expected = 460;
            var actual = pottershoppingcart.GetBookPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三集買2本_第四五集各買1本_最優惠價格為640()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 5",100,1)
            };
            double expected = 640;
            var actual = pottershoppingcart.GetBookBestPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三四集買2本_第五集買1本_最優惠價格為695()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 5",100,1)
            };
            double expected = 695;
            var actual = pottershoppingcart.GetBookBestPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }

        [TestMethod]
        public void PotterShoppingCart價格計算測試_第一二三集買4本_第四五集買2本_最優惠價格為1280()
        {
            var pottershoppingcart = new List<Book>
            {
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 1",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 2",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 3",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 4",100,1),
                new Book("Harry Potter Episode 5",100,1),
                new Book("Harry Potter Episode 5",100,1)
            };
            double expected = 1280;
            var actual = pottershoppingcart.GetBookBestPrice(discountRules);
            expected.ToExpectedObject().ShouldEqual(actual);
        }
    }
}