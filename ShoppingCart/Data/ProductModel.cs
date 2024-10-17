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
                    Name = "Product One",
                    Price = 500,
                    Image = "1.png"
                },
                new Product
                {
                    Id = 2,
                    Name = "Product Two",
                    Price = 250,
                    Image = "2.png"
                },
                new Product
                {
                    Id = 3,
                    Name = "Product Three",
                    Price = 899,
                    Image = "3.png"
                },
                new Product
                {
                    Id = 4,
                    Name = "Product Four",
                    Price = 699,
                    Image = "4.png"
                },
                new Product
                {
                    Id = 5,
                    Name = "Product Five",
                    Price = 450,
                    Image = "5.png"
                },
                new Product
                {
                    Id = 6,
                    Name = "Product Six",
                    Price = 150,
                    Image = "6.png"
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
