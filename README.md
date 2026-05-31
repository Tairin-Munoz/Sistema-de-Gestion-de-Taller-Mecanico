# 🚗 Sistema de Gestión de Taller Mecánico

API REST desarrollada en **ASP.NET Core** para la gestión de propietarios, vehículos, servicios, técnicos y órdenes de trabajo dentro de un taller mecánico.

El sistema implementa **arquitectura en capas**, **autenticación JWT** y un enfoque híbrido de acceso a datos utilizando **Entity Framework Core** y **Dapper**.

---

## 📌 Características Principales

✅ Gestión de propietarios
✅ Gestión de vehículos
✅ Gestión de servicios
✅ Gestión de técnicos
✅ Gestión de órdenes de trabajo
✅ Historial integrado del taller
✅ Autenticación JWT
✅ API documentada con Swagger
✅ Publicación en Azure
✅ Validaciones con FluentValidation
✅ Patrón Repository + UnitOfWork
✅ Arquitectura en capas

---

## Arquitectura del Proyecto

El sistema está organizado bajo una arquitectura en capas:

```text
API
│
├── Controllers
│
Services
│
├── Lógica de negocio
├── Validaciones
├── DTOs
│
Infrastructure
│
├── Entity Framework Core
├── Dapper
├── Repository
├── UnitOfWork
│
Core
│
├── Entidades
├── Interfaces
```

Esta estructura permite:

* Separación de responsabilidades
* Mejor mantenimiento
* Escalabilidad
* Código desacoplado

---

##  Patrones Implementados

### Repository Pattern

Permite abstraer el acceso a datos y reutilizar lógica mediante un repositorio genérico.

### Unit Of Work

Gestiona transacciones y coordina operaciones entre múltiples repositorios.

### Arquitectura en Capas

Organiza el sistema separando API, Services, Infrastructure y Core.

---

##  Tecnologías Utilizadas

* ASP.NET Core Web API
* C#
* Entity Framework Core
* Dapper
* Azure MySQL
* JWT Authentication
* Swagger / OpenAPI
* FluentValidation
* AutoMapper
* Azure App Service
* Git y GitHub
* Trello
* Postman

---

##  Autenticación JWT

La API utiliza autenticación JWT para proteger endpoints.

### Login

**POST**

```http
/api/Auth/login
```

Ejemplo:

```json
{
  "username": "ceo1",
  "password": "123456"
}
```

El sistema retorna un token JWT que debe enviarse mediante:

```text
Bearer TOKEN
```

---

##  Endpoints Principales

### Auth

* POST /Auth/register
* POST /Auth/login

### Propietarios

* GET /Propietarios
* POST /Propietarios
* GET /Propietarios/{id}
* PUT /Propietarios/{id}
* DELETE /Propietarios/{id}

### Vehículos

* GET /Vehiculos
* POST /Vehiculos
* GET /Vehiculos/{id}
* PUT /Vehiculos/{id}
* DELETE /Vehiculos/{id}

### Servicios

* GET /Servicios
* POST /Servicios
* GET /Servicios/{id}
* PUT /Servicios/{id}
* DELETE /Servicios/{id}

### Técnicos

* GET /Tecnicos
* POST /Tecnicos
* GET /Tecnicos/{id}
* PUT /Tecnicos/{id}
* DELETE /Tecnicos/{id}

### Órdenes de Trabajo

* GET /OrdenesTrabajo
* POST /OrdenesTrabajo
* GET /OrdenesTrabajo/{id}
* PUT /OrdenesTrabajo/{id}
* DELETE /OrdenesTrabajo/{id}

### Historial

* GET /Historial

---

##  Despliegue en Azure

La API fue desplegada en **Azure App Service**.

### Swagger

https://taller-mecanico-tairin-api.azurewebsites.net/swagger

### API

https://taller-mecanico-tairin-api.azurewebsites.net

---

##  Pruebas

Las pruebas del sistema fueron realizadas mediante:

* Swagger
* Postman

Se validaron:

* CRUD completo
* Autenticación JWT
* Manejo de errores
* Respuestas HTTP
* Historial del taller
* Endpoints protegidos

---

##  Gestión del Proyecto

La planificación y seguimiento del desarrollo se realizó utilizando **Scrum** y **Trello**.

Incluyendo:

* Product Backlog
* Sprint Backlog
* En progreso
* Terminado

---
