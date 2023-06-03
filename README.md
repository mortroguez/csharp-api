<p align="center">
  <a href="" rel="noopener">
 <img width=200px height=200px src="https://utech.academy/wp-content/uploads/2019/03/NET_core.png" alt="Project logo"></a>
</p>

<h3 align="center">.NET Core API Rest</h3>

<div align="center">

[![Estado](https://img.shields.io/badge/status-active-success.svg)]()
[![Licencia](https://img.shields.io/badge/license-MIT-blue.svg)](/LICENSE)

</div>

---

<p align="center"> Este es un proyecto de ejemplo de un API Rest en C#
    <br> 
</p>

---

## 📝 Table of Contents

- [Acerca](#acerca)
- [Primeros pasos](#primeros_pasos)
- [Documentación](Documents/MDFiles/Documentation.md) **!IMPORTANTE**
- [Despliegue](#despliegue)
- [Tecnologías](#tecnologias)
- [Autores](#autores)
- [Logros](#logros)

---

## 🧐 Acerca <a name = "acerca"></a>

En este proyecto, adquirirás conocimientos sobre cómo crear un API Rest en C# utilizando el patrón de inversión de dependencias. Aprenderás a implementar las operaciones de CRUD (crear, leer, actualizar y eliminar) en una base de datos, y también explorarás cómo desplegar el proyecto utilizando Docker.

Además, te familiarizarás con las mejores prácticas de desarrollo y diseño para crear APIs robustas y escalables. Al finalizar el proyecto, estarás capacitado para construir tus propias APIs Restful en C# utilizando el patrón de inversión de dependencias, interactuar con bases de datos y utilizar Docker para el despliegue eficiente de tu aplicación.

---

## 🏁 Primeros pasos <a name = "primeros_pasos"></a>

Antes de comenzar con el proyecto, es importante seguir una serie de pasos necesarios para poder trabajar de manera eficiente. A continuación, se presentan los pasos recomendados:

### Prerequisitos

- [.NET Core SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- [VS Code](https://code.visualstudio.com/)
- [EF Core Tool](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) - Seguir las instrucciones
- [SQL Server Database Engine](https://www.microsoft.com/es-es/sql-server/sql-server-downloads)
- [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
- [Docker](https://www.docker.com/products/docker-desktop/) - Opcional

### Configuración

Una vez que hayas instalado las dependencias necesarias, puedes proceder a ejecutar los siguientes pasos para avanzar en tu proyecto:

1. Crear un nuevo proyecto: El comando para crear un nuevo proyecto en .NET Core puede variar ligeramente según la versión que estés utilizando. Aquí tienes algunos ejemplos de comandos para crear un proyecto en diferentes tecnologías:

Para crear un proyecto web API en .NET Core:

```
dotnet new webapi -n NombreDelProyecto
```

Esto creará un nuevo proyecto web API con la estructura básica en la carpeta NombreDelProyecto. Además se crearan algunos archivos con el nombre WeatherForecast, los cuales no nos servirán de mucho, por lo que podrás eliminarlos.

2. Instalar las dependencias necesarias para utilizar Entity Framework:

[Entity Framework](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore)

```
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.5
```

[Entity Framework Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/7.0.5)

```
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.5
```

[Entity Framework SQL Server](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/7.0.5)

```
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 7.0.5
```

[Entity Framework Tools](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/7.0.5)

```
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.5
```

3. Instalar las dependencia que permite implementar los Json Web Token

[Swagger JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer/7.0.5)

```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.5
```

Recuerda que después de instalar las librerias deberás ejecutar el comando:

```
dotnet restore
```

4. Mapear la base de datos: **Entity Framework** cuenta con una asombrosa característica que permite mapear una base de datos, lo que permite interactuar con ella de forma sencilla además de crear las clases homólogas a las tablas de SQL de la base de datos.

Para ello se debe de utilizar el siguiente script

```
dotnet ef DbContext Scaffold "Data Source= localhost; Initial Catalog=AdventureWorks2019; user=sa;Password=123; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer  -o DatabaseModels -c "DatabaseContext" -f
```

Mediante el script anterior se puede mapear la base de datos en nuestro proyecto, sin embargo, también es posible mapear solo tablas específicas

```
dotnet ef DbContext Scaffold "Data Source= localhost; Initial Catalog=AdventureWorks2019; user=sa;Password=123; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer  -o DatabaseModels -c "DatabaseContext" --table NOMBRE_TABLA1 --table NOMBRE_TABLA2 --table NOMBRE_TABLA3 -f
```

Una vez ejecutado el Script se generará una carpeta llamada **DatabaseModels** con varias clases (es una clase por cada tabla de SQL Server que tengas en tu base de datos) y una llamada DatabaseContext, la cuál nos permite administrar las entidades. Cuando esa clase se genera, tendrá un función llamada **OnConfiguring**, la cuál deberás de eliminar debido a que interferirá con lo que queremos hacer.

```
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source= localhost; Initial Catalog=AdventureWorks2019; user=sa;Password=123; TrustServerCertificate=True;");

```

Ahora, en el archivo llamado [appsettings.json](./appsettings.json) y ahí vas a agregar la sección **ConnectionStrings** en donde guardaremos nuestro string de conexión:

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "Connection": "Data Source=localhost;user id=sa;password=123;Initial Catalog=AdventureWorks2019;Connection Timeout=45; TrustServerCertificate=True;"
  },
  "JwtConfig": {
    "Secret": "TuSecretodeJWT",
    "ExpirationDays": 6
  }
}
```

Por último, en el archivo [Program.cs](./Program.cs) deberás agregar la siguiente línea justo debajo del comentario:

```
// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(
    (options) =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
    }
);
```

---

## 🚀 Despliegue <a name = "despliegue"></a>

Add additional notes about how to deploy this on a live system.

## ⛏️ Tecnologías <a name = "tecnologias"></a>

- [SQL Server](https://www.microsoft.com/es-es/sql-server/sql-server-downloads) - Base de datos
- [.NET Core](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) - Tecnología backend
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/) - ORM

## ✍️ Autores <a name = "autores"></a>

- [@MortRoguez](https://github.com/mortroguez) - Idea & Initial work

## 🎉 Logros <a name = "logros"></a>

- Aún no hemos llegado a muchas personas, pero pronto habrán muchos logros.
