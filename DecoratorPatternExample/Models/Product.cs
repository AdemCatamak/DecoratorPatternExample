using System;

namespace DecoratorPatternExample.Models
{
    public class Product
    {
        public string Id { get; set; }

        public string Name { get; set; }
        private decimal _price;

        public decimal Price
        {
            get => _price;
            set => _price = Math.Round(value, 2, MidpointRounding.AwayFromZero);
        }

        // Api Call trigger this
        public Product()
            : this(string.Empty, decimal.Zero)
        {
        }

        public Product(string name, decimal price)
            : this(Guid.NewGuid().ToString(), name, price)
        {
        }

        private Product(string id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}