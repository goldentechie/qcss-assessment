# PhoneBook Application

This project is a simple PhoneBook application with a **Backend** powered by `.Net Core 6.0` and a **Frontend** built with Angular.

---

## Backend: PhoneBookAPIs

### Prerequisites
- Install [.Net Core 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0).
- Ensure a database is set up and accessible.

### Setup Instructions
1. **Update Database Connection**:
   - Open the `AppSettings.json` file in the `PhoneBookAPIs` project.
   - Update the connection strings to match your database configuration.

2. **Apply Migrations**:
   - Open the **NuGet Package Manager Console** and run the following commands:
     ```bash
     add-migration initial
     update-database
     ```

3. **Start the Backend**:
   - Press the `Debug` button or run the project in your IDE.
   - The backend server will start on port `5000`.

4. **Access API Documentation**:
   - Open your browser and navigate to `http://localhost:5000`.
   - The Swagger API documentation page will be loaded.

---

## Frontend: PhoneBookApp

### Prerequisites
- Install [Node.js](https://nodejs.org) and npm.

### Setup Instructions
1. **Install Dependencies**:
   - Navigate to the `PhoneBookApp` directory and run:
     ```bash
     npm install
     ```

2. **Start the Frontend**:
   - Run the following command:
     ```bash
     ng serve
     ```
   - The application will start, and the login screen will appear.

3. **Login**:
   - Use the seeded credentials:
     - **Username**: `admin@phonebook.com`
     - **Password**: `Admin@123`

---

## Notes
- Ensure both backend and frontend servers are running simultaneously for proper functionality.
- For any issues, refer to the logs or documentation for troubleshooting.

---
