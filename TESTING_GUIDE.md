# 🧪 **Testing Guide - Notifications System**

## 🎯 **كيفية اختبار نظام الإشعارات**

---

## 1️⃣ **إعداد البيانات الأساسية**

### **Step 1: إنشاء مستخدم**

قاعدة البيانات يجب أن تحتوي على مستخدم واحد على الأقل:

```sql
INSERT INTO Users (FullName, PhoneNumber, IsActive, CreatedAt)
VALUES ('Ahmed Ali', '+1234567890', 1, GETUTCDATE());
```

**أو عبر API:**
```
POST /api/users
{
  "fullName": "Ahmed Ali",
  "phoneNumber": "+1234567890",
  "isActive": true
}
```

---

## 2️⃣ **اختبار الإشعارات - سيناريوهات مختلفة**

### **سيناريو 1: إنشاء هدف توفير (Goal Created)**

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

**الإشعار الذي سينشأ:**
```json
{
  "title": "🎯 New Saving Goal Created",
  "message": "Your new goal 'Summer Vacation' has been created successfully!",
  "type": 3,
  "relatedEntityId": 5,
  "relatedEntityType": "SavingGoal"
}
```

---

### **سيناريو 2: إضافة أموال (Amount Added)**

```bash
POST /api/saving-transactions
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "amount": 1000,
  "contributionType": 0
}
```

**الإشعارات التي ستنشأ:**

✅ **Notification 1:**
```json
{
  "title": "💰 Amount Added",
  "message": "$1000 has been added to 'Summer Vacation'",
  "type": 0
}
```

---

### **سيناريو 3: تحقيق الهدف (Goal Reached)**

```bash
POST /api/saving-transactions
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "amount": 2000,
  "contributionType": 0
}
```

**الإشعارات التي ستنشأ:**

✅ **Notification 1:**
```json
{
  "title": "💰 Amount Added",
  "message": "$2000 has been added to 'Summer Vacation'",
  "type": 0
}
```

✅ **Notification 2:**
```json
{
  "title": "🎉 Goal Reached!",
  "message": "Congratulations! You've reached your 'Summer Vacation' goal!",
  "type": 2
}
```

---

### **سيناريو 4: سحب أموال (Amount Withdrawn)**

```bash
POST /api/saving-transactions/withdraw
Content-Type: application/json

{
  "savingGoalId": 5,
  "userId": 1,
  "amount": 500
}
```

**الإشعار الذي سينشأ:**
```json
{
  "title": "💸 Amount Withdrawn",
  "message": "$500 has been withdrawn from 'Summer Vacation'",
  "type": 1
}
```

---

## 3️⃣ **التحقق من الإشعارات**

### **الحصول على جميع الإشعارات**

```bash
GET /api/notifications/user/1
```

**Response:**
```json
[
  {
    "id": 3,
    "userId": 1,
    "title": "🎉 Goal Reached!",
    "message": "Congratulations! You've reached your 'Summer Vacation' goal!",
    "type": 2,
    "status": 0,
    "relatedEntityId": 5,
    "relatedEntityType": "SavingGoal",
    "createdAt": "2026-03-19T19:21:31.1234567",
    "readAt": null
  },
  {
    "id": 2,
    "userId": 1,
    "title": "💰 Amount Added",
    "message": "$2000 has been added to 'Summer Vacation'",
    "type": 0,
    "status": 0,
    "relatedEntityId": 5,
    "relatedEntityType": "SavingGoal",
    "createdAt": "2026-03-19T19:21:30.1234567",
    "readAt": null
  },
  {
    "id": 1,
    "userId": 1,
    "title": "🎯 New Saving Goal Created",
    "message": "Your new goal 'Summer Vacation' has been created successfully!",
    "type": 3,
    "status": 0,
    "relatedEntityId": 5,
    "relatedEntityType": "SavingGoal",
    "createdAt": "2026-03-19T19:21:29.1234567",
    "readAt": null
  }
]
```

---

### **عدد الإشعارات غير المقروءة**

```bash
GET /api/notifications/user/1/unread-count
```

**Response:**
```json
{
  "unreadCount": 3
}
```

---

## 4️⃣ **تعليم الإشعارات كمقروءة**

### **تعليم إشعار واحد كمقروء**

```bash
PUT /api/notifications/3/mark-as-read
```

**Response:**
```json
{
  "message": "Notification marked as read"
}
```

---

### **تعليم جميع الإشعارات كمقروءة**

```bash
PUT /api/notifications/user/1/mark-all-as-read
```

**Response:**
```json
{
  "message": "All notifications marked as read"
}
```

---

## 5️⃣ **تصفية الإشعارات**

### **الحصول على إشعارات حسب النوع**

```bash
GET /api/notifications/user/1/type/0
```

**المعاملات:**
- `0` = AmountAdded
- `1` = AmountWithdrawn
- `2` = GoalReached
- `3` = GoalCreated
- `4` = GroupInvitation
- `5` = GroupMemberJoined
- `6` = GroupMemberLeft
- `7` = EventReminder
- `8` = TravelPlanCreated
- `9` = MilestoneReached
- `10` = GoalFailed
- `11` = SystemNotification

---

## 6️⃣ **تحديث حالة الإشعار**

```bash
PUT /api/notifications/3/status
Content-Type: application/json

{
  "status": 2
}
```

**الحالات:**
- `0` = Unread
- `1` = Read
- `2` = Archived
- `3` = Dismissed

---

## 7️⃣ **حذف الإشعارات**

### **حذف إشعار واحد**

```bash
DELETE /api/notifications/3
```

**Response:**
```json
{
  "message": "Notification deleted successfully"
}
```

---

## 📋 **Postman Collection**

### **Environment Variables:**
```
baseUrl: http://localhost:5000
userId: 1
notificationId: 1
```

### **Requests:**

```
1. GET {{baseUrl}}/api/notifications/user/{{userId}}
2. GET {{baseUrl}}/api/notifications/user/{{userId}}/unread-count
3. GET {{baseUrl}}/api/notifications/{{notificationId}}
4. PUT {{baseUrl}}/api/notifications/{{notificationId}}/mark-as-read
5. PUT {{baseUrl}}/api/notifications/user/{{userId}}/mark-all-as-read
6. PUT {{baseUrl}}/api/notifications/{{notificationId}}/status
7. DELETE {{baseUrl}}/api/notifications/{{notificationId}}
8. GET {{baseUrl}}/api/notifications/user/{{userId}}/type/0
```

---

## 🧪 **اختبار التكامل**

### **Test Case 1: Create Goal → Check Notification**

```
1. POST /api/saving-goals
   ✓ يجب أن يعيد ID للهدف

2. GET /api/notifications/user/1/unread-count
   ✓ يجب أن يعيد 1

3. GET /api/notifications/user/1
   ✓ يجب أن نرى إشعار "Goal Created"
```

---

### **Test Case 2: Add Amount → Check Notifications**

```
1. POST /api/saving-transactions
   (add 3000 to goal with target 3000)
   ✓ يجب أن ينجح

2. GET /api/notifications/user/1/unread-count
   ✓ يجب أن يعيد 2 (AmountAdded + GoalReached)

3. GET /api/notifications/user/1
   ✓ يجب أن نرى إشعارين جديدين
```

---

### **Test Case 3: Mark All as Read**

```
1. PUT /api/notifications/user/1/mark-all-as-read
   ✓ يجب أن ينجح

2. GET /api/notifications/user/1/unread-count
   ✓ يجب أن يعيد 0

3. GET /api/notifications/user/1
   ✓ جميع الإشعارات status = Read
```

---

## 💾 **Database Checks**

### **View All Notifications:**
```sql
SELECT * FROM Notifications
ORDER BY CreatedAt DESC;
```

### **Count by Type:**
```sql
SELECT Type, COUNT(*) as Count
FROM Notifications
GROUP BY Type;
```

### **Count by Status:**
```sql
SELECT Status, COUNT(*) as Count
FROM Notifications
GROUP BY Status;
```

### **Unread Count per User:**
```sql
SELECT UserId, COUNT(*) as UnreadCount
FROM Notifications
WHERE Status = 0
GROUP BY UserId;
```

---

## ✅ **Checklist**

- [ ] قاعدة البيانات محدثة مع Migration
- [ ] المستخدم موجود في قاعدة البيانات
- [ ] التطبيق يعمل بدون أخطاء
- [ ] الإشعارات تُنشأ عند الأحداث
- [ ] يمكن الحصول على الإشعارات عبر API
- [ ] تعليم كمقروء يعمل
- [ ] الحذف يعمل
- [ ] التصفية حسب النوع تعمل

---

**مبروك! نظام الإشعارات جاهز للاختبار! 🎉**
