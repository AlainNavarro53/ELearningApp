#ELearningApp
ELearningApp es una aplicación ASP.NET Core MVC para gestionar cursos de e-learning. Permite a los administradores (profesores) crear, editar y eliminar cursos, y a los estudiantes ver la lista de cursos disponibles.

Requisitos Previos
.NET 6 SDK o superior
SQL Server
Visual Studio 2022 o Visual Studio Code
Instalación
Clonar el Repositorio

git clone https://github.com/tu_usuario/ELearningApp.git
cd ELearningApp
Configurar la Cadena de Conexión

Abre appsettings.json y configura la cadena de conexión para tu instancia de SQL Server: { "ConnectionStrings": { "ELearningConnection": "Server=localhost;Database=ELearningDb;Trusted_Connection=True;" }, ... }

Restaurar las Dependencias
dotnet restore

Aplicar las Migraciones y Crear la Base de Datos
dotnet ef migrations add InitialCreate dotnet ef database update

Ejecutar la Aplicación
dotnet run

Abrir en el Navegador
La aplicación debería estar corriendo en https://localhost:7105 o http://localhost:5287

Dependencias

Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Tools
jQuery
jQuery.Validation
jQuery.Validation.Unobtrusive
