using ShoppingCart.Model;

namespace ShoppingCart.Data
{
    public class ProductModel
    {
        private List<Product> Products;

        public ProductModel() 
        {
            Products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Canon Camera",
                    Price = 4599,
                    Image = "1.jpg"
                },
                new Product
                {
                    Id = 2,
                    Name = "Air Pods Pro 2",
                    Price =450,
                    Image = "2.jpg"
                },
                new Product
                {
                    Id = 3,
                    Name = "Audionic MN-699",
                    Price = 350,
                    Image = "3.jpg"
                },
                new Product
                {
                    Id = 4,
                    Name = "Iphone 15 Pro Max 1TB",
                    Price = 1099,
                    Image = "4.jpg"
                },
                new Product
                {
                    Id = 5,
                    Name = "Playstation 5 Pro",
                    Price = 700,
                    Image = "5.jpg"
                },
                new Product
                {
                    Id = 6,
                    Name = "X-box One",
                    Price = 499,
                    Image = "6.jpg"
                },
                new Product
                {
                    Id = 7,
                    Name = "Macbook Pro M3 Max 16'inch",
                    Price = 3999,
                    Image = "7.jpg"
                },
                new Product
                {
                    Id = 8,
                    Name = "Macbook Air M2 14'inch",
                    Price = 2500,
                    Image = "8.jpg"
                },
                new Product
                {
                    Id = 9,
                    Name = "Hp Omen",
                    Price = 1599,
                    Image = "9.jpg"
                },
                new Product
                {
                    Id = 10,
                    Name = "Dell Alien-Ware",
                    Price = 799,
                    Image = "10.png"
                },
                new Product
                {
                    Id = 11,
                    Name = "33W C to C type adapter",
                    Price = 299,
                    Image = "11.jpg"
                },
                new Product
                {
                    Id = 12,
                    Name = "Poadcast Microphone pro",
                    Price = 599,
                    Image = "12.jpg"
                },

            }; 
        } // Scope end of Constructor

        public List<Product> GetProducts() // This function is used to return all the products
        {
            return Products;
        }

        public Product GetProductById(int id) // This function is used to return individual products rather than returning all products
        {
            var productById = Products.Where(x=>x.Id == id).FirstOrDefault();
            return productById;
        }
    }
}
