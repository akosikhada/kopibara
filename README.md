# Kopibara - Coffee Shop Web Application

## Overview

Kopibara is a modern ASP.NET MVC web application for a coffee shop that allows customers to browse coffee products, place orders, and complete checkout. The application features a full e-commerce experience with product catalog, shopping cart, secure checkout, and user authentication integration.

## Features

- **Product Catalog**: Browse through a variety of hot and iced coffee products
- **Shopping Cart**: Add items to cart with customization options (size, brew style, sugar level)
- **User Authentication**: Sign in using Google authentication or admin credentials
- **Order Management**: Track and manage coffee orders
- **Secure Checkout**: Complete purchases through integrated payment processing
- **Admin Panel**: Manage products, view orders, and track sales

## Tech Stack

- **Framework**: ASP.NET MVC (.NET 8.0)
- **Database**: Microsoft SQL Server with Entity Framework Core
- **Authentication**: Google Authentication and Custom Authentication
- **Frontend**: HTML, CSS, JavaScript with ASP.NET MVC Views
- **Dependencies**:
  - Microsoft.AspNetCore.Authentication.Google
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.SqlServer
  - Newtonsoft.Json
  - RestSharp

## Setup Instructions

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (Local or Remote)
- Visual Studio 2022 (recommended) or other .NET IDE

### Database Setup

1. Update the connection string in `appsettings.json` to point to your SQL Server instance
2. Open Package Manager Console and run:
   ```
   Update-Database
   ```

### Google Authentication Setup

1. Create a project in the Google Developer Console
2. Set up OAuth credentials
3. Update the `GoogleKeys` section in `appsettings.json` with your ClientId and ClientSecret
4. Set the authorized redirect URI to include `/signin-google`

### Running the Application

1. Clone the repository
2. Open the solution in Visual Studio or your preferred IDE
3. Restore NuGet packages
4. Build the solution
5. Run the application

## Project Structure

- **Controllers/**: Contains the application's controller logic
  - `HomeController.cs`: Landing page and basic navigation
  - `OrderController.cs`: Shopping cart and order management
  - `PaymentController.cs`: Payment processing
  - `AuthController.cs`: Authentication
  - `AdminController.cs`: Admin dashboard and functionality
- **Models/**: Contains data models
  - `Kopi_products.cs`: Coffee product details
  - `CoffeeOrder.cs`: Order information
  - `Checkout.cs`: Checkout data
  - `Accounts.cs`: User account information
- **Views/**: Contains UI templates organized by controller
- **Data/**: Contains database context

  - `ApplicationDbContext.cs`: EF Core database context

- **wwwroot/**: Static assets including images, CSS, and JavaScript files

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details

## 🙏 Acknowledgments

- Coffee icons and images used in the project
- ASP.NET Core community
- All contributors who have helped shape this project
- Special thanks to our professor in IPT102 - Integrative Programming and Technologies 2
