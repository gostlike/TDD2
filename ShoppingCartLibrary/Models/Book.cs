using System;

namespace ShoppingCartLibrary.Models
{
    public class Book
    {
        public string Name { get;}
        public double Price => OriginPrice * Discount;
        public double OriginPrice { get; }
        public double Discount { get; set; }
        public Book(string name,int originprice, double discount)
        {
            Name = name;
            OriginPrice = originprice;
            Discount = discount;
        }
    }
}
