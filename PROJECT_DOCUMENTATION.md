# TaskBoard MVC Project - Complete Documentation

## Table of Contents
1. [Project Overview](#project-overview)
2. [What is MVC?](#what-is-mvc)
3. [Technology Stack](#technology-stack)
4. [Project Structure](#project-structure)
5. [Database Schema](#database-schema)
6. [Features](#features)
7. [User Roles & Permissions](#user-roles--permissions)
8. [Frontend Architecture](#frontend-architecture)
9. [How to Run the Project](#how-to-run-the-project)
10. [Frontend Development Guide](#frontend-development-guide)
11. [Common Tasks for Frontend Developers](#common-tasks-for-frontend-developers)

---

## Project Overview

**TaskBoard** is a task management web application built using ASP.NET Core MVC. It allows users to create, manage, and track tasks with different priority levels and statuses. The application has two types of users:
- **Admin users** - Can view all tasks and manage all users
- **Regular users** - Can view and manage only their own tasks

### Key Features:
- User authentication & authorization (Login/Register)
- Task creation, editing, and deletion
- Task prioritization (Low, Medium, High)
- Task status tracking (Pending, In Progress, Done)
- Admin dashboard to manage users
- Role-based access control

---

## What is MVC?

**MVC** stands for **Model-View-Controller**. It's an architectural pattern that separates an application into three interconnected components:

### 1. **Model**
- Represents the data structure and business logic
- Handles data validation and database operations
- In this project: `AppUser.cs`, `TaskItem.cs`, and `ApplicationDbContext.cs`
- Example: The `TaskItem` model defines what a task looks like (title, description, priority, status, etc.)

### 2. **View**
- The presentation layer (HTML/CSS/JavaScript)
- Displays data to users and collects user input
- Uses Razor syntax (`.cshtml` files) which mixes C# code with HTML
- In this project: All files in the `Views/` folder
- Examples: `Home/Index.cshtml` (dashboard), `Tasks/Create.cshtml` (create task form)

### 3. **Controller**
- Handles user requests and coordinates between Model and View
- Processes user input, updates the model, and determines which view to display
- In this project: All files in the `Controllers/` folder
- Examples: `HomeController`, `TasksController`, `AccountController`

### How MVC Works:
```
User → Browser Request → Controller → Model (Database) → Controller → View → HTML Response → User
```

---

## Technology Stack

| Layer | Technology | Version | Purpose |
|-------|-----------|---------|---------|
| **Framework** | ASP.NET Core | 8.0 | Web application framework |
| **Language** | C# | Latest | Backend programming |
| **Database** | SQLite | Latest | Data storage (lightweight, file-based) |
| **ORM** | Entity Framework Core | 8.0.4 | Database abstraction layer |
| **Authentication** | Cookie-based | Built-in | User login/session management |
| **Frontend** | HTML5/CSS3/JavaScript | Latest | User interface |
| **CSS Framework** | Custom CSS | - | Styling (minimal framework) |
| **Font** | Google Fonts (Inter) | Latest | Typography |

### Key Dependencies:
- **Microsoft.EntityFrameworkCore.Sqlite** - SQLite database provider
- **Microsoft.EntityFrameworkCore.Design** - Migration tools
- **Microsoft.EntityFrameworkCore.Tools** - CLI tools for EF Core

---

## Project Structure

```
mvc-project/
├── Controllers/                    # Backend request handlers
│   ├── AccountController.cs        # Login, Register, Logout
│   ├── HomeController.cs           # Dashboard/Home page
│   ├── TasksController.cs          # Task CRUD operations
│   └── UsersController.cs          # User management (Admin)
│
├── Models/                         # Data structures & business logic
│   ├── ApplicationDbContext.cs     # Database configuration & migrations
│   ├── AppUser.cs                  # User data model
│   ├── TaskItem.cs                 # Task data model
│   ├── LoginViewModel.cs           # Login form data
│   ├── RegisterViewModel.cs        # Registration form data
│   └── ErrorViewModel.cs           # Error page data
│
├── Views/                          # UI Templates (Razor syntax)
│   ├── Home/
│   │   ├── Index.cshtml            # Dashboard (authenticated users)
│   │   └── Privacy.cshtml          # Privacy page
│   ├── Account/
│   │   ├── Login.cshtml            # Login form
│   │   └── Register.cshtml         # Registration form
│   ├── Tasks/
│   │   ├── Index.cshtml            # List all tasks
│   │   ├── Create.cshtml           # Create task form
│   │   └── Edit.cshtml             # Edit task form
│   ├── Users/
│   │   ├── Index.cshtml            # User management (Admin only)
│   │   ├── Create.cshtml           # Create user (Admin)
│   │   └── Edit.cshtml             # Edit user (Admin)
│   ├── Shared/
│   │   ├── _Layout.cshtml          # Master layout (navbar, footer)
│   │   ├── _Layout.cshtml.css      # Layout styles
│   │   └── Error.cshtml            # Error page template
│   └── _ViewImports.cshtml         # Global imports for views
│
├── wwwroot/                        # Static files (client-side)
│   ├── css/
│   │   └── site.css                # Global styles
│   ├── js/
│   │   ├── site.js                 # Global scripts
│   │   ├── tasks.js                # Task-specific scripts
│   │   └── validation.js           # Form validation scripts
│   └── lib/                        # Third-party libraries (jQuery, Bootstrap)
│
├── Migrations/                     # Database version control
│   └── InitialCreate.cs            # Initial schema creation
│
├── Properties/
│   └── launchSettings.json         # App configuration (ports, profiles)
│
├── Program.cs                      # Application startup configuration
├── mvc-project.csproj              # Project file (dependencies)
├── appsettings.json                # App settings (connection strings, logging)
├── appsettings.Development.json    # Development-specific settings
└── taskboard.db                    # SQLite database file (created after migration)
```

---

## Database Schema

### Users Table
```
Table: Users
├── Id (Integer, Primary Key)
├── Username (String, 3-20 chars, Required)
├── Password (String, 6+ chars, Required)  ⚠️ NOTE: Plain text (NOT SECURE - use hashing in production)
└── Role (String: "Admin" or "User")

Seed Data:
- admin / admin123 (Admin role)
- user / user123 (User role)
```

### Tasks Table
```
Table: Tasks
├── Id (Integer, Primary Key)
├── Title (String, Required)
├── Description (String, Required)
├── Priority (String: "Low", "Medium", "High")
├── Status (String: "Pending", "In Progress", "Done")
├── CreatedBy (String, User's username who created it)
└── CreatedAt (DateTime, Auto-set to now)

Seed Data:
- Task 1: Design Homepage (High priority, In Progress)
- Task 2: Fix Login Bug (High priority, Pending)
- Task 3: Write Documentation (Low priority, Done)
```

---

## Features

### 1. **Authentication System**
- Login with username & password
- User registration
- Cookie-based session management
- Logout functionality
- Password validation (min 6 characters)

### 2. **Task Management**
- **Create Tasks** - Users create tasks with title, description, priority, and status
- **View Tasks** - Users see their tasks, Admins see all tasks
- **Edit Tasks** - Update task details, priority, or status
- **Delete Tasks** - Remove completed or unwanted tasks
- **Filter & Sort** - Tasks are sorted by creation date

### 3. **Dashboard**
- **Welcome Message** - Personalized greeting
- **Quick Stats**
  - Total tasks count
  - Pending tasks count
  - In Progress tasks count
  - Completed tasks count
- **Quick Actions** - Buttons to navigate to tasks, create new task, logout

### 4. **User Management** (Admin Only)
- View all users
- Create new users
- Edit user details and roles
- Delete users

### 5. **Role-Based Access Control**
- **Regular Users** - Can only see/manage their own tasks
- **Admins** - Can see all tasks, manage users, access admin dashboard

---

## User Roles & Permissions

### Admin User
```
Login: admin / admin123
Permissions:
✅ View all tasks (not just their own)
✅ Edit/Delete any task
✅ Access "Manage Users" button in navbar
✅ Create new users
✅ Edit/Delete users
✅ View all dashboard statistics
```

### Regular User
```
Login: user / user123
Permissions:
✅ Create new tasks
✅ View only their own tasks
✅ Edit/Delete only their own tasks
❌ Cannot see other users' tasks
❌ Cannot access user management
```

### Public (Unauthenticated)
```
Access:
✅ View landing page (Home/Index when not authenticated)
✅ Access Login page
✅ Access Register page
❌ Cannot access Tasks, Dashboard, or User Management
```

---

## Frontend Architecture

### Rendering Pipeline

```
1. User Request
   ↓
2. Controller Method (e.g., TasksController.Index())
   ├── Processes request
   ├── Queries database through Model
   └── Passes data to View
   ↓
3. View (Razor Template .cshtml)
   ├── Receives data from Controller
   ├── Renders HTML
   ├── Can execute C# code in <@...> tags
   └── Can use data passed as Model or ViewBag
   ↓
4. Browser renders final HTML + CSS + JavaScript
   ↓
5. User sees the page
```

### View Technologies Used

#### **Razor Syntax** (`@` symbol)
```
@model TaskItem                 // Receive data from controller
@ViewData["Title"]              // Access view data
@ViewBag.Username               // Access dynamic properties
@if (User.Identity.IsAuthenticated) { ... }  // C# conditionals
@foreach (var task in Model) { ... }         // C# loops
<a asp-controller="Tasks" asp-action="Create"> // ASP.NET Tag Helpers
```

#### **Layout System**
- `_Layout.cshtml` - Master template with navbar, footer, common styles
- `_ViewStart.cshtml` - Auto-applies layout to all views
- `_ViewImports.cshtml` - Global imports (namespaces, tag helpers)

#### **Tag Helpers**
```
asp-controller="Tasks"          // Link to controller
asp-action="Index"              // Link to action method
asp-for="model.Title"           // Bind form field to model property
asp-validate="true"             // Client-side validation
```

### CSS Architecture

**Main Stylesheet**: `wwwroot/css/site.css`
- **Global Classes**:
  - `.btn`, `.btn-primary`, `.btn-outline` - Button styles
  - `.dashboard`, `.landing` - Page layouts
  - `.navbar`, `.nav-container` - Navigation styles
  - `.table`, `.form-group` - Common component styles

- **Inline Styles**: Many styles are applied directly in HTML using `style=""` attributes
  - Easy to understand but not ideal for large projects
  - Consider moving to `site.css` as the project grows

### JavaScript Files

| File | Purpose |
|------|---------|
| `site.js` | Global scripts (common functionality) |
| `tasks.js` | Task-specific interactions |
| `validation.js` | Form validation before submission |

---

## How to Run the Project

### Prerequisites
- .NET 8.0 SDK installed
- SQLite support (comes built-in)

### Installation & Running

```bash
# 1. Navigate to project directory
cd /home/abdulrahman/frontEnd/flaskWork/mvc-project

# 2. Restore NuGet packages
dotnet restore

# 3. Apply database migrations (creates tables & seed data)
dotnet ef database update

# 4. Run the application
dotnet run

# Application will be available at http://localhost:5278
```

### Login Credentials
```
Admin Account:
- Username: admin
- Password: admin123

Regular User Account:
- Username: user
- Password: user123
```

---

## Frontend Development Guide

### Understanding the View (UI) Layer

When you open a `.cshtml` file, you'll see a mix of HTML and C#:

```cshtml
<!-- Regular HTML -->
<div class="task-card">
    <!-- C# mixed with HTML -->
    @if (Model != null && Model.Count > 0)
    {
        @foreach (var task in Model)
        {
            <div class="task-item">
                <h3>@task.Title</h3>
                <p>@task.Description</p>
                <span class="priority priority-@task.Priority.ToLower()">@task.Priority</span>
                <a asp-action="Edit" asp-route-id="@task.Id">Edit</a>
            </div>
        }
    }
    else
    {
        <p>No tasks found</p>
    }
</div>
```

### Making UI Changes

#### Example 1: Change Welcome Message Color
**File**: `Views/Home/Index.cshtml`
```html
<!-- Before -->
<h1 style="color: #0056b3;">Welcome to your Dashboard, @username! 👋</h1>

<!-- After (change to red) -->
<h1 style="color: #ff0000;">Welcome to your Dashboard, @username! 👋</h1>
```

#### Example 2: Add a New Button
**File**: `Views/Tasks/Index.cshtml`
```html
<div class="quick-actions">
    <a asp-action="Index" class="btn btn-primary">View All Tasks</a>
    <a asp-action="Create" class="btn btn-primary">+ New Task</a>
    <a asp-action="Archive" class="btn btn-outline">Archived Tasks</a>  <!-- New button -->
</div>
```

#### Example 3: Change CSS Styling
**File**: `wwwroot/css/site.css`
```css
.task-card {
    /* Add or modify styles */
    border: 2px solid #ddd;
    border-radius: 8px;
    padding: 15px;
    margin: 10px 0;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
}
```

### Handling User Input (Forms)

Example from `Views/Tasks/Create.cshtml`:
```cshtml
@model TaskItem

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Title"></label>
        <input asp-for="Title" class="form-control" />
        <span asp-validation-for="Title" class="text-danger"></span>
    </div>
    
    <div class="form-group">
        <label asp-for="Description"></label>
        <textarea asp-for="Description" class="form-control"></textarea>
        <span asp-validation-for="Description" class="text-danger"></span>
    </div>
    
    <button type="submit" class="btn btn-primary">Create Task</button>
</form>
```

When user submits:
1. Form data is sent to `TasksController.Create(TaskItem model)` as HTTP POST
2. Model binding automatically maps form fields to model properties
3. Server-side validation checks if data is valid
4. If valid, task is saved to database
5. User is redirected to task list

---

## Common Tasks for Frontend Developers

### Task 1: Add a New Column to Task List
**What you're changing**: `Views/Tasks/Index.cshtml`

```cshtml
<!-- In the table header row -->
<tr>
    <th>Title</th>
    <th>Priority</th>
    <th>Status</th>
    <th>Due Date</th>  <!-- New column -->
    <th>Actions</th>
</tr>

<!-- In the data rows -->
@foreach (var task in Model)
{
    <tr>
        <td>@task.Title</td>
        <td>@task.Priority</td>
        <td>@task.Status</td>
        <td>@task.CreatedAt.ToString("yyyy-MM-dd")</td>  <!-- New column data -->
        <td>
            <a asp-action="Edit" asp-route-id="@task.Id">Edit</a>
            <a asp-action="Delete" asp-route-id="@task.Id">Delete</a>
        </td>
    </tr>
}
```

### Task 2: Change Navigation Bar Styling
**What you're changing**: `Views/Shared/_Layout.cshtml` or `wwwroot/css/site.css`

```html
<!-- Make navbar background blue -->
<nav class="navbar" style="background-color: #0056b3; color: white; padding: 1rem;">
    <div class="nav-container">
        <a class="nav-brand" style="color: white; font-size: 1.5rem; font-weight: bold;">
            TaskBoard
        </a>
        <!-- Rest of navbar -->
    </div>
</nav>
```

### Task 3: Add Form Validation Message
**What you're changing**: `Views/Tasks/Create.cshtml`

```cshtml
@model TaskItem

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="Title">Task Title *</label>
        <input asp-for="Title" class="form-control" required />
        <small>Max 100 characters, required field</small>
        <span asp-validation-for="Title" class="text-danger"></span>
    </div>
</form>
```

### Task 4: Add Status Filter/Badge
**What you're changing**: `Views/Tasks/Index.cshtml`

```html
@foreach (var task in Model)
{
    <div class="task-item">
        <h3>@task.Title</h3>
        
        <!-- Status Badge -->
        @{
            var statusColor = task.Status switch
            {
                "Pending" => "badge-warning",
                "In Progress" => "badge-info",
                "Done" => "badge-success",
                _ => "badge-secondary"
            };
        }
        <span class="badge @statusColor">@task.Status</span>
    </div>
}
```

### Task 5: Modify Dashboard Stats Display
**What you're changing**: `Views/Home/Index.cshtml`

```cshtml
<!-- Current dashboard stats -->
<div class="dashboard-stats">
    <div class="stat-card">
        <h4>@ViewBag.TotalTasks</h4>
        <p>Total Tasks</p>
    </div>
    
    <div class="stat-card">
        <h4>@ViewBag.PendingTasks</h4>
        <p>Pending</p>
    </div>
    
    <div class="stat-card">
        <h4>@ViewBag.InProgressTasks</h4>
        <p>In Progress</p>
    </div>
    
    <div class="stat-card">
        <h4>@ViewBag.CompletedTasks</h4>
        <p>Completed</p>
    </div>
</div>

<style>
    .dashboard-stats {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
        gap: 1rem;
        margin: 2rem 0;
    }
    
    .stat-card {
        background: #f8f9fa;
        padding: 1.5rem;
        border-radius: 8px;
        text-align: center;
        border-left: 4px solid #0056b3;
    }
    
    .stat-card h4 {
        font-size: 2rem;
        margin: 0;
        color: #0056b3;
    }
    
    .stat-card p {
        margin: 0.5rem 0 0 0;
        color: #666;
    }
</style>
```

---

## Important Notes for Frontend Developers

### ✅ Best Practices

1. **Use ASP.NET Tag Helpers**
   ```csharp
   // Good ✅
   <a asp-controller="Tasks" asp-action="Edit" asp-route-id="@task.Id">Edit</a>
   
   // Avoid ❌
   <a href="/Tasks/Edit/@task.Id">Edit</a>
   ```

2. **Separate concerns**
   - CSS should be in `site.css`, not inline `style=""`
   - JavaScript should be in separate `.js` files
   - Complex logic should be in Controller, not View

3. **Use meaningful class names**
   ```html
   <!-- Good ✅ -->
   <div class="task-list-item">
   
   <!-- Avoid ❌ -->
   <div class="item1">
   ```

4. **Keep Views simple**
   - Views should only contain presentation logic
   - Complex calculations should be in the Controller
   - Pass processed data to View via Model or ViewBag

### ⚠️ Security Considerations

1. **Password Storage** ⚠️
   - Current implementation stores passwords in PLAIN TEXT
   - **Production**: Use bcrypt, Argon2, or PBKDF2
   - Never commit passwords to git

2. **SQL Injection**
   - Entity Framework Core prevents this automatically
   - Avoid raw SQL queries

3. **Authentication**
   - Current: Simple cookie-based auth
   - Consider OAuth2 / OpenID Connect for production

---

## Troubleshooting

### Database Issues
```
Error: "SQLite Error 1: 'no such table: Users'"
Solution: Run `dotnet ef database update`

Error: "Failed to bind to address"
Solution: Port is in use. Change port in launchSettings.json or kill process
```

### View Not Found
```
Error: "The view 'Create' was not found"
Solution: Make sure view file exists in correct folder:
         Views/[ControllerName]/[ActionName].cshtml
```

### Model Binding Issues
```
Form fields not mapping to model properties
Solution: Ensure form field names match model property names
         Use asp-for tag helper to auto-generate correct names
```

---

## Next Steps

As a frontend developer, you can now:

1. **Modify UI** - Change colors, layouts, button styles in `.cshtml` files
2. **Add new pages** - Create new `.cshtml` views and link them from Controller
3. **Improve styling** - Enhance `site.css` with better design
4. **Add JavaScript interactivity** - Use `site.js` to add dynamic features
5. **Create new features** - Work with backend developer to add new functionality

Good luck with your TaskBoard project! 🚀

