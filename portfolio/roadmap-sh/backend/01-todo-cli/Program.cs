public class Program
{
    public static void Main(string[] args)
    {
        if(args.Length<=0){return;}

        ToDo toDo = new ToDo("ToDo");
        
        string result = string.Empty;

        switch (args[0].ToLower())
        {
            case "add":                
                if(args.Length < 1){ Console.WriteLine("Please provide a task name."); break;}
                
                for(int i = 1; i< args.Length - 1; i++){result += args[i] + " ";}
                result += args[^1];
                toDo.AddToDo(new ToDoEntry(result,0));
                break;
            
            case "list":
                if(args.Length < 2){ toDo.ListToDo(); break;}

                toDo.ListToDo(args[1]);
                break;
            
            case "update-status":
                if(args.Length<3){Console.WriteLine("Please provide a to do ID and a status to update"); break;}

                int statusIndex;
                if(!int.TryParse(args[1], out statusIndex)){Console.WriteLine("Please provide a valid ID (Number greater than 0)"); break;}

                toDo.UpdateToDoStatus(statusIndex,args[2]);                
                break;
            
            case "update-content":
                if(args.Length<3){Console.WriteLine("Please provide a to do ID and content to update"); break;}

                int contentIndex;
                if(!int.TryParse(args[1], out contentIndex)){Console.WriteLine("Please provide a valid ID (Number greater than 0)"); break;}
                for(int i = 2; i< args.Length; i++){result += args[i] + " ";}
                toDo.UpdateToDoContent(contentIndex,result);
                break;
            
            case "remove":
                if(args.Length<2){Console.WriteLine("Please provide an task number to remove."); break;}

                if(int.TryParse(args[1], out int toRemove)){toDo.Remove(toRemove);}
                break;
            
            case "help":
                Console.WriteLine("To Do CLI commands:");
                Console.WriteLine("add \"content\"\t\tCreates a new to do with the specified content.");
                Console.WriteLine("list\t\tShows all to dos");
                Console.WriteLine($"list \"status-filter\"\t\tShows todos using the specified filter ({ToDo.GetStatusFilters()})");
                Console.WriteLine($"update-status \"todo-id\" \"updated-status\"\t\tUpdates the specified to do to the specified status ({ToDo.GetStatusFilters()}).");
                Console.WriteLine("update-content \"todo-id\" \"updated-content\"\t\tUpdates the specified to do to the specified content.");
                Console.WriteLine("help\t\tShows this information.");
                break;
            
            default:
                Console.WriteLine($"{args[0]} is not a known command, run \"help\" for more info.");
                break;
        }
    }
}