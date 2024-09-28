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
                    Console.WriteLine("Bye!");
                    return;

                default:
                    Console.WriteLine("Unknown Command. Please enter add, remove, list or exit");
                    break;
            }
            Console.Write("What would you like to do: ");
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
            switch (task.ToLower())
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
        Console.Write("Enter the task id to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
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
}