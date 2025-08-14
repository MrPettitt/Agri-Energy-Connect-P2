using ST10090394_PROG7311_POE_Part2.Data;
using ST10090394_PROG7311_POE_Part2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ST10090394_PROG7311_POE_Part2
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Bypass SSL certificate validation
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (sender, cert, chain, sslPolicyErrors) => true;


            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            // Create roles and seed database
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                await CreateRoles(services);
                await SeedDatabase(services);
            }


            app.Run();
            // Method to create roles
            async Task CreateRoles(IServiceProvider serviceProvider)
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roleNames = { "Farmer", "Employee" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                    }
                }
            }
            //Populated with ChatGPT
            //Method to seed database
            static async Task SeedDatabase(IServiceProvider serviceProvider)
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

                // Add employee user
                var employeeUser1 = new IdentityUser { UserName = "john.smith@demo.com", Email = "john.smith@demo.com", EmailConfirmed = true };
                var employeeResult1 = await userManager.CreateAsync(employeeUser1, "Employee123!");
                if (employeeResult1.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser1, "Employee");

                    // Add employee profile
                    var employee1 = new Employees
                    {
                        FullName = "John Smith",
                        ContactNumber = "0987654321",
                        Position = "Farm Manager",
                        UserId = employeeUser1.Id
                    };
                    context.Employees.Add(employee1);
                    await context.SaveChangesAsync();
                }

                // Add employee user
                var employeeUser2 = new IdentityUser { UserName = "jane.doe@demo.com", Email = "jane.doe@demo.com", EmailConfirmed = true };
                var employeeResult2 = await userManager.CreateAsync(employeeUser2, "Employee123!");
                if (employeeResult2.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser2, "Employee");

                    // Add employee profile
                    var employee2 = new Employees
                    {
                        FullName = "Jane Doe",
                        ContactNumber = "9876543210",
                        Position = "Agricultural Specialist",
                        UserId = employeeUser2.Id
                    };
                    context.Employees.Add(employee2);
                    await context.SaveChangesAsync();
                }

                // Add employee user
                var employeeUser3 = new IdentityUser { UserName = "william.jones@demo.com", Email = "william.jones@demo.com", EmailConfirmed = true };
                var employeeResult3 = await userManager.CreateAsync(employeeUser3, "Employee123!");
                if (employeeResult3.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser3, "Employee");

                    // Add employee profile
                    var employee3 = new Employees
                    {
                        FullName = "William Jones",
                        ContactNumber = "0987123456",
                        Position = "Field Supervisor",
                        UserId = employeeUser3.Id
                    };
                    context.Employees.Add(employee3);
                    await context.SaveChangesAsync();
                }

                // Add employee user
                var employeeUser4 = new IdentityUser { UserName = "emily.davis@demo.com", Email = "emily.davis@demo.com", EmailConfirmed = true };
                var employeeResult4 = await userManager.CreateAsync(employeeUser4, "Employee123!");
                if (employeeResult4.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser4, "Employee");

                    // Add employee profile
                    var employee4 = new Employees
                    {
                        FullName = "Emily Davis",
                        ContactNumber = "0987234567",
                        Position = "Crop Consultant",
                        UserId = employeeUser4.Id
                    };
                    context.Employees.Add(employee4);
                    await context.SaveChangesAsync();
                }

                // Add employee user
                var employeeUser5 = new IdentityUser { UserName = "michael.wilson@demo.com", Email = "michael.wilson@demo.com", EmailConfirmed = true };
                var employeeResult5 = await userManager.CreateAsync(employeeUser5, "Employee123!");
                if (employeeResult5.Succeeded)
                {
                    await userManager.AddToRoleAsync(employeeUser5, "Employee");

                    // Add employee profile
                    var employee5 = new Employees
                    {
                        FullName = "Michael Wilson",
                        ContactNumber = "0987345678",
                        Position = "Logistics Coordinator",
                        UserId = employeeUser5.Id
                    };
                    context.Employees.Add(employee5);
                    await context.SaveChangesAsync();
                }

                // Add farmer user
                var farmerUser1 = new IdentityUser { UserName = "john.doe@demo.com", Email = "john.doe@demo.com", EmailConfirmed = true };
                var farmerResult1 = await userManager.CreateAsync(farmerUser1, "Farmer123!");
                if (farmerResult1.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser1, "Farmer");

                    // Add farmer profile
                    var farmer1 = new Farmers
                    {
                        FullName = "John Doe",
                        ContactNumber = "1234567891",
                        Address = "123 Main St, Springfield, IL",
                        UserId = farmerUser1.Id
                    };
                    context.Farmers.Add(farmer1);
                    await context.SaveChangesAsync();

                    // Add products for the farmer
                    var products1 = new List<Products>
                    {
                        new Products { ProductName = "Apple", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-10), FarmerId = farmer1.FarmerId },
                        new Products { ProductName = "Carrot", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-6), FarmerId = farmer1.FarmerId },
                        new Products { ProductName = "Banana", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-12), FarmerId = farmer1.FarmerId },
                        new Products { ProductName = "Potato", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-2), FarmerId = farmer1.FarmerId }
                    };
                    context.Products.AddRange(products1);
                    await context.SaveChangesAsync();
                }

                // Add farmer user
                var farmerUser2 = new IdentityUser { UserName = "jane.smith@demo.com", Email = "jane.smith@demo.com", EmailConfirmed = true };
                var farmerResult2 = await userManager.CreateAsync(farmerUser2, "Farmer123!");
                if (farmerResult2.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser2, "Farmer");

                    // Add farmer profile
                    var farmer2 = new Farmers
                    {
                        FullName = "Jane Smith",
                        ContactNumber = "1234567892",
                        Address = "456 Elm St, Shelbyville, IL",
                        UserId = farmerUser2.Id
                    };
                    context.Farmers.Add(farmer2);
                    await context.SaveChangesAsync();

                    // Add products for the farmer
                    var products2 = new List<Products>
                    {
                        new Products { ProductName = "Tomato", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-8), FarmerId = farmer2.FarmerId },
                        new Products { ProductName = "Lettuce", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-5), FarmerId = farmer2.FarmerId },
                        new Products { ProductName = "Strawberry", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-15), FarmerId = farmer2.FarmerId },
                        new Products { ProductName = "Onion", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-3), FarmerId = farmer2.FarmerId }
                    };
                    context.Products.AddRange(products2);
                    await context.SaveChangesAsync();
                }

                // Add farmer user
                var farmerUser3 = new IdentityUser { UserName = "michael.brown@demo.com", Email = "michael.brown@demo.com", EmailConfirmed = true };
                var farmerResult3 = await userManager.CreateAsync(farmerUser3, "Farmer123!");
                if (farmerResult3.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser3, "Farmer");

                    // Add farmer profile
                    var farmer3 = new Farmers
                    {
                        FullName = "Michael Brown",
                        ContactNumber = "1234567893",
                        Address = "789 Maple Ave, Capital City, IL",
                        UserId = farmerUser3.Id
                    };
                    context.Farmers.Add(farmer3);
                    await context.SaveChangesAsync();

                    // Add products for the farmer
                    var products3 = new List<Products>
                    {
                        new Products { ProductName = "Grapes", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-9), FarmerId = farmer3.FarmerId },
                        new Products { ProductName = "Cabbage", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-7), FarmerId = farmer3.FarmerId },
                        new Products { ProductName = "Peach", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-11), FarmerId = farmer3.FarmerId },
                        new Products { ProductName = "Radish", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-1), FarmerId = farmer3.FarmerId }
                    };
                    context.Products.AddRange(products3);
                    await context.SaveChangesAsync();
                }

                // Add farmer user
                var farmerUser4 = new IdentityUser { UserName = "emily.johnson@demo.com", Email = "emily.johnson@demo.com", EmailConfirmed = true };
                var farmerResult4 = await userManager.CreateAsync(farmerUser4, "Farmer123!");
                if (farmerResult4.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser4, "Farmer");

                    // Add farmer profile
                    var farmer4 = new Farmers
                    {
                        FullName = "Emily Johnson",
                        ContactNumber = "1234567894",
                        Address = "321 Oak St, Ogdenville, IL",
                        UserId = farmerUser4.Id
                    };
                    context.Farmers.Add(farmer4);
                    await context.SaveChangesAsync();

                    // Add products for the farmer
                    var products4 = new List<Products>
                    {
                        new Products { ProductName = "Orange", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-14), FarmerId = farmer4.FarmerId },
                        new Products { ProductName = "Broccoli", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-4), FarmerId = farmer4.FarmerId },
                        new Products { ProductName = "Blueberry", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-13), FarmerId = farmer4.FarmerId },
                        new Products { ProductName = "Garlic", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-5), FarmerId = farmer4.FarmerId }
                    };
                    context.Products.AddRange(products4);
                    await context.SaveChangesAsync();
                }

                // Add farmer user
                var farmerUser5 = new IdentityUser { UserName = "william.white@demo.com", Email = "william.white@demo.com", EmailConfirmed = true };
                var farmerResult5 = await userManager.CreateAsync(farmerUser5, "Farmer123!");
                if (farmerResult5.Succeeded)
                {
                    await userManager.AddToRoleAsync(farmerUser5, "Farmer");

                    // Add farmer profile
                    var farmer5 = new Farmers
                    {
                        FullName = "William White",
                        ContactNumber = "1234567895",
                        Address = "654 Pine St, North Haverbrook, IL",
                        UserId = farmerUser5.Id
                    };
                    context.Farmers.Add(farmer5);
                    await context.SaveChangesAsync();

                    // Add products for the farmer
                    var products5 = new List<Products>
                    {
                        new Products { ProductName = "Pear", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-10), FarmerId = farmer5.FarmerId },
                        new Products { ProductName = "Spinach", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-6), FarmerId = farmer5.FarmerId },
                        new Products { ProductName = "Plum", ProductCategory = "Fruit", ProductionDate = DateTime.Now.AddDays(-12), FarmerId = farmer5.FarmerId },
                        new Products { ProductName = "Pepper", ProductCategory = "Vegetable", ProductionDate = DateTime.Now.AddDays(-2), FarmerId = farmer5.FarmerId }
                    };
                    context.Products.AddRange(products5);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
