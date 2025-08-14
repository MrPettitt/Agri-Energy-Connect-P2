using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ST10090394_PROG7311_POE_Part2.Data;
using ST10090394_PROG7311_POE_Part2.Models;

namespace ST10090394_PROG7311_POE_Part2.Controllers
{
    [Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EmployeeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: EmployeeController
        public ActionResult EmployeeIndex()
        {
            return View();
        }

        // GET: Employee/AddFarmer
        public IActionResult AddFarmer()
        {
            return View();
        }

        // POST: Employee/AddFarmer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddFarmer(FarmerModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Create a new IdentityUser
                    var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true }; //bypass email confirmation for development purposes
                    // Create the user and assign the password
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        // Add the user to the "Farmer" role
                        await _userManager.AddToRoleAsync(user, "Farmer");

                        // Set the UserId of the farmer to the Id of the new user
                        var farmer = new Farmers
                        {
                            FullName = model.FullName,
                            ContactNumber = model.ContactNumber,
                            Address = model.Address,
                            UserId = user.Id
                        };

                        // Add the farmer to the database
                        _context.Farmers.Add(farmer);
                        await _context.SaveChangesAsync();

                        return RedirectToAction(nameof(EmployeeIndex));
                    }
                    else
                    {
                        // If there was an error creating the user, add the errors to the ModelState
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log the error
                    Console.WriteLine(ex.Message);
                    // Add an error message to the model state
                    ModelState.AddModelError("", "An error occurred while saving to the database.");
                }
            }
            return View(model);
        }

        // GET: Employee/ViewProducts
        public IActionResult ViewProducts()
        {
            ViewBag.Farmers = new SelectList(_context.Farmers, "FarmerId", "FullName");
            return View();
        }

        // POST: Employee/ViewProducts
        [HttpPost]
        public IActionResult ViewProducts(int farmerId, DateTime? startDate, DateTime? endDate, string category)
        {
            ViewBag.Farmers = new SelectList(_context.Farmers, "FarmerId", "FullName");

            var products = _context.Products.Where(p => p.FarmerId == farmerId).AsQueryable();

            if (startDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate >= startDate);
            }
            if (endDate.HasValue)
            {
                products = products.Where(p => p.ProductionDate <= endDate);
            }
            if (!string.IsNullOrEmpty(category) && category != "All Categories")
            {
                products = products.Where(p => p.ProductCategory == category);
            }

            return View(products.ToList());
        }

    }
}
