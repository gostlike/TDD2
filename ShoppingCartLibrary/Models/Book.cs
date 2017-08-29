namespace ShoppingCartLibrary.Models
{
    public class Book
    {
        public string Name { get;}

        public int Price { get;}

        public Book(string name,int price)
        {
            Name = name;
            Price = price;
        }
    }
}
