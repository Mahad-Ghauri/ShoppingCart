using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Data;
using ShoppingCart.Model;

namespace ShoppingCart.Pages
{
    public class IndexModel : PageModel
    {
        public List<Product> Products;
        public IndexModel()
        {
            ProductModel productModel = new ProductModel();
            Products = productModel.GetProducts();
        }

        public void OnGet()
        {

        }
    }
}
