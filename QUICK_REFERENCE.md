# 🔖 QUICK REFERENCE GUIDE

---

## ⚡ QUICK COMMANDS

### Git Commands
```bash
# View history
git log --oneline

# Add and commit
git add .
git commit -m "message"

# Push to remote
git push origin main
```

### .NET Commands
```bash
# Build
dotnet build

# Run
dotnet run

# Create migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Restore packages
dotnet restore
```

---

## 🔌 API QUICK TEST

### Create Saving Goal
```
POST http://localhost:5001/api/saving-goals
{
  "userId": 1,
  "goalName": "Summer Vacation",
  "targetAmount": 3000,
  "durationDays": 180,
  "savingType": 0
}
```

### Get Notifications
```
GET http://localhost:5001/api/notifications/user/1
```

### Add Amount
```
POST http://localhost:5001/api/saving-transactions
{
  "savingGoalId": 1,
  "userId": 1,
  "amount": 500,
  "contributionType": 0
}
```

---

## 📊 ENUMS REFERENCE

### NotificationType
- 0 = AmountAdded
- 1 = AmountWithdrawn
- 2 = GoalReached
- 3 = GoalCreated
- 4 = GroupInvitation
- 5 = GroupMemberJoined
- 6 = GroupMemberLeft
- 7 = EventReminder
- 8 = TravelPlanCreated
- 9 = MilestoneReached
- 10 = GoalFailed
- 11 = SystemNotification

### SavingType
- 0 = Personal
- 1 = Shared
- 2 = Emergency

### SavingStatus
- 0 = Active
- 1 = Paused
- 2 = Completed
- 3 = Failed

### NotificationStatus
- 0 = Unread
- 1 = Read
- 2 = Archived
- 3 = Dismissed

---

## 🗂️ FILE LOCATIONS

### Controllers
`SavingsApp/Controllers/`

### Services
`SavingsApp/Services/Implementations/`
`SavingsApp/Services/Interfaces/`

### Models
`SavingsApp/Models/Entities/`
`SavingsApp/Models/Enums/`

### DTOs
`SavingsApp/DTOS/`

### Database
`SavingsApp/Data/AppDbContext.cs`

### Migrations
`SavingsApp/Migrations/`

---

## 🔧 KEY INTERFACES

### INotificationService
- CreateNotificationAsync()
- GetUserNotificationsAsync()
- GetUnreadCountAsync()
- MarkAsReadAsync()
- DeleteNotificationAsync()

### ISavingGoalService
- CreateSavingGoalAsync()
- GetGoalDetailsAsync()
- GetUserGoalsAsync()
- UpdateSavingGoalAsync()

### ISavingTransactionService
- AddTransactionAsync()
- WithdrawAsync()

### IEventSavingService
- CreateEventSavingAsync()
- GetEventSavingAsync()
- UpdateEventSavingAsync()
- DeleteEventSavingAsync()

---

## 🛠️ DEPENDENCY INJECTION

In `Program.cs`:
```csharp
builder.Services.AddScoped<ISavingGoalService, SavingGoalService>();
builder.Services.AddScoped<ISavingTransactionService, SavingTransactionService>();
builder.Services.AddScoped<IEventSavingService, EventSavingService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
```

---

## 📝 ENTITY RELATIONSHIPS

```
User
├── Has Many: SavingGoals
├── Has Many: SavingTransactions
├── Has Many: GroupMembers
└── Has Many: Notifications

SavingGoal
├── FK: User
├── Has Many: SavingTransactions
├── Has One: TravelSaving
├── Has One: EventSaving
└── Has One: GroupSaving

GroupSaving
├── FK: SavingGoal
└── Has Many: GroupMembers

GroupMember
├── FK: GroupSaving
└── FK: User

SavingTransaction
├── FK: SavingGoal
└── FK: User

Notification
└── FK: User
```

---

## ✅ STATUS CODES

| Code | Exception | Usage |
|------|-----------|-------|
| 200 | - | Success |
| 201 | - | Created |
| 400 | BadRequestException | Bad Input |
| 401 | UnauthorizedException | Not Authorized |
| 404 | NotFoundException | Not Found |
| 409 | ConflictException | Conflict |
| 500 | - | Server Error |

---

## 🚀 NEXT STEPS

1. **Implement JWT Authentication**
2. **Add Unit Tests (xUnit)**
3. **Integrate Serilog for Logging**
4. **Add Pagination & Filtering**
5. **Implement Repository Pattern**

---

## 📖 DOCUMENTATION REFERENCE

| File | Purpose |
|------|---------|
| COMPLETE_SUMMARY.md | Full project overview |
| NOTIFICATIONS_GUIDE.md | Notifications system |
| TESTING_GUIDE.md | Testing scenarios |
| USAGE_GUIDE.md | API examples |
| STATISTICS.md | Project metrics |
| PROJECT_SUMMARY.md | Architecture details |

---

**Keep this handy for quick reference! 🎯**
