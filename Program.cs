using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        StudentClub studentClub = new StudentClub();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\nStudent Club Management System");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("1. Register a New Society");
            Console.WriteLine("2. Allocate Funding to Societies");
            Console.WriteLine("3. Register an Event for a Society");
            Console.WriteLine("4. Display Society Funding Information");
            Console.WriteLine("5. Display Events for a Society");
            Console.WriteLine("6. Exit");
            Console.Write("Select an option: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    studentClub.RegisterSociety();
                    break;
                case 2:
                    studentClub.AllocateFunding();
                    break;
                case 3:
                    studentClub.RegisterEvent();
                    break;
                case 4:
                    studentClub.DisplayFundingInfo();
                    break;
                case 5:
                    studentClub.DisplayEvents();
                    break;
                case 6:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }
        }
    }
}

// ClubRole class
class ClubRole
{
    public string Name { get; set; }
    public string Role { get; set; }
    public string ContactInfo { get; set; }
}

// Society class
class Society
{
    public string Name { get; set; }
    public string Contact { get; set; }
    public List<Activity> Activities { get; set; } = new List<Activity>();

    public virtual void AddActivity(string activityName)
    {
        Activities.Add(new Activity { Name = activityName });
        Console.WriteLine($"Activity '{activityName}' added to Society '{Name}'.");
    }

    public void ListEvents()
    {
        Console.WriteLine($"Events for Society: {Name}");
        foreach (var activity in Activities)
        {
            Console.WriteLine("- " + activity.Name);
        }
    }
}

// FundedSociety class (inherits from Society)
class FundedSociety : Society
{
    public double FundingAmount { get; set; }
}

// NonFundedSociety class (inherits from Society)
class NonFundedSociety : Society
{
}

// Activity class
class Activity
{
    public string Name { get; set; }
}

// StudentClub class
class StudentClub
{
    public double Budget { get; set; } = 1000; // Example budget
    public ClubRole President { get; set; }
    public ClubRole VicePresident { get; set; }
    public ClubRole GeneralSecretary { get; set; }
    public ClubRole FinanceSecretary { get; set; }
    public List<Society> Societies { get; set; } = new List<Society>();

    public void RegisterSociety()
    {
        Console.Write("Enter Society Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Contact Person: ");
        string contact = Console.ReadLine();
        Console.Write("Is it a funded society? (yes/no): ");
        string isFunded = Console.ReadLine().ToLower();

        Society society;
        if (isFunded == "yes")
        {
            society = new FundedSociety();
            Console.Write("Enter Funding Amount: ");
            ((FundedSociety)society).FundingAmount = double.Parse(Console.ReadLine());
        }
        else
        {
            society = new NonFundedSociety();
        }

        society.Name = name;
        society.Contact = contact;
        Societies.Add(society);
        Console.WriteLine($"Society '{name}' registered successfully.");
    }

    public void AllocateFunding()
    {
        Console.Write("Enter Society Name for Funding Allocation: ");
        string name = Console.ReadLine();
        FundedSociety society = Societies.Find(s => s is FundedSociety && s.Name == name) as FundedSociety;

        if (society != null)
        {
            Console.Write("Enter Funding Amount: ");
            double amount = double.Parse(Console.ReadLine());
            society.FundingAmount += amount;
            Console.WriteLine($"Funding allocated to Society '{name}'. Total Funding: {society.FundingAmount}");
        }
        else
        {
            Console.WriteLine("Society not found or is not a funded society.");
        }
    }

    public void RegisterEvent()
    {
        Console.Write("Enter Society Name to Register Event: ");
        string name = Console.ReadLine();
        Society society = Societies.Find(s => s.Name == name);

        if (society != null)
        {
            Console.Write("Enter Event Name: ");
            string eventName = Console.ReadLine();
            society.AddActivity(eventName);
        }
        else
        {
            Console.WriteLine("Society not found.");
        }
    }

    public void DisplayFundingInfo()
    {
        Console.WriteLine("\nSociety Funding Information:");
        foreach (var society in Societies)
        {
            if (society is FundedSociety fundedSociety)
            {
                Console.WriteLine($"{fundedSociety.Name} - Funding: {fundedSociety.FundingAmount}");
            }
            else
            {
                Console.WriteLine($"{society.Name} - No Funding");
            }
        }
    }

    public void DisplayEvents()
    {
        Console.Write("Enter Society Name to Display Events: ");
        string name = Console.ReadLine();
        Society society = Societies.Find(s => s.Name == name);

        if (society != null)
        {
            society.ListEvents();
        }
        else
        {
            Console.WriteLine("Society not found.");
        }
    }
}