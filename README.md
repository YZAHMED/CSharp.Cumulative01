# Cumulative01 Teacher Management System

Hey there! This project is a simple Teacher Management System that lets you interact with an API and see a list of teachers. It's built with ASP.NET Core and MySQL for storing and managing teacher data.

## Table of Contents

1. [Overview](#overview)
2. [Technologies Used](#technologies-used)
3. [API Endpoints](#api-endpoints)
4. [Controller Details](#controller-details)
5. [Frontend Integration](#frontend-integration)
6. [Setup Instructions](#setup-instructions)

## Overview

This project has two main parts:
- **API controller** that talks to the database to get the teachers' info.
- **Page controller** that sends the teacher data to the frontend to be shown in a list or as detailed information about a single teacher.

The app lets you:
- List all teachers
- View details of a teacher by ID
- Filter teachers based on their hire date

## Technologies Used

- **ASP.NET Core** for the web framework
- **MySQL** to store the teacher data
- **C#** for all the backend code
- **MVC** (Model-View-Controller) pattern for structure

## API Endpoints

### 1. **TeacherAPIController**

This controller handles requests for teacher data from the database.

- **GET api/Teacher/List**
  
  Returns a list of all teachers.

  **Example Response:**
  ```json
  [
    {"TeacherId": 1, "TeacherFirstName": "John", "TeacherLastName": "Doe", "EmployeeID": "E123", "HireDate": "2015-06-23T00:00:00", "Salary": 50000.00},
    {"TeacherId": 2, "TeacherFirstName": "Jane", "TeacherLastName": "Smith", "EmployeeID": "E124", "HireDate": "2016-07-10T00:00:00", "Salary": 55000.00}
  ]
