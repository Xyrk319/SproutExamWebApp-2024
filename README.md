# Sprout Exam Web App 

## Overview
This is a Salary Calculator system based on the Technical Exam boilerplate given by Sprout's Exam Coordinator 

## Features
- **Employee Creation**: Add new employees with details such as full name, birthdate, TIN (Tax Identification Number), and employee type.
- **Backend Form Validation**: All fields for creating and editing an Employee are "required" for validation. At the same time, TIN has an additional validation, having a format of XXX-XXX-XXX-XXX where X is a number.
- **Error Handling**: Displays validation errors in the frontend create and edit forms.

## Technologies Used
- **Frontend**: React
- **Backend**: ASP.NET Core Web API
- **Database**: MS SQL
- Node.js: Version 16.20.2 or below
- .NET 5.0 SDK:

## Setup and Installation
1. Clone the repository
2. Setup a MS SQL Server
3. Using SQL Server Management Studio or any other Database Clients, restore the database .bak file
4. Run the solution or the csproj using terminal (dotnet run) or via VS Code
5. Open browser and go to localhost:5001 (default port in the code)

## API Endpoints
- GET /api/employees
- GET /api/employees/{id}
- PUT /api/employees/{id}
- POST /api/employees
- DELETE /api/employees/{id}
- POST /api/employees/{id}/calculate
