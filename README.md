E-Commerce Project

This is a generic e-commerce platform built using ASP.NET Core with a variety of modern features and integrations, making it robust, scalable, and production-ready.

Features

1. User Management

Google Login option (OAuth integration).

Secure user authentication and account management.

User information managed efficiently with SQL Server.

2. Product Management

Full CRUD operations for products.

Integration with Azure Service Bus to send alerts for product deletions.

Seamless product data storage and management.

3. Order & Payment Processing

Stripe API integration as the payment gateway for seamless transactions.

Purchase alerts and notifications powered by Azure Service Bus.

Automated email notifications for successful orders.

4. Storage & Blob Management

Azure Blob Storage for storing product images, user data, and other assets securely.

5. Additional Integrations

SQIDs Encode for generating obfuscated IDs to enhance security.

Notifications and alerts using Azure Service Bus.

Configured email service for key activities like purchases or updates.

Tech Stack

Backend

ASP.NET Core

Entity Framework Core

SQL Server

Azure Service Bus (Messaging)

Azure Blob Storage (File management)

Stripe API (Payment Gateway)

Authentication

Google OAuth for secure third-party login.

Custom authentication and authorization mechanisms.

Additional Libraries

SQIDs: For obfuscated ID encoding.

SendGrid/MailKit: Email service providers for notifications and alerts.

Frontend (Optional Integration)

This project is backend-focused. However, it can be seamlessly integrated with any modern frontend framework like React, Angular, or Vue.js.

Installation & Setup

Prerequisites

Install SQL Server and ensure it is running.

Set up Azure accounts for Service Bus and Blob Storage.

Stripe account credentials for payment gateway.

Google OAuth credentials for user authentication.

Steps

Clone the Repository

git clone https://github.com/Hforna/EcommerceAspNetCore.git
cd generic-ecommerce

Update Configuration

Replace placeholders in appsettings.json with the following:

SQL Server connection string.

Azure Service Bus and Blob Storage credentials.

Stripe API keys.

Google OAuth client ID and secret.

Database Setup

Update the connection string in appsettings.json.

Apply migrations using Entity Framework Core:

dotnet ef database update

Run the Project

dotnet build
dotnet run

API Endpoints

Endpoint

Method

Description

/api/users/login

POST

Login using Google OAuth.

/api/products

GET/POST

Fetch or create products.

/api/products/{id}

PUT/DELETE

Update or delete a product.

/api/orders

POST

Create a new order.

/api/alerts

GET

Fetch buy and delete alerts.
