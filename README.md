Este repositorio contiene el código para una aplicación de monitoreo de sensores en tiempo real que permite insertar lecturas de sensores, leer lecturas recientes y detectar sensores anormales.

Características:

Implementación de un sistema de publicación-suscripción para manejar eventos de sensores anormales.
Control de concurrencia para garantizar la integridad de los datos en accesos concurrentes.
Conexión a una base de datos SQL Server para almacenar lecturas de sensores.
Interfaz de usuario en consola para interactuar con la aplicación.
Detección de sensores anormales con base en un valor umbral predefinido.
-Requisitos:

*Tener instalado Microsoft Visual Studio o un IDE compatible con C#.
*Tener acceso a una base de datos SQL Server con las tablas necesarias (ver sección "Configuración").
*Familiaridad con el lenguaje de programación C# y conceptos básicos de bases de datos.
-Configuración:

1. Base de datos SQL Server:
Cree una base de datos llamada "BaseDatosTiempoRea" en SQL Server.
Cree una tabla llamada "Lectura" con las siguientes columnas:
*id (int, identity, primary key)
*sensor_id (int)
*timestamp (datetime)
*valor (decimal(18,2))
2. Cadena de conexión:
Modifique la cadena de conexión en el código (connectionString en Program.cs) para que coincida con la dirección IP o nombre del servidor de su base de datos SQL Server, el nombre de la base de datos y las credenciales de acceso.
-Uso:

Clonee el repositorio o descargue el código.
Abra el proyecto en Visual Studio o su IDE preferido.
Compile y ejecute el proyecto.
Siga las instrucciones en la interfaz de usuario en consola para insertar lecturas de sensores, leer lecturas recientes y ver información sobre sensores anormales.
