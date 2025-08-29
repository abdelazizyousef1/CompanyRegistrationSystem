CompanyRegistrationSystem

Overview
CompanyRegistrationSystem is a web application that allows anonymous users to register as a company through a secure portal. The system supports company sign-up, OTP-based email verification, password setting, and login functionality. The backend is built with a .NET Core MVC Web API following a layered architecture (Data, Repository, Services, API), while the frontend is developed using Angular 16. PostgreSQL is used as the database, ensuring robust data management.
This project demonstrates my skills in full-stack development, RESTful API design, database management, and clean code practices, making it a strong addition to my portfolio.
Features

Company Sign-Up: Users can register a company with Arabic and English names, email, phone number (optional), website URL (optional), and company logo (optional).
Form Validation: Ensures required fields (company names, email) are filled, with email uniqueness and format validation.
Logo Upload: Users can upload and preview a company logo before submission.
OTP Verification: After sign-up, a simulated OTP is sent to the company email and displayed in a tooltip for validation.
Password Setting: Users set a password (min. 6 characters, with capital letter, number, and special character) after OTP validation.
Login System: Users can log in with email and password, accessing a home page with the company logo, name, and logout button.
Clean Code: Follows SOLID principles, dependency injection, and modular design.

Tech Stack

Backend: .NET Core 8, Entity Framework Core, ASP.NET Core MVC Web API
Frontend: Angular 16, TypeScript, HTML/CSS
Database: PostgreSQL
Other Tools: Git, Postman (for API testing), npm

Project Structure
CompanyRegistrationSystem/
├── BackEnd/
│   ├── Data/               # Entity Framework models and DbContext
│   ├── Repository/         # Data access logic
│   ├── Services/           # Business logic and OTP generation
│   ├── API/                # Controllers and API endpoints
│   └── appsettings.json    # Configuration (e.g., database connection)
├── FrontEnd/               # Angular 16 application
│   ├── src/app/components/ # Angular components (sign-up, OTP, password, login, home)
│   ├── src/app/services/   # API service for HTTP requests
│   └── src/assets/         # Static assets (e.g., logo uploads)
└── README.md               # Project documentation

Installation
Backend (.NET Core)

Clone the repository:
git clone https://github.com/abdelazizyousef1/CompanyRegistrationSystem.git
cd CompanyRegistrationSystem/BackEnd


Install dependencies:
dotnet restore


Set up PostgreSQL:

Create a database named CompanyDB.
Update the connection string in appsettings.json:{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=CompanyDB;Username=postgres;Password=your_password"
  }
}


Apply migrations:dotnet ef database update




Run the backend:
dotnet run

The API will start at https://localhost:5001.


Frontend (Angular)

Navigate to the frontend directory:
cd CompanyRegistrationSystem/FrontEnd


Install dependencies:
npm install


Configure API URL:Update src/environments/environment.ts with the backend URL:
export const environment = {
  production: false,
  apiUrl: 'https://localhost:5001/api'
};


Run the frontend:
ng serve

The app will be available at http://localhost:4200.


Usage

Sign Up:

Navigate to /signup on the frontend.
Fill in the form (Company Arabic/English Name, Email, Phone Number, Website URL, Logo).
Preview the uploaded logo and click "Sign Up".
A simulated OTP is displayed in a tooltip.


OTP Validation:

Enter the OTP from the tooltip on the OTP validation page.
If valid, proceed to the password setting page.


Set Password:

Enter a new password and confirm it (must include capital letter, number, special character, and be >6 characters).
Submit to set the password and redirect to the login page.


Login:

Log in with the company email and password.
On success, view the home page showing the company logo, "Hello {Company Name}", and a logout button.



API Endpoints

POST /api/companies/register: Register a new company.
POST /api/companies/verify-otp: Validate OTP.
POST /api/companies/set-password: Set company password.
POST /api/auth/login: Log in and receive a JWT token.
GET /api/companies/profile: Get company profile (requires authentication).


Contributing
Contributions are welcome! To contribute:

Fork the repository.
Create a new branch (git checkout -b feature/your-feature).
Commit your changes (git commit -m "Add your feature").
Push to the branch (git push origin feature/your-feature).
Open a pull request.

Please read CONTRIBUTING.md for more details.
License
This project is licensed under the MIT License.
Contact

Author: Abdelaziz Yousef
Email: abdelazizyousef158@gmail.com
LinkedIn: https://www.linkedin.com/in/abdelazizyousef158/


Feel free to star ⭐ this repository if you find it useful!
