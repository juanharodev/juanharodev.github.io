
using System.Text.Json;

public class ToDo
{
    public ToDo(string filePath)
    {
        FilePath = filePath;
        Load();
    }

    public string FilePath {get; set;}
    List<ToDoEntry> entries = new List<ToDoEntry>();
    void Load()
    {
        string json = File.ReadAllText(FilePath);

        entries = JsonSerializer.Deserialize<List<ToDoEntry>>(json)!;
    }

    void Save()
    {   
        var options = new JsonSerializerOptions{WriteIndented = true};
        string json = JsonSerializer.Serialize(entries, options);
        File.WriteAllText(FilePath,json);
    }

    public void AddToDo(ToDoEntry toDoEntry)
    {
        entries.Add(toDoEntry);
        Save();
        Console.WriteLine($"\"{toDoEntry.Content}\" was added");
    }

    public void ListToDo()
    {
        PrintToDo(entries);
    }

    public void ListToDo(int status)
    {
        List<ToDoEntry> _entries = entries.Where(entry => entry.Status == status).ToList();  
        PrintToDo(_entries);
    }

    static void PrintToDo(List<ToDoEntry> _entries)
    {
        Console.WriteLine("#\tTo Do\t\tStatus");
        for(int i = 0; i<_entries.Count; i++)
        {
            Console.WriteLine($"{i+1}\t{_entries[i].Content}\t\t{GetStatusName(_entries[i].Status)}");
        }
    }

    public void UpdateToDoContent(int index, string content)
    {
        index--;
        if(index < 0 ||  entries.Count <= index)
        {
            Console.WriteLine("To Do does not exists\nPlease pick a valid To Do");
            return;
        }
        Console.WriteLine($"\"{entries[index].Content}\" was updated to \"{content}\", successfully!");
        entries[index] = new ToDoEntry(content, entries[index].Status);
        Save();
    } 

    public void UpdateToDoStatus(int index, int status)
    {
        index--;
        if(index < 0 ||  entries.Count <= index)
        {
            Console.WriteLine("To Do does not exists\nPlease pick a valid To Do");
            return;
        }
        Console.WriteLine($"\"{entries[index].Content}\" status was updated from \"{GetStatusName(entries[index].Status)}\" to \"{GetStatusName(status)}\", successfully!");
        entries[index] = new ToDoEntry(entries[index].Content, status);
        Save();
    } 

    public void Remove(int index)
    {
        index--;
        if(index < 0 ||  entries.Count <= index)
        {
            Console.WriteLine("To Do does not exists\nPlease pick a valid To Do");
            return;
        }
        Console.WriteLine($"Todo #{index + 1}  \"{entries[index].Content}\" was delete!");
        entries.RemoveAt(index);
        Save();
    }

    

    public static string GetStatusName(int statusCode)
    {
        return statusCode switch
        {
            2 => "Done",
            1 => "In progress",
            0 => "To do",
            _ => "Invalid"
        };
    }
}

public record ToDoEntry(string Content, int Status);
