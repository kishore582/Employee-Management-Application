**Employee Management Application - Documentation**

**Project Overview**
This project is an Employee Management System developed using C# as a three-tier architecture. It allows users to perform basic CRUD operations on employee records and manage employee addresses through an intuitive user interface. The project uses an MSSQL database to store employee details.

**Architecture**
The application follows a three-tier architecture with the following layers:

1. Presentation Layer (UI): This layer includes .aspx pages that provide the user interface for interacting with the system.
2. Business Access Layer (BAL): This layer contains the logic for handling business rules and communication between the UI and the database.
3. Data Access Layer (DAL): This layer handles the database interaction and operations like CRUD for employees and addresses.

**Features**
- Login Functionality: Allows users to authenticate by entering a username and password. This feature is implemented without using any frameworks.
- CRUD Operations for Employee: Add, view, update, and delete employee information in the Employee table.
- Manage Employee Address: Add, view, update, and delete employee address details. This is connected to the Employee_Address table.
- Expandable Fields on Name Click: Clicking on the 'Name' field of an employee expands additional fields to add or update address information.
