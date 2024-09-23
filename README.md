# dasigno-demokrata

Repositorio de código de REST API para prueba técnica en .NET 8 para Dasigno

## Arquitectura (Clean Architecture)
![Dasigno-Architecture](https://github.com/user-attachments/assets/d9c1c964-ca8e-4483-997a-e3cbbf3cd811)

La arquitectura implementada en el desarrollo de la prueba técnica fue Clean Architecture (Arquitectura Limpia). Dentro de los patrones y buenas prácticas implementadas en el desarrollo de la prueba se aplicaron los patrones **Repository** y **Options**, así como buenas prácticas como **Dependency Injection**, **Separation of Concerns**, **Single Responsibility**, estándar de nombres, uso correcto de los códigos de respuesta HTTP y en general los principios **SOLID** para tener un código limpio. También se implementó la captura global de errores por medio de un filtro (**GlobalErrorHandlingAttribute**)

## Estructura de proyectos

- La carpeta **src** contiene el desarrollo de la solución. Se divide en 3 subcarpetas: Core, Infrastructure y Presentation
  - **Core** contiene las entidades (proyecto **Dasigno.Demokrata.Core.Domain**) y la lógica de la aplicación (proyecto **Dasigno.Demokrata.Core.Application**) en donde se definen las interfaces que implementarán las otras capas
  - **Infrastructure** es para el acceso a la base de datos (proyecto **Dasigno.Demokrata.Infrastructure.DataAccess**)
  - **Presentation** es el Web Api (proyecto **Dasigno.Demokrata.Presentation.WebApi**)
- En la carpeta **tests** se encuentra el proyecto de pruebas unitarias. Se hicieron pruebas al controller y a la clase del servicio de usuarios para obtener un 78% de cobertura de código.

## Tecnologias y frameworks utilizados

- **.NET 8**
- **Entity Framework Core** para el acceso a base de datos
- **Swagger** para documentar el Api
- **AutoMapper** para el mapeo automático entre entidades y DTOs
- **FluentValidation** para crear reglas de validación fuertemente tipadas
- **SQL Server** como motor de base de datos
- **NUnit** y **Moq** para los unit tests
- **FluentAssertions** es una conjunto de métodos de extensión que permiten especificar de forma más natural el resultado esperado de las pruebas unitarias
- La solución fue implementada en Visual Studio 2022

## Configuración a la base de datos

La base de datos utilizada es SQL Server. Para configurar la cadena de conexión se debe modificar el parámetro **DemokrataDatabase** en la sección **ConnectionStrings** del archivo **appsettings.json**

## JSON de prueba

**JSON para crear un usuario**

```
{
  "firstName": "PrimerNombre",
  "middleName": "SegundoNombre",
  "lastName": "PrimerApellido",
  "surName": "SegundoApellido",
  "birthDate": "1990-02-12",
  "salary": 1000000
}
```

**JSON para actualizar un usuario**

```
{
  "id": 1,
  "firstName": "PrimerNombre",
  "middleName": "SegundoNombre",
  "lastName": "PrimerApellido",
  "surName": "SegundoApellido",
  "birthDate": "1990-02-12",
  "salary": 1000000
}
```
