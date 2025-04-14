<div align="center">
  <img src="./wwwroot/images/KOPIBARA.png" alt="KOPIBARA Logo" width="300" height="300" />
  <h1>☕ KOPIBARA</h1>
  <p><strong><i>Your Modern Coffee Ordering Experience</i></strong></p>
  
  <p>
    <img src="https://img.shields.io/badge/ASP.NET%20Core-8.0-512BD4?style=flat-square&logo=dotnet" alt="ASP.NET Core 8.0" />
    <img src="https://img.shields.io/badge/Entity%20Framework-Core-512BD4?style=flat-square&logo=dotnet" alt="Entity Framework Core" />
    <img src="https://img.shields.io/badge/Payment-PayMongo-6772E5?style=flat-square&logo=stripe" alt="PayMongo" />
    <img src="https://img.shields.io/badge/Auth-Google%20OAuth-4285F4?style=flat-square&logo=google" alt="Google OAuth" />
  </p>
</div>

## 📋 Overview

**KOPIBARA** is a state-of-the-art web-based coffee ordering system developed by the KPL Team as part of the **IPT102 - Integrative Programming and Technologies 2** course. The platform provides customers with a seamless experience to browse and order a variety of coffee beverages, with both hot and iced options available.

## ✨ Key Features

<table>
  <tr>
    <td width="50%">
      <h3>🔐 Secure Authentication</h3>
      <ul>
        <li>Google OAuth integration for customer login</li>
        <li>Custom admin authentication system</li>
        <li>Secure cookie-based session management</li>
      </ul>
    </td>
    <td width="50%">
      <h3>💸 Seamless Payments</h3>
      <ul>
        <li>Integration with PayMongo payment gateway</li>
        <li>Support for multiple payment methods</li>
        <li>Secure transaction processing</li>
      </ul>
    </td>
  </tr>
  <tr>
    <td width="50%">
      <h3>☕ Comprehensive Menu</h3>
      <ul>
        <li>Extensive selection of premium coffee</li>
        <li>Hot and iced variations available</li>
        <li>Detailed product descriptions and images</li>
      </ul>
    </td>
    <td width="50%">
      <h3>📱 Responsive Design</h3>
      <ul>
        <li>Modern, user-friendly interface</li>
        <li>Optimized for all device sizes</li>
        <li>Intuitive ordering workflow</li>
      </ul>
    </td>
  </tr>
</table>

## ☕ Coffee Selection

<div align="center">
  <table>
    <tr>
      <td align="center"><img src="./wwwroot/images/HOT-ESPRESSO.jpg" width="100" /><br><b>Espresso</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-CAPPUCCINO.jpg" width="100" /><br><b>Cappuccino</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-LATTE.jpg" width="100" /><br><b>Latte</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-MACCHIATO.jpg" width="100" /><br><b>Macchiato</b></td>
    </tr>
    <tr>
      <td align="center"><img src="./wwwroot/images/HOT-MOCHA.jpg" width="100" /><br><b>Mocha</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-VIETNAMESE.jpg" width="100" /><br><b>Vietnamese</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-IRISH.jpg" width="100" /><br><b>Irish Coffee</b></td>
      <td align="center"><img src="./wwwroot/images/HOT-BLACK-COFFEE.jpg" width="100" /><br><b>Black Coffee</b></td>
    </tr>
  </table>
  <p><em>Available in both hot and iced variants</em></p>
</div>

## 🛠️ Technology Stack

<table>
  <tr>
    <td>
      <h3>Backend</h3>
      <ul>
        <li><strong>Framework:</strong> ASP.NET Core 8.0</li>
        <li><strong>Database:</strong> Microsoft SQL Server</li>
        <li><strong>ORM:</strong> Entity Framework Core</li>
        <li><strong>Authentication:</strong> Google OAuth 2.0, Custom auth</li>
      </ul>
    </td>
    <td>
      <h3>Frontend</h3>
      <ul>
        <li><strong>Views:</strong> ASP.NET MVC with Razor</li>
        <li><strong>Styling:</strong> CSS, Bootstrap</li>
        <li><strong>Client-side:</strong> JavaScript</li>
      </ul>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <h3>Libraries & Packages</h3>
      <ul>
        <li><strong>JSON Processing:</strong> Newtonsoft.Json</li>
        <li><strong>API Integration:</strong> RestSharp</li>
        <li><strong>Authentication:</strong> Microsoft.AspNetCore.Authentication.Google</li>
        <li><strong>Database:</strong> Microsoft.EntityFrameworkCore.SqlServer</li>
      </ul>
    </td>
  </tr>
</table>

## 🚀 Getting Started

### Prerequisites

- .NET 8.0 SDK
- Microsoft SQL Server (LocalDB or higher)
- Visual Studio 2022 or VS Code with C# extensions
- Google OAuth credentials (client ID and secret)

### Setup & Installation

1. Clone the repository

   ```bash
   git clone https://github.com/akosikhada/kopibara.git
   cd kopibara
   ```

2. Configure the application settings

   - Rename `appsettings.example.json` to `appsettings.json`
   - Update the connection string and Google OAuth credentials

3. Run database migrations

   ```bash
   dotnet ef database update
   ```

4. Start the application
   ```bash
   dotnet run
   ```

## 📁 Project Structure

```
KOPIBARA/
├── Controllers/      # Request handling logic
│   ├── AdminController.cs    # Admin panel functionality
│   ├── AuthController.cs     # Authentication management
│   ├── HomeController.cs     # Main application pages
│   ├── OrderController.cs    # Order processing
│   └── PaymentController.cs  # Payment integration
├── Models/           # Data models
│   ├── Accounts.cs           # User account data
│   ├── CoffeeOrder.cs        # Order information
│   ├── Kopi_products.cs      # Product catalog
│   └── OrderList.cs          # Order management
├── Views/            # Razor views for UI
├── Data/             # Database context
└── wwwroot/          # Static assets
    └── images/       # Product images & branding
```

## 🙏 Acknowledgments

- Coffee icons and images used in the project
- ASP.NET Core community
- All contributors who have helped shape this project
- Special thanks to our professor in IPT102 - Integrative Programming and Technologies 2

---

<div align="center">
  <p>© 2024 KPL Team | All Rights Reserved</p>
</div>
