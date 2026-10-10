using System.Text.Json;
using System.Text.Json.Serialization;

public record class Expense
{
    public string? Description {get; set;}
    public decimal Amount{get; set;}
    public DateOnly Date {get; init;}
    
    [JsonConstructor]
    private Expense()
    {
        
    }

    public Expense( string description, decimal amount)
    {
        Description = description;
        Amount = amount < 0? 0 : amount;
        Date =  DateOnly.FromDateTime(DateTime.Now);
    }

    public Expense(string description, decimal amount, DateOnly date)
    {
        Description = description;
        Amount = amount < 0? 0 : amount;
        Date = date;
    }
}

public class ExpensePersistency
{
    public List<Expense> Expenses = new List<Expense>();
    string saveFilePath = Path.Combine(Directory.GetCurrentDirectory(),"expenses.json");

    public ExpensePersistency()
    {
        Load();
    }

    void Load()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            try
            {
                Expenses = JsonSerializer.Deserialize<List<Expense>>(json);
            }
            catch(Exception e)
            {
                Console.Error.WriteLine("Unable to read save file.");
                Console.WriteLine($"Reason: {e.Message}");
                Console.WriteLine("Creating a new file...");
            }
        }
    }

    void Save()
    {
        string json = JsonSerializer.Serialize(Expenses, new JsonSerializerOptions{WriteIndented = true });
        File.WriteAllText(saveFilePath,json);
    }

    public void AddExpense(string description, decimal amount)
    {
        if (amount < 0)
        {
            amount = 0;
        }
        Expenses.Add(new Expense(description,amount));
        Save();
        Console.WriteLine($"Expense \"{description} - ${amount}\" added successfully");
    }

    public void ListExpenses(int monthOption)
    {
        if(!(0 <= monthOption && monthOption <= 12)){
            Console.Error.WriteLine($"Invalid month filter, pick a month number between 1 - 12");
            return;
        }
        List<Expense> expensesToPrint = monthOption == 0? Expenses : Expenses.Where(expense => expense.Date.Month == monthOption).ToList();

        if(expensesToPrint.Count  <= 0){Console.WriteLine("No expenses to show");}
        string outputFormat = "{0, -3} {1, -10} {2, -15} {3, -10}\n";
        Console.WriteLine(outputFormat,"ID","Date","Amount","Description");
        int id = 0;
        foreach(Expense expense in expensesToPrint)
        {
            id++;
            Console.WriteLine(outputFormat,id,expense.Date,$"${expense.Amount:0.00}",expense.Description);
        }
    }

    public void UpdateExpense(int id, string updatedDescription, decimal updatedAmount)
    {
        if(Expenses.Count <= 0)
        {
            Console.Error.WriteLine("Could not complete the operation.\nThere are no expenses to update");
            return;
        }
        if(!IsValidID(id - 1))
        {
            Console.Error.WriteLine($"Invalid ID, please choose an ID between 1 - {Expenses.Count}");
            return;
        }

        string description = (updatedDescription == default? Expenses[id-1].Description : updatedDescription)!;

        decimal amount = updatedAmount < 0? Expenses[id - 1].Amount : updatedAmount;

        Console.WriteLine($"Expense#{id}: \"{Expenses[id-1].Description} ${Expenses[id-1].Amount:0.00}\" was successfully updated to \"{description} ${amount:0.00}\"");

        Expenses[id-1].Description = description;
        Expenses[id-1].Amount = updatedAmount;

        Save();
    }

    public void DeleteExpense(int id)
    {
        if(Expenses.Count <= 0)
        {
            Console.Error.WriteLine("Could not complete the operation.\nThere are no expenses to delete");
            return;
        }
        if(!IsValidID(id - 1))
        {
            Console.Error.WriteLine($"Invalid ID, please choose an ID between 1 - {Expenses.Count}");
            return;
        }

        Expense toRemove = Expenses[id - 1];
        Console.WriteLine($"Expense #{id}: \"{toRemove.Description} ${toRemove.Amount}\" was successfully removed");

        Expenses.RemoveAt(id);
    }

    public void SummaryExpenses(int monthOption)
    {
        if(Expenses.Count <= 0)
        {
            Console.Error.WriteLine("Could not complete the operation.\nThere are no expenses to summarize");
            return;
        }
        if(!(0 <= monthOption && monthOption <= 12)){
            Console.Error.WriteLine($"Invalid month filter, pick a month number between 1 - 12");
            return;
        }

        List<Expense> expensesToSum = monthOption == 0? Expenses : Expenses.Where(expense => expense.Date.Month == monthOption).ToList();

        decimal total = expensesToSum.Sum(expense => expense.Amount);

        string monthString = monthOption == 0? string.Empty : "for " + GetMonth(monthOption);
        Console.WriteLine($"Total expenses {monthString}: ${total}");
    }
    
    string GetMonth(int month)
    {
        return month switch
        {
            1 => "January",
            2 => "February",
            3 => "March",
            4 => "April",
            5 => "May",
            6 => "June",
            7 => "July",
            8 => "August",
            9 => "September",
            10 => "October",
            11 => "November",
            12 => "December",
            _ => string.Empty
        };
    }
    bool IsValidID(int id) => 0 <= id && id < Expenses.Count;
}