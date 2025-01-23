
# IncomeExpenseManager

IncomeExpenseManager is a web application designed to help users efficiently track and manage their incomes and expenses. By categorizing transactions and providing insightful summaries, it enables users to maintain a clear overview of their financial activities.

## Features

- **User Authentication**: Secure user registration and login functionalities to ensure personalized data management.
- **Transaction Management**: Add, edit, and delete income and expense entries with ease.
- **Category Management**: Create and manage custom categories for better organization of transactions.
- **Data Persistence**: All data is stored in a SQL Server database, ensuring reliability and consistency.

## Technologies Used

- **ASP.NET Core**: Serves as the primary framework for building the web application.
- **Entity Framework Core**: Facilitates database interactions using the code-first approach.
- **SQL Server**: Acts as the relational database management system for storing application data.
- **Bootstrap**: Provides responsive design elements for an enhanced user interface.

## Getting Started

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
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
- **Manage Transactions**: Add new income or expense entries, assign them to categories, and view your transaction history.
- **Manage Categories**: Create, edit, or delete categories to organize your transactions effectively.

## Contributing

Contributions are welcome! Please fork the repository and create a pull request with your proposed changes. Ensure that your code adheres to the existing coding standards and includes appropriate tests.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE.txt) file for more details.

---

*Note: This README provides a comprehensive overview of the IncomeExpenseManager application, including setup instructions and usage guidelines.*
