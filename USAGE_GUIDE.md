# 📚 دليل الميزات الجديدة

## 🔍 **كيفية استخدام التحسينات الجديدة**

---

## 1️⃣ **Input Validation (FluentValidation)**

### 📝 مثال على API Request:

```json
POST /api/saving-goals
Content-Type: application/json

{
  "userId": 1,
  "goalName": "Trip to Paris",
  "targetAmount": 5000,
  "durationDays": 365,
  "savingType": 1
}
```

### ✅ سيتم التحقق من:
- ✓ Goal name ليس فارغ وأقل من 100 حرف
- ✓ TargetAmount أكبر من 0
- ✓ DurationDays أكبر من 0
- ✓ UserId صحيح
- ✓ SavingType هو enum صحيح

### ❌ إذا كانت البيانات خاطئة، ستحصل على:
```json
{
  "error": "Goal name is required",
  "status": 400,
  "traceId": "0HN123..."
}
```

---

## 2️⃣ **AutoMapper Usage**

### 🔄 التحويلات المدعومة:

```csharp
// ✅ CreateSavingGoalDto → SavingGoal
var savingGoal = _mapper.Map<SavingGoal>(createGoalDto);

// ✅ SavingGoal → SavingGoalDetailsDto
var goalDetails = _mapper.Map<SavingGoalDetailsDto>(savingGoal);

// ✅ UpdateSavingGoalDto → SavingGoal
var updatedGoal = _mapper.Map<SavingGoal>(updateDto);

// ✅ CreateTravelSavingDto → TravelSaving
var travelSaving = _mapper.Map<TravelSaving>(travelDto);
```

---

## 3️⃣ **Custom Exceptions**

### 📍 الأخطاء المدعومة:

#### **NotFoundException (404)**
```csharp
if (goal == null)
    throw new NotFoundException("Saving goal not found");
```

**Response:**
```json
{
  "error": "Saving goal not found",
  "status": 404,
  "traceId": "0HN123..."
}
```

---

#### **BadRequestException (400)**
```csharp
if (userId <= 0)
    throw new BadRequestException("Valid user ID is required");
```

**Response:**
```json
{
  "error": "Valid user ID is required",
  "status": 400,
  "traceId": "0HN123..."
}
```

---

#### **ConflictException (409)**
```csharp
if (existingGroup != null)
    throw new ConflictException("Group saving already exists for this goal");
```

**Response:**
```json
{
  "error": "Group saving already exists for this goal",
  "status": 409,
  "traceId": "0HN123..."
}
```

---

#### **UnauthorizedException (401)**
```csharp
throw new UnauthorizedException("User is not authorized");
```

**Response:**
```json
{
  "error": "User is not authorized",
  "status": 401,
  "traceId": "0HN123..."
}
```

---

## 4️⃣ **API Examples**

### ✅ **Create Saving Goal**
```bash
POST /api/saving-goals
Content-Type: application/json

{
  "userId": 1,
  "goalName": "Summer Vacation",
  "targetAmount": 3000,
  "durationDays": 180,
  "savingType": 0
}
```

**Response:**
```json
{
  "id": 5,
  "message": "Saving goal created successfully"
}
```

---

### ✅ **Create Travel Saving**
```bash
POST /api/travel-saving
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "country": "France",
  "currencyType": 2,
  "equivalentAmount": 2800
}
```

**Response:**
```json
{
  "id": 3,
  "message": "Travel saving created successfully"
}
```

---

### ✅ **Get Saving Goal Details**
```bash
GET /api/saving-goals/5?userId=1
```

**Response:**
```json
{
  "id": 5,
  "userId": 1,
  "goalName": "Summer Vacation",
  "targetAmount": 3000,
  "currentAmount": 1500,
  "durationDays": 180,
  "savingType": 0,
  "status": 0,
  "createdAt": "2026-03-19T12:00:00"
}
```

---

### ✅ **Add Transaction**
```bash
POST /api/saving-transactions
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "amount": 500,
  "contributionType": 0
}
```

**Response:**
```json
{
  "message": "Amount added successfully"
}
```

---

### ✅ **Withdraw Funds**
```bash
POST /api/saving-transactions/withdraw
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "amount": 200
}
```

**Response:**
```json
{
  "message": "Withdraw completed successfully"
}
```

---

### ✅ **Create Group Saving**
```bash
POST /api/group-saving
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1
}
```

**Response:**
```json
{
  "id": 2,
  "message": "Group saving created successfully"
}
```

---

### ✅ **Add Group Member**
```bash
POST /api/group-saving/5/members
Content-Type: application/json

{
  "userId": 2
}
```

**Response:**
```json
{
  "message": "Member added successfully"
}
```

---

### ✅ **Get Group Details**
```bash
GET /api/group-saving/5
```

**Response:**
```json
{
  "id": 2,
  "savingGoalId": 5,
  "groupMembers": [
    {
      "id": 1,
      "groupSavingId": 2,
      "userId": 1,
      "role": 1
    },
    {
      "id": 2,
      "groupSavingId": 2,
      "userId": 2,
      "role": 0
    }
  ]
}
```

---

## 🔄 **Error Handling Flow**

```
User Request
    ↓
Controller Action
    ↓
Validation (FluentValidation)
    ↓ [if validation fails]
BadRequestException (400)
    ↓
    Service Layer
    ↓
[if business logic fails]
Custom Exception (NotFoundException, ConflictException, etc.)
    ↓
ExceptionMiddleware
    ↓
Convert to JSON Response
    ↓
User Response
```

---

## 🎯 **Validation Rules**

### **SavingGoal:**
- ✓ GoalName: Required, Max 100 chars
- ✓ TargetAmount: > 0
- ✓ DurationDays: > 0
- ✓ UserId: > 0
- ✓ SavingType: Valid Enum

### **TravelSaving:**
- ✓ SavingGoalId: > 0
- ✓ Country: Required, Max 50 chars
- ✓ CurrencyType: Valid Enum
- ✓ EquivalentAmount: > 0

### **EventSaving:**
- ✓ SavingGoalId: > 0
- ✓ EventDate: Must be in future
- ✓ EventType: Valid Enum

### **Transaction:**
- ✓ SavingGoalId: > 0
- ✓ UserId: > 0
- ✓ Amount: > 0
- ✓ ContributionType: Valid Enum

---

## 📋 **HttpStatusCode Mapping**

| Exception Type | Status Code | معنى |
|---|---|---|
| NotFoundException | 404 | البيان غير موجود |
| BadRequestException | 400 | بيانات خاطئة |
| UnauthorizedException | 401 | غير مصرح |
| ConflictException | 409 | تضارب (مثلاً عنصر موجود مسبقاً) |
| Exception (عام) | 500 | خطأ في الخادم |

---

## 🚀 **الخطوات التالية**

1. ✅ **Phase 1**: Validation + AutoMapper + Custom Exceptions ✓
2. 📝 **Phase 2**: Authentication & JWT
3. 📝 **Phase 3**: Unit Tests
4. 📝 **Phase 4**: Logging
5. 📝 **Phase 5**: Pagination & Filtering

---

**استمتع بالتحسينات الجديدة! 🎉**
