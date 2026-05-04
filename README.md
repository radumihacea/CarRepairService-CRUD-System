# 🚗 Auto Service Management System

A distributed, Client-Server desktop application built with C# and .NET Framework for managing an auto repair shop. The project demonstrates a complete C.R.U.D. (Create, Read, Update, Delete) architecture using a modern WPF client and a SOAP Web Service connected to a SQL Server database.

## ✨ Features
*   **Create:** Register new cars entering the service (License Plate, Brand, Reported Issue).
*   **Read:** View all currently registered cars in a clean, modern DataGrid.
*   **Update:** Modify existing records (e.g., updating a car's repair status).
*   **Delete:** Remove records once the car is fixed and returned to the customer.
*   **Search/Filter:** Quickly find specific cars by their license plate using the `LIKE` SQL operator.
*   **Dynamic UI Formatting:** The DataGrid automatically highlights a row in green when the reported issue is marked exactly as "Rezolvat" (Resolved), utilizing WPF DataTriggers.
*   **Interactive Selection:** Clicking on any row in the table automatically populates the form fields for quick editing.

## 🛠️ Technologies & Architecture
This project is split into two main components:

1.  **Backend (Server):**
    *   **ASP.NET Web Service (.asmx):** Acts as the middleman securely handling requests.
    *   **SQL Server (LocalDB):** A `.mdf` database used for permanent data persistence.
    *   **ADO.NET:** Used for secure database connections and parameterized SQL queries to prevent SQL Injection.

2.  **Frontend (Client):**
    *   **WPF (Windows Presentation Foundation):** Replaced legacy WinForms with a modern XAML-based UI, utilizing Grids, StackPanels, and custom styling.
    *   **SOAP Protocol:** Used by the client to communicate with the Web Service.

## 🚀 How to Run Locally

1.  **Clone the repository:**
    `git clone https://github.com/YourUsername/YourRepositoryName.git`
2.  **Start the Server:**
    *   Open the Server project (`ServerServiceAuto.sln`) in Visual Studio.
    *   Press `F5` to run the ASP.NET Web Service. Keep the browser window open.
3.  **Start the Client:**
    *   Open the Client project (`ClientWPFServiceAuto.sln`) in a new Visual Studio window.
    *   *(Optional)* If the server port changed, update the Service Reference (`RefService`) in the Solution Explorer.
    *   Press `F5` to run the WPF desktop application.

## 📝 License
This project was created for educational purposes. Feel free to use or modify it!
