# 🏥 Medispring – Hospital Appointment System

Medispring is a feature-rich Hospital Appointment Management System built as a RESTful Web API using **ASP.NET Core** and **Entity Framework Core**.  
It automates doctor scheduling, patient appointment booking, approval workflows, reporting, and notifications while following a clean **N-Tier Architecture**.

---

## 🚀 Key Features

### ✅ Core Modules
- Branch Management
- Doctor Management
- Patient Management
- Doctor Scheduling
- Appointment Booking

---

### ⭐ Advanced Functionalities (Beyond CRUD)

- **Automated Appointment Workflow**
  - Auto approval / rejection based on daily capacity
  - Serial-based appointment timing
  - Automatic cancellation handling

- **Doctor Scheduling & Conflict Prevention**
  - Time overlap detection
  - Slot duration validation
  - Schedule update safeguards

- **Email Notifications**
  - Appointment approval
  - Appointment rejection
  - Appointment cancellation

- **Advanced Search & Filtering**
  - Doctors by branch
  - Branches by doctor
  - Patient search
  - Schedule-based filtering

- **Appointment Reports**
  - Daily appointment report
  - Weekly appointment report
  - Status-based summaries

- **Doctor Availability Suggestion**
  - Suggests doctors based on:
    - Branch
    - Day
    - Time
    - Availability & capacity

- **Admin Dashboard Statistics**
  - Total appointments
  - Today’s appointments
  - Approved vs rejected counts
  - Active doctors & patients

- **Authentication & Role-Based Access**
  - JWT-based authentication
  - Role-based authorization (Admin, Doctor, Patient)
  - Secure API endpoints

---

## 🧱 Architecture

The project follows a strict **N-Tier Architecture**:


- **API Layer**: Handles HTTP requests & responses
- **BLL**: Contains all business rules & workflows
- **DAL**: Handles database operations using EF Core

---

## 🛠️ Technology Stack

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- AutoMapper
- JWT Authentication
- SMTP Email Service
- Visual Studio 2025 (Insider)

---

## 🔐 Security

- JWT Authentication
- Role-Based Authorization
- Secure endpoint access control
- Input validation and error handling

---

## 📊 Database Design

- Relational database with proper constraints
- Foreign key relationships
- Enum-based status management
- Optimized queries with `Include()` and projections

---

## 🧪 Error Handling & Validation

- Layered validation (Service-level)
- Controller-level exception handling
- Proper HTTP status codes:
  - 200 OK
  - 400 Bad Request
  - 404 Not Found
  - 500 Internal Server Error

---

## 🎓 Academic Relevance

This project fulfills and exceeds academic requirements by implementing:
- Multiple advanced functionalities beyond CRUD
- Real-world hospital workflow automation
- Clean architecture and separation of concerns
- Professional-grade API design

---


## 👤 Author

**Salman Farshe**  
Department of Computer Science & Engineering  

---

## 📜 License

This project is developed for educational purposes.
