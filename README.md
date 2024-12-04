# Encuestas - ACME
Es una plataforma web que permite a los usuarios crear, gestionar, y consultar resultados de encuestas de manera sencilla y eficiente. La plataforma también incluye funcionalidades de autenticación y autorización para proteger las operaciones de creación, modificación y eliminación.

# Requisitos previos
- .NET 8 SDK
- SQL Server
- Visual Studio o cualquier editor compatible con .NET

# Pasos para instalación y configuración
1. Clonar el repositorio:
   
       git clone https://github.com/JOHANHG21/Test-ACME.git

       cd Test-ACME

3. Configurar la base de datos:
   
       Actualiza el archivo appsettings.json con la cadena de conexión de tu base de datos SQL Server

4. Restaurar dependencias:
   
       dotnet restore

5. Aplicar migraciones y actualizar la base de datos:
   
       dotnet ef database update

6. Ejecutar la aplicación:
   
       dotnet run

7. Abrir el navegador en la URL http://localhost:5000 o la URL proporcionada

