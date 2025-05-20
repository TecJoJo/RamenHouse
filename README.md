# 🍽️ restaurant-style meal management Web App

A modern, user-friendly web application built with **ASP.NET Core MVC**, designed for restaurant-style meal management. The app provides seamless user authentication and an admin dashboard for managing meals efficiently.

---

## 🚀 Deployment

This application is deployed on Azure Web Services.  
You can try it live here: [Live Demo](https://ramenhouse-fcfudqg8eya5ehf5.swedencentral-01.azurewebsites.net/)

## 🚀 Features

### 👥 User Authentication
- **Registration**: Secure sign-up process with validation.
- **Login**: Authenticated access for users and admin roles.

### 🧑‍💼 Admin Dashboard
- Add new meals with name, description, price, and category.
- Edit or update existing meal details.
- Manage meal categories.

### 📋 Menu Page
- View all meals added by the admin.
- Sort meals based on categories (e.g., Appetizers, Main Course, Desserts).

### 🏠 Landing Page
- Company introduction and mission.
- Essential information such as contact details, operating hours, and a brief overview of services.

---

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core MVC
- **Frontend**: Razor Views, Tailwind CSS
- **Authentication**: ASP.NET Identity
- **Database**: Entity Framework Core + SQLite

---

## ⚙️ Project Setup Instructions

1. **Clone the repository**
   ```bash
   git clone https://github.com/TecJoJo/RamenHouse.git
   cd RamenHouse
   ```

2. **Install dependencies**
   - Backend dependencies are managed by .NET. Restore them with:
     ```bash
     dotnet restore
     ```
    - In Visual Studio 2022, dependencies are restored automatically.


3. **Database setup**
   - The SQLite database file (`ApplicationDB.db`) is located in the repo's root path with passwords securely hashed. You may not need to restore the database.
   - If you want to start fresh:
     - Delete all the database-related files (`ApplicationDB.db`, `ApplicationDB.db-shm`, `ApplicationDB.db-wal`) in the root path.
     - To apply migrations and re-create the database, run:
     ```bash
     dotnet ef database update --project ramenHouse
     ```
   - The project uses SQLite. The default connection string is set in `ramenHouse/appsettings.json`.
  
   

4. **Run the application**
- In Visual Studio 2022, press **F5** to start the app locally.
- If you want to start the app from the command line, run:

   ```bash
   dotnet run --project ramenHouse
   ```
   The app will be available at `https://localhost:5001` or the port specified in your launch settings.



---
