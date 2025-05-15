# E-Commerce Project

This is a generic e-commerce platform built using **ASP.NET Core**, featuring modern integrations to ensure robustness, scalability, and production readiness.

## Features

### User Management
- Google Login option (OAuth integration)
- Secure user authentication and account management
- User information managed efficiently with SQL Server

### Product Management
- Full CRUD operations for products
- Integration with Azure Service Bus to send alerts for product deletions
- Seamless product data storage and management

### Order & Payment Processing
- **Stripe API** integration for seamless payment processing
- Purchase alerts and notifications powered by Azure Service Bus
- Automated email notifications for successful orders

### Storage & Blob Management
- **Azure Blob Storage** for securely storing product images, user data, and other assets

### Additional Integrations
- **SQIDs** Encode for generating obfuscated IDs to enhance security
- Notifications and alerts using **Azure Service Bus**
- Configured email service for key activities (e.g., purchases, updates)

## Tech Stack

### Backend
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Azure Service Bus (Messaging)
- Azure Blob Storage (File Management)
- Stripe API (Payment Gateway)

### Authentication
- Google OAuth for secure third-party login
- Custom authentication and authorization mechanisms

### Additional Libraries
- **SQIDs**: For obfuscated ID encoding
- **SendGrid/MailKit**: Email service providers for notifications and alerts

---

## Installation & Setup

### Prerequisites
- Install **SQL Server** and ensure it is running
- Set up Azure accounts for **Service Bus** and **Blob Storage**
- Stripe account credentials for payment gateway
- Google OAuth credentials for user authentication

### Steps

#### 1. Clone the Repository
```bash
git clone https://github.com/Hforna/EcommerceAspNetCore.git
cd EcommerceAspNetCore
```

#### 2. Update Configuration
Replace placeholders in `appsettings.json` with the following:
- SQL Server connection string
- Azure Service Bus and Blob Storage credentials
- Stripe API keys
- Google OAuth client ID and secret

#### 3. Database Setup
Update the connection string in `appsettings.json` and apply migrations:
```bash
dotnet ef database update
```

#### 4. Run the Project
```bash
dotnet build
dotnet run
```
