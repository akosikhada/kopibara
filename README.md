<div align="center">
  <h1>☕ KOPIBARA</h1>
  <img src="./wwwroot/images/KOPIBARA.png" alt="KOPIBARA Logo" width="200" height="200" />
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
      <h3>🔐 Enterprise-Grade Security</h3>
      <ul>
        <li>Seamless Google OAuth 2.0 integration for frictionless customer login</li>
        <li>Role-based admin authentication with granular permission controls</li>
        <li>Advanced cookie-based session management with encryption</li>
      </ul>
    </td>
    <td width="50%">
      <h3>💸 Effortless Transactions</h3>
      <ul>
        <li>Fully integrated PayMongo payment gateway with real-time processing</li>
        <li>Support for credit/debit cards, e-wallets, and online banking</li>
        <li>PCI-compliant secure transaction handling with detailed receipts</li>
      </ul>
    </td>
  </tr>
  <tr>
    <td width="50%">
      <h3>☕ Premium Coffee Experience</h3>
      <ul>
        <li>Curated selection of specialty-grade coffee from global origins</li>
        <li>Customizable temperature options with hot and iced preparations</li>
        <li>Rich product catalog with high-resolution imagery and tasting notes</li>
      </ul>
    </td>
    <td width="50%">
      <h3>📱 Adaptive User Experience</h3>
      <ul>
        <li>Sleek, intuitive interface with modern design principles</li>
        <li>Fully responsive layout optimized for desktop, tablet, and mobile</li>
        <li>Streamlined ordering flow with minimal steps to checkout</li>
      </ul>
    </td>
  </tr>
  <tr>
    <td width="50%">
      <h3>🔔 Real-Time Notifications</h3>
      <ul>
        <li>Instant order confirmations via email and in-app alerts</li>
        <li>Order status tracking from preparation to delivery</li>
        <li>Personalized promotions based on customer preferences</li>
      </ul>
    </td>
    <td width="50%">
      <h3>📊 Comprehensive Analytics</h3>
      <ul>
        <li>Detailed sales reporting and performance metrics</li>
        <li>Customer behavior insights and preference tracking</li>
        <li>Inventory management with automatic reordering suggestions</li>
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
