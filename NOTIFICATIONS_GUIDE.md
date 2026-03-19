# 📬 **نظام الإشعارات (Notifications System)**

## ✨ **ما تم إضافته:**

### 1️⃣ **Notification Model**
```csharp
- Id
- UserId (FK)
- Title
- Message
- Type (NotificationType Enum)
- Status (NotificationStatus Enum)
- RelatedEntityId
- RelatedEntityType
- CreatedAt
- ReadAt
```

### 2️⃣ **Notification Types**
- ✅ **AmountAdded** - عند إضافة أموال
- ✅ **AmountWithdrawn** - عند سحب أموال
- ✅ **GoalReached** - عند تحقيق الهدف
- ✅ **GoalCreated** - عند إنشاء هدف
- ✅ **GroupInvitation** - عند دعوة للانضمام لمجموعة
- ✅ **GroupMemberJoined** - عند انضمام عضو
- ✅ **GroupMemberLeft** - عند مغادرة عضو
- ✅ **EventReminder** - تذكير الحدث
- ✅ **TravelPlanCreated** - إنشاء خطة سفر
- ✅ **MilestoneReached** - نقطة مهمة
- ✅ **GoalFailed** - فشل الهدف
- ✅ **SystemNotification** - إشعارات النظام

### 3️⃣ **Notification Status**
- ✅ **Unread** - لم تُقرأ
- ✅ **Read** - تم قراءتها
- ✅ **Archived** - محفوظة
- ✅ **Dismissed** - مرفوضة

---

## 🔧 **الخدمات المضافة:**

### **INotificationService Interface:**
```csharp
✅ CreateNotificationAsync()
✅ GetUserNotificationsAsync()
✅ GetUnreadCountAsync()
✅ GetNotificationByIdAsync()
✅ MarkAsReadAsync()
✅ MarkAllAsReadAsync()
✅ DeleteNotificationAsync()
✅ UpdateNotificationStatusAsync()
✅ GetNotificationsByTypeAsync()
```

### **NotificationService Implementation:**
- ✅ تنفيذ كامل للخدمة
- ✅ معالجة الأخطاء
- ✅ عمليات CRUD كاملة

---

## 📡 **API Endpoints:**

### **POST** `/api/notifications`
إنشاء إشعار جديد
```json
{
  "userId": 1,
  "title": "💰 Amount Added",
  "message": "$500 added to savings",
  "type": 0,
  "relatedEntityId": 5,
  "relatedEntityType": "SavingGoal"
}
```

### **GET** `/api/notifications/user/{userId}`
الحصول على جميع الإشعارات
```
GET /api/notifications/user/1
```

### **GET** `/api/notifications/user/{userId}/unread-count`
عدد الإشعارات غير المقروءة
```
GET /api/notifications/user/1/unread-count

Response: { "unreadCount": 3 }
```

### **GET** `/api/notifications/{id}`
الحصول على إشعار محدد
```
GET /api/notifications/5
```

### **PUT** `/api/notifications/{id}/mark-as-read`
تعليم الإشعار كمقروء
```
PUT /api/notifications/5/mark-as-read
```

### **PUT** `/api/notifications/user/{userId}/mark-all-as-read`
تعليم جميع الإشعارات كمقروءة
```
PUT /api/notifications/user/1/mark-all-as-read
```

### **PUT** `/api/notifications/{id}/status`
تحديث حالة الإشعار
```json
PUT /api/notifications/5/status

{
  "status": 1
}
```

### **DELETE** `/api/notifications/{id}`
حذف إشعار
```
DELETE /api/notifications/5
```

### **GET** `/api/notifications/user/{userId}/type/{type}`
الحصول على إشعارات حسب النوع
```
GET /api/notifications/user/1/type/0
```

---

## 🔄 **تكامل الإشعارات مع الخدمات:**

### **1. عند إضافة أموال:**
```
✅ إشعار "Amount Added"
✅ إشعار "Goal Reached" (إذا تحقق الهدف)
```

### **2. عند سحب أموال:**
```
✅ إشعار "Amount Withdrawn"
```

### **3. عند إنشاء هدف:**
```
✅ إشعار "Goal Created"
```

### **4. عند مجموعة:**
```
✅ إشعار "Group Invitation"
✅ إشعار "GroupMemberJoined"
```

---

## 📁 **الملفات المضافة:**

```
✅ Models/Enums/
   ├── NotificationType.cs
   └── NotificationStatus.cs

✅ Models/Entities/
   └── Notification.cs

✅ DTOS/
   ├── NotificationDto.cs
   ├── CreateNotificationDto.cs
   └── UpdateNotificationStatusDto.cs

✅ Services/Interfaces/
   └── INotificationService.cs

✅ Services/Implementations/
   └── NotificationService.cs

✅ Validators/
   └── CreateNotificationValidator.cs

✅ Controllers/
   └── NotificationController.cs

✅ Migrations/
   └── AddNotifications.cs
```

---

## 🔗 **الملفات المعدّلة:**

```
🔄 Program.cs - تسجيل الخدمة
🔄 Models/Entities/User.cs - إضافة العلاقة
🔄 Data/AppDbContext.cs - إضافة DbSet والعلاقات
🔄 Services/Implementations/SavingTransactionService.cs - إضافة الإشعارات
🔄 Services/Implementations/SavingGoalService.cs - إضافة الإشعارات
🔄 Mappings/MappingProfile.cs - إضافة الـ mappings
```

---

## 💡 **الميزات:**

✅ **Automatic Notifications**: تُنشأ الإشعارات تلقائياً عند الأحداث  
✅ **Status Tracking**: تتبع حالة الإشعار (مقروء/غير مقروء)  
✅ **Related Entity Links**: ربط الإشعار بالعنصر ذي الصلة  
✅ **Filtering**: تصفية حسب النوع والحالة  
✅ **Read Status**: تتبع وقت القراءة  

---

## 🚀 **مثال الاستخدام:**

### **سيناريو: المستخدم يضيف $500**

```
1. POST /api/saving-transactions
   {
     "savingGoalId": 5,
     "userId": 1,
     "amount": 500,
     "contributionType": 0
   }

2. النظام ينشئ تلقائياً:
   ✅ إشعار "Amount Added"
   ✅ إشعار "Goal Reached" (إذا تحقق الهدف)

3. المستخدم يتحقق من الإشعارات:
   GET /api/notifications/user/1

4. يحصل على قائمة الإشعارات مع التفاصيل
```

---

## 🎯 **الخطوات القادمة:**

- [ ] Real-time notifications with SignalR
- [ ] Email notifications
- [ ] Push notifications
- [ ] Notification preferences
- [ ] Bulk operations
- [ ] Advanced filtering

---

**تم تطبيق نظام الإشعارات بنجاح! 📬✨**
