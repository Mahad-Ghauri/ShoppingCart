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

    
        public IActionResult OnGet()
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["Error"] = "Your cart is empty. Please add items before checkout.";
                return RedirectToPage("/Cart"); 
            }

            return Page();
        }

       

        public IActionResult OnPost()
        {
            cart = SessionService.GetSessionObjectFromJson<List<Item>>(HttpContext.Session, "cart");

            if (cart == null || cart.Count == 0)
            {
                TempData["Error"] = "Your cart is empty. Cannot proceed with checkout.";
                return RedirectToPage("/Cart");
            }

            if (ModelState.IsValid)
            {
                SessionService.SetSessionObjectAsJson(HttpContext.Session, "cart", null);
                return RedirectToPage("/OrderConfirmation");
            }

            return Page(); 
        }
    }
}