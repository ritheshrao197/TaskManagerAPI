# 📚 Task Manager API - Complete Learning Guide & README

# Task Manager API

A comprehensive RESTful API built with ASP.NET Core 8 that demonstrates full CRUD operations, JWT authentication, and role-based authorization.

## 📖 What I Learned From This Project

This project taught me modern backend development with .NET Core, from authentication to database operations. Here's everything I mastered:

### 1. **ASP.NET Core Web API Fundamentals**
- Creating RESTful endpoints with controllers
- Understanding HTTP methods (GET, POST, PUT, PATCH, DELETE)
- Implementing proper HTTP status codes (200, 201, 400, 401, 403, 404)
- Request/response handling and model binding
- Dependency Injection (DI) pattern
- Middleware pipeline configuration

### 2. **JWT (JSON Web Token) Authentication**
- Generating secure tokens for user authentication
- Implementing token validation and expiration
- Adding claims (user ID, username, email, role)
- Bearer token implementation in Swagger
- Securing endpoints with `[Authorize]` attribute
- Token-based authorization flow

### 3. **Entity Framework Core & Database Design**
- Code-first database approach
- Creating models and relationships (one-to-many)
- DbContext configuration and lifecycle
- Migrations: creating, updating, and applying
- Seeding initial data (admin user)
- LINQ queries for data access
- SQL Server connection setup

### 4. **Role-Based Authorization**
- Implementing User/Admin roles
- Role checking in controllers
- Data isolation based on user roles
- Admin privileges (view all users' tasks)
- Using `[Authorize(Roles = "Admin")]` attribute

### 5. **Security Best Practices**
- Password hashing with BCrypt
- Never storing plain text passwords
- JWT secret key management
- Protecting sensitive configuration
- Input validation with data annotations
- SQL injection prevention via EF Core

### 6. **API Architecture Patterns**
- Clean separation of concerns (Controllers, Models, Data, Services)
- Repository pattern through DbContext
- DTOs (Data Transfer Objects) for authentication
- Service layer for business logic (JwtService)
- Interface-based programming

### 7. **Error Handling & Validation**
- Model state validation
- Custom error messages
- HTTP response types (BadRequest, Unauthorized, Forbidden, NotFound)
- Null handling and defensive programming

### 8. **Swagger/OpenAPI Documentation**
- Auto-generating API documentation
- Adding JWT bearer authentication in Swagger
- Documenting endpoints with XML comments
- Interactive API testing interface

### 9. **Database Operations**
- CRUD operations via Entity Framework
- Relationship management (User → Tasks)
- Cascade delete configuration
- Query filtering based on user context
- Async/await patterns for database calls

### 10. **Project Configuration**
- appsettings.json management
- Environment-specific configurations
- Connection strings setup
- CORS configuration basics
- Kestrel server configuration

### 11. **Development Tools & Commands**
- dotnet CLI commands (new, build, run, add package, ef migrations)
- Package Manager Console
- SQL Server LocalDB setup
- Debugging techniques

### 12. **HTTP & REST Concepts**
- RESTful API design principles
- Stateless communication
- Resource gaming conventions
- Idempotent operations (PUT, DELETE)
- Partial updates with PATCH

## 🚀 Features

- ✅ **Full CRUD Operations** - Create, Read, Update, Delete tasks
- ✅ **JWT Authentication** - Secure token-based authentication
- ✅ **Role-Based Authorization** - Admin and User roles
- ✅ **User Management** - Registration and login
- ✅ **Task Management** - Personal tasks for each user
- ✅ **Admin Privileges** - Admin can view/manage all tasks
- ✅ **Swagger Documentation** - Interactive API documentation
- ✅ **SQL Server Database** - Persistent data storage
- ✅ **Password Hashing** - BCrypt for security
- ✅ **Data Validation** - Input validation and error handling

## 🛠️ Technologies Used

| Technology | Version | Purpose |
|------------|---------|---------|
| .NET 8 | 8.0 | Framework |
| ASP.NET Core | 8.0 | Web API |
| Entity Framework Core | 8.0 | ORM |
| SQL Server | LocalDB | Database |
| JWT Bearer | 8.0 | Authentication |
| BCrypt | 4.0.3 | Password Hashing |
| Swashbuckle | 6.5.0 | API Documentation |

## 📋 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code / Any IDE

## 🔧 Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd TaskManagerAPI
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Update Database Connection (Optional)
Edit `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=TaskManagerDB;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### 4. Install EF Core Tools (If not installed)
```bash
dotnet tool install --global dotnet-ef
```

### 5. Create Database & Apply Migrations
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### 6. Run the Application
```bash
dotnet run
```

### 7. Access Swagger UI
Open browser to: `http://localhost:5003/swagger`

## 📚 API Documentation

### Authentication Endpoints

#### Register User
```http
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "username": "johndoe",
  "password": "Password123!"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "johndoe",
  "password": "Password123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIs...",
  "username": "johndoe",
  "email": "user@example.com",
  "role": "User"
}
```

### Task Endpoints (All require Bearer token)

#### Get All Tasks
```http
GET /api/tasks
Authorization: Bearer <your-token>
```

#### Get Single Task
```http
GET /api/tasks/{id}
Authorization: Bearer <your-token>
```

#### Create Task
```http
POST /api/tasks
Authorization: Bearer <your-token>
Content-Type: application/json

{
  "title": "Complete project",
  "description": "Finish the API documentation",
  "dueDate": "2024-12-31T23:59:59Z"
}
```

#### Update Task
```http
PUT /api/tasks/{id}
Authorization: Bearer <your-token>
Content-Type: application/json

{
  "title": "Updated title",
  "description": "Updated description",
  "isCompleted": true,
  "dueDate": "2024-12-31T23:59:59Z"
}
```

#### Toggle Task Completion
```http
PATCH /api/tasks/{id}/complete
Authorization: Bearer <your-token>
```

#### Delete Task
```http
DELETE /api/tasks/{id}
Authorization: Bearer <your-token>
```

## 👥 User Roles

### Regular User
- Register and login
- Create, read, update, delete their own tasks
- View only their tasks

### Admin User
- All user permissions
- View all users' tasks
- Manage any task in the system

**Default Admin Credentials:**
- Username: `admin`
- Password: `Admin123!`

## 🔒 Security Features

- **Password Hashing**: BCrypt algorithm (salt + hash)
- **JWT Tokens**: 7-day expiration, signed with HMAC-SHA256
- **Role-Based Access**: Separate endpoints for different roles
- **Input Validation**: Data annotations on all models
- **SQL Injection Protection**: Entity Framework parameterized queries
- **CORS Ready**: Configurable cross-origin resource sharing

## 📁 Project Structure

```
TaskManagerAPI/
├── Controllers/
│   ├── AuthController.cs          # Authentication endpoints
│   └── TasksController.cs         # CRUD operations
├── Models/
│   ├── User.cs                    # User entity
│   ├── TaskItem.cs                # Task entity
│   └── AuthModels.cs              # DTOs for auth
├── Data/
│   └── ApplicationDbContext.cs    # Database context
├── Services/
│   └── JwtService.cs              # Token generation
├── Migrations/                    # EF migrations (auto-generated)
├── Program.cs                     # Application entry point
└── appsettings.json               # Configuration
```

## 🧪 Testing the API

### Using Swagger UI
1. Run `dotnet run`
2. Navigate to `http://localhost:5003/swagger`
3. Register a user via `/api/auth/register`
4. Login via `/api/auth/login`
5. Copy the token
6. Click "Authorize" button and enter: `Bearer <token>`
7. Test any task endpoint

### Using cURL

```bash
# Register
curl -X POST http://localhost:5003/api/auth/register \
  -H "Content-Type: application/json" \
  -d '{"email":"test@test.com","username":"testuser","password":"Password123!"}'

# Login
curl -X POST http://localhost:5003/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"testuser","password":"Password123!"}'

# Create Task (use token from login response)
curl -X POST http://localhost:5003/api/tasks \
  -H "Authorization: Bearer YOUR_TOKEN" \
  -H "Content-Type: application/json" \
  -d '{"title":"My Task","description":"Learning ASP.NET Core"}'
```

### Using PowerShell

```powershell
# Login and get token
$response = Invoke-RestMethod -Uri "http://localhost:5003/api/auth/login" `
  -Method POST `
  -ContentType "application/json" `
  -Body '{"username":"testuser","password":"Password123!"}'

$token = $response.token

# Create task
Invoke-RestMethod -Uri "http://localhost:5003/api/tasks" `
  -Method POST `
  -Headers @{Authorization = "Bearer $token"} `
  -ContentType "application/json" `
  -Body '{"title":"PowerShell Task","description":"Testing API"}'
```

## 🎯 Key Learning Outcomes

By completing this project, I gained practical experience in:

1. **Backend Development**: Building production-ready APIs
2. **Authentication Systems**: Implementing JWT from scratch
3. **Database Design**: Creating relationships and constraints
4. **Security**: Password hashing, token management, role-based access
5. **API Design**: RESTful principles and best practices
6. **Error Handling**: Proper HTTP responses and status codes
7. **Documentation**: Swagger/OpenAPI specifications
8. **Command Line**: dotnet CLI and EF Core tools
9. **Debugging**: Identifying and fixing common issues
10. **Architecture**: Clean separation of concerns

## 🚧 Future Improvements to Explore

- [ ] Refresh token mechanism
- [ ] Email verification on registration
- [ ] Password reset functionality
- [ ] Task categories and tags
- [ ] File attachments for tasks
- [ ] Task sharing between users
- [ ] Due date notifications
- [ ] Pagination and filtering
- [ ] API versioning
- [ ] Rate limiting
- [ ] Request logging with Serilog
- [ ] Unit and integration tests
- [ ] Docker containerization
- [ ] CI/CD pipeline setup
- [ ] Redis caching for frequent queries

## 🐛 Common Issues & Solutions

### Issue: dotnet-ef command not found
**Solution**: Install globally with `dotnet tool install --global dotnet-ef`

### Issue: Database connection failed
**Solution**: Ensure SQL Server LocalDB is running:
```bash
sqllocaldb start mssqllocaldb
```

### Issue: 401 Unauthorized
**Solution**: 
- Token may be expired (default 7 days)
- Ensure "Bearer " prefix is included
- Check token is correctly copied

### Issue: 403 Forbidden
**Solution**: User trying to access another user's task without admin role

### Issue: HTTPS redirect warning
**Solution**: Comment `app.UseHttpsRedirection();` in Program.cs for development

## 📊 Database Schema

### Users Table
| Column | Type | Description |
|--------|------|-------------|
| Id | INT (PK) | Auto-generated |
| Email | NVARCHAR(256) | Unique email |
| Username | NVARCHAR(100) | Unique username |
| PasswordHash | NVARCHAR(255) | BCrypt hash |
| Role | NVARCHAR(50) | Admin/User |
| CreatedAt | DATETIME2 | Registration date |

### Tasks Table
| Column | Type | Description |
|--------|------|-------------|
| Id | INT (PK) | Auto-generated |
| Title | NVARCHAR(200) | Task title |
| Description | NVARCHAR(1000) | Task details |
| IsCompleted | BIT | Completion status |
| CreatedAt | DATETIME2 | Creation date |
| DueDate | DATETIME2 | Optional deadline |
| UserId | INT (FK) | Owner reference |

## 🤝 What I'd Do Differently Next Time

1. **Use Result pattern** for better error handling
2. **Implement MediatR** for CQRS pattern
3. **Add FluentValidation** for complex validations
4. **Use AutoMapper** for object-object mapping
5. **Implement Global Exception Handling** middleware
6. **Add Health Checks** for monitoring
7. **Use Strongly Typed IDs** instead of primitive types
8. **Implement Soft Delete** pattern
9. **Add Audit Logging** for tracking changes
10. **Use Environment Variables** for secrets

## 📈 Skills Matrix

| Skill Area | Proficiency Gained |
|------------|-------------------|
| ASP.NET Core | ⭐⭐⭐⭐⭐ |
| JWT Authentication | ⭐⭐⭐⭐⭐ |
| Entity Framework | ⭐⭐⭐⭐ |
| REST API Design | ⭐⭐⭐⭐⭐ |
| Security Practices | ⭐⭐⭐⭐ |
| Database Design | ⭐⭐⭐⭐ |
| Swagger/OpenAPI | ⭐⭐⭐⭐ |
| Error Handling | ⭐⭐⭐⭐ |
| Testing | ⭐⭐⭐ |

## 🎓 Certificate of Completion

This project demonstrates understanding of:
- ✅ Building RESTful APIs with .NET 8
- ✅ Implementing secure authentication
- ✅ Role-based authorization
- ✅ Database operations with Entity Framework
- ✅ Following industry best practices

## 📄 License

This project is for educational purposes.

## 🙏 Acknowledgments

- Microsoft Documentation
- ASP.NET Core Community
- Open Source Contributors

---

## 🎉 Final Thoughts

This project transformed my understanding of backend development. I went from knowing basic C# to building a production-ready API with authentication, database integration, and security best practices. The hands-on experience with JWT, EF Core, and role-based authorization is invaluable for real-world applications.

**Time to Complete:** ~4-6 hours
**Lines of Code:** ~500
**Concepts Learned:** 20+
**Confidence Level:** Ready for production-like projects!

---

*"Learning by building is the best way to master technology."*

**Start using the API today and continue exploring!** 🚀
