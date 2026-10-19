# Campus API

Servicio REST en ASP.NET Core para administrar estudiantes, docentes, cursos,
asignaciones y usuarios de un sistema académico.

## Preparación

1. Copia `appsettings.example.json` como `appsettings.Development.json` y
   configura `ConnectionStrings:DefaultConnection` con tus credenciales.
2. Aplica las migraciones con `dotnet ef database update`.
3. Inicia el servicio con `dotnet run`.

La configuración local usa `http://localhost:5136` y
`https://localhost:7200`. El archivo `CampusApi.http` contiene solicitudes de
prueba listas para ejecutar.

## Recursos disponibles

| Recurso | Ruta base | Operaciones |
| --- | --- | --- |
| Estudiantes | `/api/estudiantes` | GET, POST, PUT, DELETE |
| Docentes | `/api/docentes` | GET, POST, PUT, DELETE |
| Cursos | `/api/cursos` | GET, POST, PUT, DELETE |
| Asignaciones | `/api/asignaciones` | GET, POST, PUT, DELETE |
| Usuarios | `/api/usuarios` | GET, POST, PUT, DELETE |

Usuarios también expone `POST /api/usuarios/login` y
`PUT /api/usuarios/{id}/password`.

## Organización

- `Controllers`: endpoints HTTP.
- `Data`: contexto y configuración de Entity Framework Core.
- `Domain`: entidades persistidas.
- `Dtos`: contratos de entrada y salida.
- `Services`: utilidades de seguridad.
- `Migrations`: historial del esquema de SQL Server.

Las rutas públicas y la estructura de la base de datos se mantienen estables
para conservar compatibilidad con los clientes existentes.
