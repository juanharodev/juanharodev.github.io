# ToDo list CLI

Proyecto creado como uno de los retos de Roadmap.sh

Aplicación de consola diseñada con .NET 10 para resolver el reto de roadmap.sh ["GitHub User Activity"](https://roadmap.sh/projects/github-user-activity).

## Características  

* Obtención de datos de la API de GitHub.
* Manejo de errores.
    * Usuario no proporcionado.
    * Fallos en la petición a la API.
    * Formato incorrecto de la respuesta.
    * Usuario sin actividad reciente.
* Manejo de la respuesta y muestra de eventos.
    * Evento *"Push"* con rama y repositorio.
    * Evento *"Issues"* con tipo, description y repositorio.
    * Evento *"Watch"* en repositorio.
    * Evento *"Fork"* con repositorio bifurcado y su destino.
    * Evento *"Create"* con tipo, nombre y repositorio.
    * Gestión de eventos no registrados con tipo y repositorio.  

## Instalación

1. Clona el repositorio
```bash
git clone https://github.com/juanharodev/juanharodev.github.io.git
```
<hr>

2. Abre la una terminal y navega hasta la carpeta del proyecto
```bash
cd /portfolio/roadmap-sh/backend/02-github-user-activity
```
<hr>

3. Restaura las dependencias del proyecto
```bash
dotnet restore
```
<hr>

4. Compila el proyecto
```bash
dotnet build
```
<hr>

5.  Corre la aplicación usando usando el nombre de usuario a buscar
```bash
dotnet run "usuario"
```

## ¿Cómo se usa? 

Una vez instalado usa el comando *dotnet run* junto al *nombre de usuario* para iniciar el funcionamiento.

### Ejemplo de uso
```bash
dotnet run juanharodev
```

