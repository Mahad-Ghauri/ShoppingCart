using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShoppingCart.Model;
using ShoppingCart.Service;

namespace ShoppingCart.Pages
{
    public class CheckoutModel : PageModel
    {
        public List<Item> cart { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; }

        public void OnGet()
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");
        }

        public IActionResult OnPost()
        {
            // Retrieve cart
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            // Process order logic (e.g., save to database, send confirmation email)
            if (cart != null && cart.Count > 0)
            {
                // Clear cart after successful order
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", null);

                // Redirect to a confirmation page or display a success message
                return RedirectToPage("/OrderConfirmation");
            }

            return Page(); // Stay on checkout if there is an issue
        }
    }
}