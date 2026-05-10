using System;
using System.Collections.Generic;
using System.Linq;

class Job
{
    public string Name { get; set; }
    public int ArrivalTime { get; set; }
    public int ExecutionTime { get; set; }
    public int RemainingTime { get; set; }
    public int FinishTime { get; set; }
    public int TurnaroundTime => FinishTime - ArrivalTime;
    public int WaitingTime => TurnaroundTime - ExecutionTime;

    public Job(string name, int arrival, int execution)
    {
        Name = name;
        ArrivalTime = arrival;
        ExecutionTime = execution;
        RemainingTime = execution;
    }

    public void Reset() => RemainingTime = ExecutionTime;
}

class Program
{
    static void Main()
    {
        // Data from your table
        var jobs = new List<Job>
        {
            new Job("A", 0, 3),
            new Job("B", 2, 6),
            new Job("C", 5, 5),
            new Job("D", 6, 3),
            new Job("E", 8, 6),
            new Job("F", 9, 2),
            new Job("G", 10, 6)
        };

        // UPDATED: Now only showing TQ 1 and 6
        int[] timeQuantums = { 1, 6 };

        foreach (var tq in timeQuantums)
        {
            Console.WriteLine($"\n================================");
            Console.WriteLine($" ROUND ROBIN (TQ = {tq})");
            Console.WriteLine($"================================");
            
            // Reset for each simulation run
            jobs.ForEach(j => j.Reset());
            RunRoundRobin(jobs, tq);
        }
    }

    static void RunRoundRobin(List<Job> jobs, int tq)
    {
        int currentTime = 0;
        int completed = 0;
        var readyQueue = new Queue<Job>();
        var remainingJobs = new List<Job>(jobs.OrderBy(j => j.ArrivalTime));
        
        Console.WriteLine("Timeline: [Start - End] Job (Remaining)");

        while (completed < jobs.Count)
        {
            // 1. Add all jobs that arrived by the current time
            while (remainingJobs.Count > 0 && remainingJobs[0].ArrivalTime <= currentTime)
            {
                readyQueue.Enqueue(remainingJobs[0]);
                remainingJobs.RemoveAt(0);
            }

            if (readyQueue.Count == 0)
            {
                if (remainingJobs.Count > 0)
                    currentTime = remainingJobs[0].ArrivalTime;
                continue;
            }

            Job current = readyQueue.Dequeue();
            int slice = Math.Min(current.RemainingTime, tq);
            
            Console.WriteLine($"[{currentTime,2} - {currentTime + slice,2}] Task {current.Name} (Left: {current.RemainingTime - slice})");
            
            currentTime += slice;
            current.RemainingTime -= slice;

            // 2. Add jobs that arrived *during* execution
            while (remainingJobs.Count > 0 && remainingJobs[0].ArrivalTime <= currentTime)
            {
                readyQueue.Enqueue(remainingJobs[0]);
                remainingJobs.RemoveAt(0);
            }

            // 3. Re-queue or complete
            if (current.RemainingTime > 0)
            {
                readyQueue.Enqueue(current);
            }
            else
            {
                current.FinishTime = currentTime;
                completed++;
            }
        }

        PrintMetrics(jobs);
    }

    static void PrintMetrics(List<Job> jobs)
    {
        Console.WriteLine("\nFinal Metrics:");
        Console.WriteLine("Job | Finish | Turnaround | Waiting");
        foreach (var j in jobs)
            Console.WriteLine($"{j.Name,-3} | {j.FinishTime,-6} | {j.TurnaroundTime,-10} | {j.WaitingTime}");
        
        Console.WriteLine($"\nAverage Waiting Time: {jobs.Average(j => j.WaitingTime):F2} ms");
    }
}