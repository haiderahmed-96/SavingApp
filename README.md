# 💰 SavingsApp

**A Modern ASP.NET Core 8 Application for Managing Savings Goals with Automatic Notifications**

![Build Status](https://img.shields.io/badge/build-passing-brightgreen)
![Framework](https://img.shields.io/badge/.NET-8.0-blue)
![License](https://img.shields.io/badge/license-MIT-green)

---

## 🎯 About

SavingsApp is a comprehensive savings management system built with **ASP.NET Core 8** that helps users:
- 💳 Create and manage multiple saving goals
- ✈️ Plan savings for specific purposes (travel, events, groups)
- 🔔 Receive automatic notifications for important events
- 👥 Collaborate in group savings
- 📊 Track transaction history

---

## ✨ Key Features

### 🎯 Saving Goals
- Create personalized saving goals
- Track progress toward targets
- Set specific durations
- Multiple saving types supported

### ✈️ Specialized Savings
- **Travel Savings**: Plan trips with currency conversion
- **Event Savings**: Save for special events
- **Group Savings**: Collaborate with others

### 📬 Smart Notifications
- Automatic alerts on transactions
- Goal completion notifications
- Group activity updates
- 12 different notification types

### 💪 Robust Validation
- FluentValidation on all inputs
- Custom error messages
- Comprehensive validation rules

### 🛡️ Error Handling
- Custom exception classes
- Proper HTTP status codes
- Detailed error responses

---

## 🚀 Quick Start

### Prerequisites
- .NET 8 SDK
- SQL Server
- Visual Studio 2022+ or VS Code

### Installation

```bash
# Clone repository
git clone https://github.com/haiderahmed-96/SavingApp.git
cd SavingsApp

# Restore packages
dotnet restore

# Update database
dotnet ef database update

# Run application
dotnet run
```

### Access Points
- **API**: https://localhost:5001
- **Swagger UI**: https://localhost:5001/swagger

---

## 📚 Documentation

| Document | Purpose |
|----------|---------|
| [COMPLETE_SUMMARY.md](COMPLETE_SUMMARY.md) | Full project overview |
| [QUICK_REFERENCE.md](QUICK_REFERENCE.md) | Quick command reference |
| [NOTIFICATIONS_GUIDE.md](NOTIFICATIONS_GUIDE.md) | Notifications system |
| [TESTING_GUIDE.md](TESTING_GUIDE.md) | Testing scenarios |
| [USAGE_GUIDE.md](USAGE_GUIDE.md) | API usage examples |
| [STATISTICS.md](STATISTICS.md) | Project metrics |

---

## 🔗 API Endpoints

### Saving Goals (4)
```
POST   /api/saving-goals
GET    /api/saving-goals/{id}
GET    /api/saving-goals
PUT    /api/saving-goals/{id}
```

### Travel Savings (4)
```
POST   /api/travel-saving
GET    /api/travel-saving/{goalId}
PUT    /api/travel-saving/{goalId}
DELETE /api/travel-saving/{goalId}
```

### Event Savings (5)
```
POST   /api/event-saving
GET    /api/event-saving/{goalId}
GET    /api/event-saving/goal/{goalId}/all
PUT    /api/event-saving/{goalId}
DELETE /api/event-saving/{goalId}
```

### Group Savings (5)
```
POST   /api/group-saving
GET    /api/group-saving/{goalId}
POST   /api/group-saving/{goalId}/members
DELETE /api/group-saving/{goalId}/members/{userId}
DELETE /api/group-saving/{goalId}
```

### Transactions (2)
```
POST /api/saving-transactions
POST /api/saving-transactions/withdraw
```

### Notifications (8)
```
POST   /api/notifications
GET    /api/notifications/user/{userId}
GET    /api/notifications/user/{userId}/unread-count
GET    /api/notifications/{id}
PUT    /api/notifications/{id}/mark-as-read
PUT    /api/notifications/user/{userId}/mark-all-as-read
PUT    /api/notifications/{id}/status
DELETE /api/notifications/{id}
```

---

## 📊 Architecture

```
SavingsApp
├── Controllers       (6 Controllers, 28 Endpoints)
├── Services         (4 Services, 4 Interfaces)
├── Models           (8 Entities, 9 Enums)
├── DTOs             (13 Data Transfer Objects)
├── Validators       (7 FluentValidation Rules)
├── Mappings         (AutoMapper Profiles)
├── Middlewares      (Exception Handling)
├── Data             (Entity Framework DbContext)
└── Migrations       (Database Migrations)
```

---

## 🎓 Technology Stack

| Technology | Version | Purpose |
|-----------|---------|---------|
| ASP.NET Core | 8.0 | Web Framework |
| Entity Framework Core | 8.0 | ORM |
| FluentValidation | 12.1.1 | Input Validation |
| AutoMapper | 12.0.1 | Object Mapping |
| SQL Server | Latest | Database |
| Swagger | Latest | API Documentation |

---

## 🔄 Development Phases

### ✅ Phase 1: Validation & Mapping
- Input validation with FluentValidation
- AutoMapper integration
- Custom exception handling
- Enhanced error middleware

### ✅ Phase 2: Notifications System
- In-app notifications
- Automatic alerts
- 12 notification types
- Full CRUD operations

### 📋 Phase 3: Authentication & JWT
- JWT token implementation
- Identity system
- Role-based authorization

### 🔜 Future Phases
- Unit Testing (xUnit)
- Logging (Serilog)
- Pagination & Filtering
- Repository Pattern
- Real-time Notifications (SignalR)

---

## 📈 Project Statistics

| Metric | Count |
|--------|-------|
| Controllers | 6 |
| Services | 4 |
| Entities | 8 |
| Enums | 9 |
| DTOs | 13 |
| Validators | 7 |
| API Endpoints | 28 |
| Database Migrations | 3 |
| Documentation Files | 7 |

---

## 🧪 Testing

See [TESTING_GUIDE.md](TESTING_GUIDE.md) for comprehensive testing scenarios.

Quick test:
```bash
# Create a saving goal
POST /api/saving-goals
{
  "userId": 1,
  "goalName": "Summer Vacation",
  "targetAmount": 3000,
  "durationDays": 180,
  "savingType": 0
}

# Check notifications
GET /api/notifications/user/1
```

---

## 🛡️ Security Features

✅ Input validation on all endpoints  
✅ SQL injection protection (EF Core)  
✅ CORS policy configured  
✅ Exception handling  
✅ Data type validation  

---

## 📝 Database Entities

- **User**: User accounts and profile
- **SavingGoal**: Main savings targets
- **SavingTransaction**: Financial activities
- **TravelSaving**: Travel-specific details
- **EventSaving**: Event-specific details
- **GroupSaving**: Group savings container
- **GroupMember**: Group participants
- **Notification**: User notifications

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit pull requests.

---

## 📞 Support

For issues, questions, or suggestions, please create an issue on GitHub.

---

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

---

## 👨‍💻 Author

**Developed by:** The SavingsApp Team  
**Repository:** https://github.com/haiderahmed-96/SavingApp  
**Last Updated:** March 19, 2026  

---

## 🎉 Getting Started

1. **Clone** the repository
2. **Read** COMPLETE_SUMMARY.md for overview
3. **Check** QUICK_REFERENCE.md for commands
4. **Review** API documentation in /swagger
5. **Start** developing!

---

**Status:** 🟢 ACTIVE & PROGRESSING

**Next Phase:** Authentication & JWT Implementation

---

*For detailed information, see the documentation files in the repository.*
