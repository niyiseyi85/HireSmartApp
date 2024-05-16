# HireSmartApp

HireSmartApp is a web application that allows employers to create programs and candidates to apply for these programs. The application uses Identity Server 4 for role-based permissions, ensuring secure and appropriate access for both employers and candidates.

## Features

- Employers can create and manage programs.
- Candidates can apply to programs.
- Role-based permissions using Identity Server 4.
- Secure user authentication and authorization.

## Technologies Used

- **ASP.NET Core**: Web API framework.
- **Entity Framework Core**: ORM for database interactions.
- **MongoDB**: NoSQL database for storing application data.
- **Identity Server 4**: Authentication and authorization.
- **AutoMapper**: Object-object mapping.
- **FluentValidation**: Validation framework.
- **Moq**: Mocking framework for unit testing.
- **xUnit**: Testing framework.

## Getting Started

### Prerequisites

- .NET Core SDK
- MongoDB
- SQL Server (for Identity Server and relational data)
- Node.js (for front-end, if applicable)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/HireSmartApp.git
   cd HireSmartApp
