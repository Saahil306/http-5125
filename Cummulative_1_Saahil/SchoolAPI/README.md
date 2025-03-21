# SchoolAPI - ASP.NET Core Web API & MVC Project

This project is an **ASP.NET Core Web API and MVC** application that manages teachers in a school database. It allows users to **view, create, update, and delete teachers** through a RESTful API and a user-friendly web interface.

---

## ** Features**
- **Web Pages**:
  - View a list of all teachers.
  - View details of a specific teacher.
- **API Endpoints**:
  - Retrieve all teachers.
  - Retrieve a teacher by ID.
  - Add a new teacher.

---

## ** Technologies Used**
- **ASP.NET Core 8** - Web API & MVC framework
- **Entity Framework Core** - ORM for database access
- **MySQL** (via **Pomelo** provider) - Database storage
- **cURL / Postman** - API testing
- **VS Code** - Development environment

---
## **API Endpoints**

### **Get All Teachers and get by ID **

Teachers List -->	http://localhost:5288/TeacherPage/List
Single Teacher Details -->	http://localhost:5288/TeacherPage/Show/{id}


## **API Endpoints**

### **Get All Teachers and get by ID **
```http
GET http://localhost:5288/api/TeacherAPI
GET http://localhost:5288/api/TeacherAPI/{id}





