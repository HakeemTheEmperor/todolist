// See https://aka.ms/new-console-template for more information
using System;
using System.Data;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        List<string> tasks = new List<string>();
        string? command;

        Console.Write("Welcome to the tasks app! \nEnter a command (add, delete, list, exit) ");

        while (true)
        {
            command = Console.ReadLine()?.Trim().ToLower();

            switch (command)
            {
                case "add":
                    AddTask(tasks);
                    break;
                
                case "list":
                    ListTasks(tasks);
                    break;

                case "delete":
                    RemoveTask(tasks);
                    break;
                    
                case "exit":
                    Console.WriteLine("Bye!");
                    return;
                    
                default:
                    Console.WriteLine("Unknown Command. Please enter add, remove, list or exit");
                    break;

            }
            Console.Write("What would you like to do: ");
        }
    }

    static void AddTask(List<string> tasks)
    {
        while (true)
        {
        Console.Write("Enter a new task / enter done : ");
        string? task = Console.ReadLine();
        while (string.IsNullOrEmpty(task))
        {
            Console.Write("Enter a new task / enter done: ");
            task = Console.ReadLine();
        }
        switch (task)
        {
            case "done":
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
    static void RemoveTask(List<string> tasks)
    {
        Console.Write("Enter the task id to delete: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= tasks.Count)
        {
            string removedTask = tasks[index - 1];
            tasks.RemoveAt(index - 1);
            Console.WriteLine($"Task {removedTask} removed.");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine("Invalid task id. Please try again.");
            Console.WriteLine();
        }
    }
}