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
        public decimal Subtotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GSTAmount { get; set; }
        public decimal Total { get; set; }

        private const double DiscountPercentage = 7.0; // 7% Discount
        private const double GstPercentage = 13.0;      // 13% GST

        public double FinalTotalAmount { get; set; }

        public void OnGet(int? id = null)
        {
            ProductModel productModel = new();

            // Retrieve the cart from session
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            //if (cart == null || cart.Count == 0)
            //{
            //    TempData["Error"] = "Your cart is empty. Please add items before checkout.";
            //    return RedirectToPage("/Cart");
            //}

            // If the cart is null, initialize it as a new empty list
            if (cart == null)
            {
                cart = new List<Item>();
            }

            // Only try to add a product if an id is provided
            if (id.HasValue)
            {
                int checkID = Check_If_Exists(cart, id.Value);
                if (checkID == -1)
                {
                    // Add the product to the cart
                    cart.Add(new Item()
                    {
                        Product = productModel.GetProductById(id.Value),
                        Quantity = 1
                    });
                }
                else
                {
                    // Increase the quantity if the product is already in the cart
                    cart[checkID].Quantity++;
                }

                // Save the cart to the session
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }

            CalculateCartTotals(); // Calculating the price after discount and GST
            CalculateFinalAmount(); // Will be passed to checkout.cshtml.cs

              

        }

        public void OnGetIncreaseProduct(int id)
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            int checkID = Check_If_Exists(cart, id);
            if (checkID != -1)
            {
                cart[checkID].Quantity++;
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }

            CalculateCartTotals();
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
                    cart.RemoveAt(checkID);
                }
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", cart);
            }

            CalculateCartTotals();
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

            CalculateCartTotals();
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

        private void CalculateCartTotals()
        {
            // Calculate the subtotal (before discount)
            Subtotal = cart.Sum(item => item.Quantity * (decimal)item.Product.Price);

            // Calculate the discount amount using the defined constant
            DiscountAmount = Subtotal * (decimal)(DiscountPercentage / 100);

            // Calculate the total after discount
            var discountedTotal = Subtotal - DiscountAmount;

            // Calculate GST (13% of the discounted total) using the defined constant
            GSTAmount = discountedTotal * (decimal)(GstPercentage / 100);

            // Calculate the final total (discounted total + GST)
            Total = discountedTotal + GSTAmount;
        }

        private void CalculateFinalAmount()
        {
            var subtotal = cart.Sum(x => x.Quantity * x.Product.Price);
            var discountAmount = subtotal * (DiscountPercentage / 100);
            var subtotalAfterDiscount = subtotal - discountAmount;
            var gstAmount = subtotalAfterDiscount * (GstPercentage / 100);
            FinalTotalAmount = subtotalAfterDiscount + gstAmount;
        }
    }
}
