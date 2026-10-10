# ToDo list CLI

Proyecto creado como uno de los retos de Roadmap.sh

Aplicación de consola diseñada con .NET 10 para resolver el reto de roadmap.sh ["Expense Tracker"](https://roadmap.sh/projects/expense-tracker).

## Características  

* Agregar gasto.
* Listar los gastos.
* Filtrar la lista de gastos por mes.
* Actualizar gasto.
    * Description.
    * Cantidad.
* Eliminar gasto.
* Mostrar total de gastos.
* Mostrar total de gastos por mes.
* Opciones de comando.
* Persistencia de gastos mediante un archivo .json

## Instalación

1. Clona el repositorio
```bash
git clone https://github.com/juanharodev/juanharodev.github.io.git
```
<hr>

2. Abre la una terminal y navega hasta la carpeta del proyecto
```bash
cd /portfolio/roadmap-sh/backend/03-expense-tracker
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

5.  Corre la aplicación usando comando y las opciones para especificar el proceso a realizar.
```bash
dotnet run "command" "options"
```

## ¿Cómo se usa? 

Una vez instalado usa el comando *dotnet run* junto a los argumentos deseados para iniciar el funcionamiento. 

### Argumentos
* *add*:  Agrega un nuevo gasto.
    * *"--description" (Requerido)* Especifica la description del gasto.
    * *"--amount"* Especifica la cantidad de dinero gastada.
* *list*: Lista todos los gastos.
    * *"--month"* Lista los gastos del mes especificado (1-12).
* *"update"* Actualiza los datos del gasto especificado.
    * *"--id" (Requerido)* Especifica el id del gasto a modificar.
    * *"--description"* Especifica la nueva description del gasto.
    * *"--amount"* Especifica la  nueva cantidad de dinero gastada.
* *"delete"* Elimina un gasto especificado.
    * *"--id" (Requerido)* Especifica el id del gasto a modificar.
* *summary* Muestra el total de gastos.
    * *"--month"* Lista los gastos del mes especificado (1-12).
* *"help"* Muestra ayuda del comando especificado.

### Ejemplo de uso
```bash
dotnet run add --description "Lunch" --amount 5 
dotnet run add --description "Phone bill"
dotnet run update --id 2 --amount 12.99
dotnet run list
dotnet run list --month 10
dotnet run delete --id 1
dotnet run summary
dotnet run summary --month 8
```

