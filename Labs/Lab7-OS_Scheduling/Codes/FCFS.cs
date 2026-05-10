using System;
using System.Collections.Generic;
using System.Linq;

public record ProcessTask(string ID, int ArriveAt, int Duration, int Weight)
{
    public int TStart { get; set; }
    public int TEnd { get; set; }
}

class SchedulerSystem
{
    static void Main()
    {
        List<ProcessTask> taskList = 
        [
            new("A", 0, 3, 5),
            new("B", 2, 6, 4),
            new("C", 5, 5, 8),
            new("D", 6, 3, 6),
            new("E", 8, 6, 10),
            new("F", 9, 2, 3),
            new("G", 10, 6, 7)
        ];

        // REMOVED THE .Where(t => t.Weight > 5) FILTER
        // Now it takes ALL jobs and sorts them by arrival time
        var fullQueue = taskList
            .OrderBy(t => t.ArriveAt)
            .ToList();

        int globalTime = 0;
        Console.WriteLine("--- Full Process Scheduling Report (A-G) ---");

        foreach (var task in fullQueue)
        {
            // If CPU is idle, jump to the time the next job arrives
            globalTime = Math.Max(globalTime, task.ArriveAt);

            task.TStart = globalTime;
            task.TEnd = task.TStart + task.Duration;
            globalTime = task.TEnd;

            Console.WriteLine($"[ID: {task.ID}] | Priority: {task.Weight} | " +
                              $"Arrival: {task.ArriveAt} | Exec: {task.Duration} | " +
                              $"Window: {task.TStart} - {task.TEnd}");
        }
    }
}