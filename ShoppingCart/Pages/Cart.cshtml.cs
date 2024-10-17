using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Data;
using ShoppingCart.Model;
using ShoppingCart.Service;

namespace ShoppingCart.Pages
{
    public class CartModel : PageModel
    {

        public List<Item> cart;

        public void OnGet(int id) // To get the id of the product
        {
            ProductModel productModel = new();// This will provide the data of the product

            // The data saved on the variable named 'cart' get and store it in the cart variable of the type List of Item type
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if (cart == null)
            {
                cart = new List<Item>()
                {
                    new Item()
                    {
                        Product = productModel.GetProductById(id),
                        Quantity = 1
                    }
                };

                //This is used to save the data in the session so that the data is not lost if we go forward and backward on the website
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                int checkID = Check_If_Exists(cart, id);
                if (checkID == -1)
                {
                    cart.Add(new Item()
                    {
                        Product = productModel.GetProductById(id),
                        Quantity = 1
                    });
                }
                else
                {
                    cart[checkID].Quantity++;
                }

                //This is used to save the data in the session so that the data is not lost if we go forward and backward on the website
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }
        public void OnGetIncreaseProduct(int id)
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int checkID = Check_If_Exists(cart, id);
            if (checkID != -1)
            {
                //var increaseItem = cart.FirstOrDefault(x => x.Product.Id == id);
                //increaseItem.Quantity++;
                cart[checkID].Quantity++;
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }

        }

        public void OnGetDecreaseProduct(int id)
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int checkID = Check_If_Exists(cart, id);
            if (checkID != -1)
            {
                if (cart[checkID].Quantity > 1)
                {
                    cart[checkID].Quantity--;
                }
                else
                {
                    // If the quantity becomes 0, remove the product from the cart
                    cart.RemoveAt(checkID);
                }
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }

        public void OnGetRemoveProduct(int id)  
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int index = Check_If_Exists(cart, id);
            if (index != -1)
            {
                cart.RemoveAt(index);
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }
        }
        private int Check_If_Exists(List<Item> cart, int id)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
