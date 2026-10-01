using System.CommandLine;

public class Program
{
    public static int Main(string[] args)
    {
        Option<string> descriptionOption = new("--description")
        {
            Description = "An expense description",
        };

        Option<decimal> amountOption = new("--amount")
        {
          Description = "An expense amount", 
          DefaultValueFactory = parseResult => -1,
          CustomParser = result =>
          {
              if(decimal.TryParse(result.Tokens.Single().Value, out decimal amount)){
                if(amount < 0)
                {
                    result.AddError("Amount must be a 0 or greater decimal number");
                }
              }
              else
              {
                  result.AddError("Amount must be a decimal number");
              }

              return amount;
          }
        };

        Command addCommand = new("add","Add a new expense")
        {
            descriptionOption,
            amountOption
        };

        addCommand.SetAction(parseResult => { AddExpense(parseResult.GetValue(descriptionOption)!, parseResult.GetValue(amountOption)); });

        Option<int> monthOption = new("--month")
        {
            Description = "Show expenses of picked month (1 - 12)\n0 or blank to show all expenses",
            DefaultValueFactory = parseResult => 0
        };

        Command listCommand = new("list", "List expenses")
        {
            monthOption
        };
        listCommand.SetAction(parseResult => { ListExpenses(parseResult.GetValue(monthOption)); });

        Option<int> idOption = new("--id")
        {
            Description = "Unique identifier of an expense",
            Required = true
        };

        Command updateCommand = new ("update","Update an expense")
        {
            idOption,
            descriptionOption,
            amountOption
        };

        updateCommand.SetAction(parseResult => { UpdateExpense(parseResult.GetValue(idOption),parseResult.GetValue(descriptionOption)!,parseResult.GetValue(amountOption)); });

        Command deleteCommand = new("delete", "Deletes an expense")
        {
            idOption
        };
        deleteCommand.SetAction(parseResult => { DeleteExpense(parseResult.GetValue(idOption)); });

        Command summaryExpenses = new("summary","Summaries expenses")
        {
            monthOption
        };
        summaryExpenses.SetAction(parseResult => { SummaryExpenses(parseResult.GetValue(monthOption)); });

        RootCommand rootCommand = new("Expense tracker");

        rootCommand.Subcommands.Add(addCommand);
        rootCommand.Subcommands.Add(listCommand);
        rootCommand.Subcommands.Add(updateCommand);
        rootCommand.Subcommands.Add(deleteCommand);
        rootCommand.Subcommands.Add(summaryExpenses);

        ParseResult parseResult = rootCommand.Parse(args);
        return parseResult.Invoke();
    }

    public static void AddExpense(string description, decimal amount)
    {
        ExpensePersistency expense = new ExpensePersistency();
        expense.AddExpense(description, amount);
    }
    public static void UpdateExpense(int id, string description, decimal amount)
    {
        ExpensePersistency expense = new ExpensePersistency();
        expense.UpdateExpense(id,description,amount);
    }

    public static void DeleteExpense(int id)
    {
        ExpensePersistency expense = new ExpensePersistency();
        expense.DeleteExpense(id);
    }

    public static void ListExpenses(int month)
    {
        ExpensePersistency expense = new ExpensePersistency();
        expense.ListExpenses(month);
    }

    public static void SummaryExpenses(int monthOption)
    {
        ExpensePersistency expense = new ExpensePersistency();
        expense.SummaryExpenses(monthOption);
    }
        
}