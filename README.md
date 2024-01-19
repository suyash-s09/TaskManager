# Command Line Task Manager 

## Overview

Command Line Task Manager is a command line application implemented in C# for managing tasks. The application provides two modes: Admin and User. Admins can create, add, delete, update, and read tasks as well as users, while users can read and update task statuses.

## Features

### Admin Mode

- **Create a new task:** Admins can create a new task by providing details such as task name, description, etc.
- **Delete tasks:** Admins have the capability to delete tasks by specifying the task identifier.
- **Update task details:** Admins can update task details like name, description, etc.
- **Read all tasks:** Admins can view a list of all tasks in the system.

- **Create a new User:** Admins can create a new user by providing details such as userId and password.
- **Delete User:** Admins have the capability to delete User by specifying the user identifier.
- **Update User details:** Admins can update task details like userId and password.
- **Read all user deatils:** Admins can view a list of all users in the system.

### User Mode

- **Read all tasks:** Users can view a list of all tasks in the system.
- **Update task status:** Users can update the status of a task (e.g., mark it as completed, in progress).

## Getting Started

### Prerequisites

- .NET SDK (3.1 or higher recommended)

### Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/suyash-s09/TaskManager.git

2. **Change Directory:**

   ```bash
   cd TaskManager/TaskmangerApp

3. **Build the application:**

   ```bash
   dotnet build

4. **Run the application:**

   ```bash
   dotnet run

## Usage

- Follow the on-screen prompts to navigate through the application.
- Use Admin Mode for task and user management functions and User Mode for task status updates.


