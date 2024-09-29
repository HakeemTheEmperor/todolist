using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        string todoFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "TODO");
        string tasksFile = Path.Combine(todoFolder, "tasks.json");

        if (!Directory.Exists(todoFolder))
        {
            Directory.CreateDirectory(todoFolder);
        }

        List<string> tasks = LoadTasks(tasksFile);

        PrintHeader();

        Console.Write("Welcome to the tasks app! \nEnter a command (add, delete, list, exit) ");

        while (true)
        {
            string? command = Console.ReadLine()?.Trim().ToLower();

            switch (command)
            {
                case "add":
                    AddTask(tasks, tasksFile);
                    break;

                case "list":
                    ListTasks(tasks);
                    break;

                case "delete":
                    RemoveTask(tasks, tasksFile);
                    break;

                case "exit":
                    SaveTasks(tasks, tasksFile);
                    PrintFooter();
                    return;

                case "clear":
                    ClearTasks(tasks, tasksFile);
                    break;

                default:
                    Console.WriteLine("Unknown Command. Please enter add, delete, list or exit");
                    break;
            }
            Console.Write("What would you like to do (add, delete, list, exit): ");
        }
    }

    static List<string> LoadTasks(string tasksFile)
    {
        List<string>? tasks = new List<string>();

        if (File.Exists(tasksFile))
        {
            string json = File.ReadAllText(tasksFile);
            tasks = JsonSerializer.Deserialize<List<string>>(json);
        }

        return tasks ?? new List<string>();
    }

    static void AddTask(List<string> tasks, string tasksFile)
    {
        while (true)
        {
            Console.Write("Enter a new task / enter done: ");
            string? task = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(task))
            {
                Console.Write("Please enter a valid task (cannot be empty): ");
                task = Console.ReadLine();
            }
            switch (task.Trim().ToLower())
            {
                case "done":
                    SaveTasks(tasks, tasksFile);
                    return;
                default:
                    tasks.Add(task);
                    Console.WriteLine($"Task {task} added.");
                    Console.WriteLine();
                    break;
            }
        }
    }

    static void ListTasks(List<string> tasks)
    {
        Console.WriteLine("id\t tasks");
        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}.\t {tasks[i]}");
        }
        Console.WriteLine();
    }

    static void RemoveTask(List<string> tasks, string tasksFile)
    {
        ListTasks(tasks);
        Console.WriteLine();
        Console.Write("Enter the task id to delete: ");
        string? input = Console.ReadLine();

        if (input?.ToLower().Trim() == "back")
        {
            Console.WriteLine("Operation cancelled by User!");
            return;
        }
        else if (int.TryParse(input, out int index) && index > 0 && index <= tasks.Count)
        {
            string removedTask = tasks[index - 1];
            tasks.RemoveAt(index - 1);
            SaveTasks(tasks, tasksFile);
            Console.WriteLine($"Task {removedTask} removed.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Invalid task id. Please try again.");
            Console.WriteLine();
        }
    }

    static void SaveTasks(List<string> tasks, string tasksFile)
    {
        string json = JsonSerializer.Serialize(tasks);
        File.WriteAllText(tasksFile, json);
    }

    static void ClearTasks(List<string> tasks, string tasksFile) 
    { 
        tasks.Clear();
        SaveTasks(tasks, tasksFile);
        Console.WriteLine("All tasks cleared.");
        Console.WriteLine();
    }

    static void ResetTasksDaily(List<string> tasks, string tasksFile)
    {
        tasks.Clear();
        SaveTasks(tasks, tasksFile);
        Console.WriteLine("Daily tasks reset.");
        Console.WriteLine();
    }

    static void PrintHeader()
{
    Console.Clear();
    Console.WriteLine("===============================================");
    Console.WriteLine("             Todo List App                  ");
    Console.WriteLine("===============================================");
}

static void PrintFooter()
{
    Console.WriteLine("===============================================");
    Console.WriteLine("                     GoodBye                   ");
    Console.WriteLine("===============================================");
}
}