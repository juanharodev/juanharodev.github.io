# ToDo list CLI

Proyecto creado como uno de los retos de Roadmap.sh

Aplicación de consola diseñada con .NET 10 para resolver el reto de roadmap.sh ["Task Tracker CLI"](https://roadmap.sh/projects/task-tracker).

## Características  

* Agregar tareas.
* Actualizar el contenido de la tarea.
* Actualizar el estado de la tarea.
* Listar todas las tareas.
* Listar las tareas con un estado especifico.
* Remover tareas.
* Persistencia de tareas mediante un archivo .json . 

## Instalación

1. Clona el repositorio
```bash
git clone https://github.com/juanharodev/juanharodev.github.io.git
```
<hr>

2. Abre la una terminal y navega hasta la carpeta del proyecto
```bash
cd /portfolio/roadmap-sh/backend/01-todo-cli
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

5.  Restaura las dependencias del proyecto
```bash
dotnet restore
```

<hr>

6.  Corre la aplicación usando argumentos para indicar el proceso a realizar
```bash
dotnet run "args"
```

## ¿Cómo se usa? 

Una vez instalado usa el comando *dotnet run* junto a los argumentos deseados para iniciar el funcionamiento. 

### Argumentos
* *add*: Agrega una tarea nueva con el contenido especificado.
* *list*: Muestra todas las tareas creadas.
* *list "filtro"*: Muestra todas las tareas que correspondan con el filtro proporcionado.
* *update-status "id" "new-status"*: Actualiza la tarea indicada por el id, con el nuevo estado.
* *update-content "id" "new-content"*: Actualiza la tarea indicada por el id, con el nuevo contenido.
* *remove "id"*: Remueve la tarea con el id especificado.
* *help*: Muestra los comandos disponibles.

Los estados disponibles son: 
* "To Do": 0, todo
* "In progress": 1, in-progress
* "Done": 2, done

### Ejemplo de uso
```bash
dotnet run add Example task

dotnet run list

dotnet run list 1

dotnet run list in-progress

dotnet run update-status 1 2

dotnet run update-status 1 done

dotnet run update-content 1 Completed task

dotnet run delete 1
```

