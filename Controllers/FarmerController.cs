using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST10090394_PROG7311_POE_Part2.Data;
using ST10090394_PROG7311_POE_Part2.Models;

namespace ST10090394_PROG7311_POE_Part2.Controllers
{
    [Authorize(Roles = "Farmer")]
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public FarmerController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: FarmerController
        public ActionResult FarmerIndex()
        {
            return View();
        }

        //GET: FarmerController/AddProduct
        public async Task<IActionResult> AddProductView()
        {
            await Task.Yield(); // Forces method to be asynchronous
            return View();
        }

        //POST: FarmerController/AddProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductView(Products product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userId = _userManager.GetUserId(User);
                    var farmer = await _context.Farmers.Include(f => f.Products).FirstOrDefaultAsync(f => f.UserId == userId);

                    if (farmer != null)
                    {
                        product.FarmerId = farmer.FarmerId;
                        farmer.Products.Add(product); // Add product via navigation property

                        await _context.SaveChangesAsync();

                        TempData["Message"] = "Product added successfully.";
                        return RedirectToAction(nameof(FarmerIndex));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Farmer not found.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine(ex.Message);
                    ModelState.AddModelError("", "An error occurred while saving to the database.");
                }
            }

            // Log ModelState errors for debugging
            foreach (var state in ModelState)
            {
                Console.WriteLine($"Key: {state.Key}, Error: {state.Value.Errors.FirstOrDefault()?.ErrorMessage}");
            }

            return View(product);
        }
    }
}
