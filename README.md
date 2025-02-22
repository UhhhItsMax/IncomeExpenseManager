# IncomeExpenseManager

IncomeExpenseManager is a web application designed to help users efficiently track and manage their incomes, expenses, and bank accounts. With features like categorized transactions, insightful summaries, and visual charts, it provides users with a comprehensive view of their financial activities.

## Features

- **User Authentication**: Secure user registration and login functionalities using ASP.NET Identity.
- **Transaction Management**: Add, edit, and delete income and expense entries.
- **Bank Account Management**: Track multiple bank accounts and associate transactions with them.
- **Category Management**: Create and manage custom categories for better organization of transactions.
- **Data Visualization**: View financial summaries through charts for better insights.
- **Data Persistence**: All data is stored in a SQL Server database, ensuring reliability and consistency.

## Technologies Used

- **ASP.NET Core (.NET 9.0)**: Modern framework for building scalable web applications.
- **Entity Framework Core**: Code-first database management for efficient development.
- **SQL Server**: Robust relational database system for storing application data.
- **Bootstrap**: Provides responsive design elements for an enhanced user experience.

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) (or any preferred C# IDE)

### Installation

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/UhhhItsMax/IncomeExpenseManager.git
   ```
2. **Navigate to the Project Directory**:
   ```bash
   cd IncomeExpenseManager
   ```
3. **Restore NuGet Packages**:
   Open the solution in Visual Studio and restore the NuGet packages to ensure all dependencies are available.

4. **Update Database Connection String**:
   Modify the `appsettings.json` file to include your SQL Server connection string:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=IncomeExpenseDB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
5. **Apply Migrations**:
   Use the Package Manager Console in Visual Studio to apply the database migrations:
   ```powershell
   Update-Database
   ```

### Running the Application

1. **Build the Solution**:
   Ensure the project builds without errors.

2. **Run the Application**:
   Start the application using Visual Studio's debugging tools or by running the project from the command line:
   ```bash
   dotnet run
   ```
3. **Access the Application**:
   Open a web browser and navigate to `https://localhost:5001` to start using IncomeExpenseManager.

## Usage

- **Register a New Account**: Sign up to create a personalized account.
- **Log In**: Access your account using your credentials.
- **Manage Bank Accounts**: Add and track multiple bank accounts.
- **Manage Transactions**: Add, edit, or delete income and expense entries.
- **Manage Categories**: Create, edit, or delete categories for better financial organization.
- **View Financial Charts**: Visualize transaction history with charts for better insights.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your proposed changes. Ensure that your code adheres to the existing coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.

---

*Note: This README provides an updated overview of the IncomeExpenseManager application, including newly added features such as bank account management and financial visualization charts.*

