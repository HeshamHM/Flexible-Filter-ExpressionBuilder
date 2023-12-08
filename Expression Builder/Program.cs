using Expression_Builder;
using System.Linq.Expressions;

class Program
{
    static void Main()
    {
        IEnumerable<User> users = UserDate();
        UserExpressionBuilder expressionBuilder = UserExpressionBuilder.CreateInstance(Expression.Parameter(typeof(User), "u"));
        while (true)
        {
            Console.WriteLine("1. Print all users");
            Console.WriteLine("2. Add active filter");
            Console.WriteLine("3. Add search filter");
            Console.WriteLine("4. Add country filter");
            Console.WriteLine("5. Apply filters and print");
            Console.WriteLine("6. Create new instance");
            Console.WriteLine("7. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Print(users);
                    break;
                case "2":
                    Console.Write("Enter active filter (true/false): ");
                    if (bool.TryParse(Console.ReadLine(), out bool isActive))
                    {
                        expressionBuilder.AddActiveFilter(isActive);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter true or false.");
                    }
                    break;
                case "3":
                    Console.Write("Enter search term: ");
                    string searchTerm = Console.ReadLine();
                    expressionBuilder.AddSearchExpression(searchTerm);
                    break;
                case "4":
                    Console.Write("Enter country filter: ");
                    string countryFilter = Console.ReadLine();
                    expressionBuilder.AddCountryFilter(countryFilter);
                    break;
                case "5":
                    Print(users.Where(expressionBuilder.Build().Compile()));
                    break;
                case "6":
                    expressionBuilder = UserExpressionBuilder.CreateInstance(Expression.Parameter(typeof(User), "u"));
                    break;
                case "7":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }


private static void Print(IEnumerable<User> users)
    {
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
        Console.WriteLine("|   Id   |   Name            \t|   Email              \t |   Country              \t|   IsActive   |");
        Console.WriteLine("---------------------------------------------------------------------------------------------------------------");

        foreach (var user in users)
        {
            Console.WriteLine($"|   {user.Id,-4} |   {user.Name,-18} |   {user.Email,-20} | {user.Country,-10}              \t|   {user.IsActive,-11} |");
        }

        Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
    }
    private static IEnumerable<User> UserDate()
    {
        return new[]
        {
           new User { Name = "Hesham Mohamed", Id = 1, Email = "h1@gmail.com", Country = "Egypt" },
    new User { Name = "John Doe", Id = 2, Email = "john@gmail.com", Country = "USA" },
    new User { Name = "Alice Smith", Id = 3, Email = "alice@gmail.com", Country = "Canada" },
    new User { Name = "Bob Johnson", Id = 4, Email = "bob@gmail.com", Country = "UK" },
    new User { Name = "Emma Davis", Id = 5, Email = "emma@gmail.com", Country = "Australia" },
    new User { Name = "Michael Wilson", Id = 6, Email = "michael@gmail.com", Country = "Germany" },
    new User { Name = "Sophia Miller", Id = 7, Email = "sophia@gmail.com", Country = "France" },
    new User { Name = "Matthew Brown", Id = 8, Email = "matthew@gmail.com", Country = "Spain" },
    new User { Name = "Olivia Jones", Id = 9, Email = "olivia@gmail.com", Country = "Italy" },
    new User { Name = "Daniel Taylor", Id = 10, Email = "daniel@gmail.com", Country = "Japan" },
    new User { Name = "Ava Martinez", Id = 11, Email = "ava@gmail.com", Country = "China" },
    new User { Name = "William Jackson", Id = 12, Email = "william@gmail.com", Country = "Brazil" },
    new User { Name = "Emily Harris", Id = 13, Email = "emily@gmail.com", Country = "Mexico" },
    new User { Name = "Liam Nelson", Id = 14, Email = "liam@gmail.com", Country = "South Africa" },
    new User { Name = "Mia Wilson", Id = 15, Email = "mia@gmail.com", Country = "Russia",IsActive=false },
    new User { Name = "James Anderson", Id = 16, Email = "james@gmail.com", Country = "India" ,IsActive=false },
    new User { Name = "Charlotte Thomas", Id = 17, Email = "charlotte@gmail.com", Country = "South Korea" },
    new User { Name = "Benjamin Lee", Id = 18, Email = "benjamin@gmail.com", Country = "Argentina" },
    new User { Name = "Amelia White", Id = 19, Email = "amelia@gmail.com", Country = "Nigeria" },
    new User { Name = "Ethan Harris", Id = 20, Email = "ethan@gmail.com", Country = "Saudi Arabia" },
    new User { Name = "Harper Davis", Id = 21, Email = "harper@gmail.com", Country = "Turkey" },
    new User { Name = "Jackson Taylor", Id = 22, Email = "jackson@gmail.com", Country = "Indonesia" },
    new User { Name = "Aria Brown", Id = 23, Email = "aria@gmail.com", Country = "Thailand" },
    new User { Name = "Logan Martinez", Id = 24, Email = "logan@gmail.com", Country = "Vietnam",IsActive=false },
    new User { Name = "Grace Wilson", Id = 25, Email = "grace@gmail.com", Country = "Philippines" },
    new User { Name = "Isaac Johnson", Id = 26, Email = "isaac@gmail.com", Country = "Malaysia" },
    new User { Name = "Lily Anderson", Id = 27, Email = "lily@gmail.com", Country = "Singapore" , IsActive = false},
    new User { Name = "Carter Thomas", Id = 28, Email = "carter@gmail.com", Country = "New Zealand" },
    new User { Name = "Chloe Lee", Id = 29, Email = "chloe@gmail.com", Country = "Egypt" },
    new User { Name = "Owen Harris", Id = 30, Email = "owen@gmail.com", Country = "Brazil" }

        };
    }
}