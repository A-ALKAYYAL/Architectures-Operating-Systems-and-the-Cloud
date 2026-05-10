using System;
using System.Collections.Generic;
using System.Linq;

// Using a Record simplifies the class definition while keeping all properties
public record TaskEntry(string Label, int EntryTime, int Duration, int Rank)
{
    public int ActualStart { get; set; }
    public int ActualEnd { get; set; }
}

class SchedulerApp
{
    static void Main()
    {
        // Renamed variables and used a collection expression for the list
        var rawData = new List<TaskEntry>
        {
            new("A", 0, 3, 5),
            new("B", 2, 6, 4),
            new("C", 5, 5, 8),
            new("D", 6, 3, 6),
            new("E", 8, 6, 10),
            new("F", 9, 2, 3),
            new("G", 10, 6, 7)
        };

        // Filter: Priority > 5, then sort by arrival time
        var filteredQueue = from t in rawData
                            where t.Rank > 5
                            orderby t.EntryTime
                            select t;

        int timeline = 0;
        Console.WriteLine("Filtered Task Schedule:");
        Console.WriteLine(new string('-', 30));

        foreach (var task in filteredQueue)
        {
            // Handle idle time if the next task hasn't arrived yet
            timeline = Math.Max(timeline, task.EntryTime);

            task.ActualStart = timeline;
            task.ActualEnd = task.ActualStart + task.Duration;
            timeline = task.ActualEnd;

            // Formatted output string
            string output = $"[Task {task.Label}] Priority: {task.Rank} | " +
                            $"Arrived: {task.EntryTime} | Run: {task.Duration} | " +
                            $"Started: {task.ActualStart} | Finished: {task.ActualEnd}";

            Console.WriteLine(output);
        }
    }
}