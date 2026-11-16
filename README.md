# Agri-Energy Connect

## Description
Agri-Energy Connect is an ASP.NET Core Web App MVC project designed to connect farmers and employees in the agricultural industry. It allows farmers to manage their product listings and employees to manage farmer profiles and product information.

## Features
- **Role-based Access:** Farmers and employees have different access levels and capabilities within the application.
- **Secure Login:** Users must log in to access the system, ensuring data security and role-specific access.
- **Product Management:** Farmers can add, edit, and view their product listings, including name, category, and production date.
- **Employee Functionality:** Employees can add new farmer profiles, view all products from a specific farmer, and use filters for product searching.

## Installation
1. **Clone the Repository:** `git clone https://github.com/MrPettitt/Agri-Energy-Connect-P2.git`
2. **Navigate to the Project Directory:** `cd ST10090394_PROG7311_POE_Part2`
3. **Install Dependencies:** `dotnet restore`
4. **Update Database:** `dotnet ef database update`
5. **Run the Application:** `dotnet run`

## Technologies Used
- ASP.NET Core MVC
- .NET 8.0
- SQL Server (for data storage)

## Demo Logins
### Farmers
- **Emails:**
  - john.doe@demo.com
  - jane.smith@demo.com
  - michael.brown@demo.com
  - emily.johnson@demo.com
  - william.white@demo.com
- **Password:** Farmer123!

### Employees
- **Emails:**
  - john.smith@demo.com
  - jane.doe@demo.com
  - william.jones@demo.com
  - emily.davis@demo.com
  - michael.wilson@demo.com
- **Password:** Employee123!

## Contributors
- [Richard Pettitt](https://github.com/MrPettitt)

## License

Released under the [MIT License](LICENSE).
