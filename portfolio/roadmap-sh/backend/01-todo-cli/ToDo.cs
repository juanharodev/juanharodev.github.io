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
        if (!FilePath.EndsWith(".json")){FilePath += ".json";}

        string json = string.Empty;
        if(File.Exists(FilePath))
        {
            json =  File.ReadAllText(FilePath);
        }

        try
        {
            entries = JsonSerializer.Deserialize<List<ToDoEntry>>(json)!;
        }
        catch
        {
            entries = new List<ToDoEntry>();
            Save();
        }
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

    public void ListToDo(string status)
    {
        ListToDo(GetStatusCode(status));
    }

    public void ListToDo(int status)
    {
        if("Invalid" == GetStatusName(status)){Console.WriteLine($"Invalid status, please provide a valid status ({GetStatusFilters()})");  return;}
        List<ToDoEntry> _entries = entries.Where(entry => entry.Status == status).ToList();  
        PrintToDo(_entries);
    }

    static void PrintToDo(List<ToDoEntry> _entries)
    {
        Console.WriteLine("ID\tTo Do\t\tStatus");
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

    public void UpdateToDoStatus(int index, string status)
    {
        UpdateToDoStatus(index, GetStatusCode(status));
    }

    public void UpdateToDoStatus(int index, int status)
    {
        index--;
        if(index < 0 ||  entries.Count <= index)
        {
            Console.WriteLine("To Do does not exists\nPlease pick a valid To Do");
            return;
        }
        if(GetStatusName(status) == "Invalid")
        {
            Console.WriteLine($"Please provide a valid status ({GetStatusFilters()})"); 
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
        Console.WriteLine($"Todo ID#{index + 1}: \"{entries[index].Content}\", was delete!");
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

    public int GetStatusCode(string status)
    {
        return status.ToLower() switch
        {
            "0" or "todo" => 0,
            "1" or "in-progress" => 1,
            "2" or "done" => 2,
            _ => -1,
        };
    }

    public static string GetStatusFilters()
    {
        return "To do: 0, todo; In progress: 1, in-progress; Done: 2, done.";
    }
}

public record ToDoEntry(string Content, int Status);