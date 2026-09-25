public class Program
{
    public static void Main(string[] args)
    {
        ToDo toDo = new ToDo("ToDo.json");
        Console.WriteLine("Show To do list");
        toDo.ListToDo();
        Console.ReadKey(false);
        Console.Clear();
        
        toDo.AddToDo(new ToDoEntry("New entry",0));
        toDo.ListToDo();
        Console.ReadKey(false);
        Console.Clear();

        toDo.Remove(6);
        toDo.ListToDo();
        Console.ReadKey(false);
        Console.Clear();

        toDo.UpdateToDoContent(1, "Updated task");
        toDo.ListToDo();
        Console.ReadKey(false);
        Console.Clear();

        toDo.UpdateToDoStatus(1, 2);
        toDo.ListToDo();
        Console.ReadKey(false);
        Console.Clear();
    }
}