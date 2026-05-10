using System;
using System.Collections.Generic;
using System.Linq;

class Job
{
    public string Name { get; set; }
    public int ArrivalTime { get; set; }
    public int ExecutionTime { get; set; }
    public int RemainingTime { get; set; }
    public int StartTime { get; set; }
    public int FinishTime { get; set; }
    public int TurnaroundTime { get; set; }
    public double NormalizedTurnaroundTime { get; set; }

    public Job(string name, int arrivalTime, int executionTime)
    {
        Name = name;
        ArrivalTime = arrivalTime;
        ExecutionTime = executionTime;
        ResetJob();
    }

    public void ResetJob()
    {
        RemainingTime = ExecutionTime;
        StartTime = -1;
        FinishTime = 0;
        TurnaroundTime = 0;
        NormalizedTurnaroundTime = 0;
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Table 2 Process Data
        var originalJobs = new List<Job>
        {
            new Job("A", 0, 3),
            new Job("B", 2, 6),
            new Job("C", 5, 5),
            new Job("D", 6, 3),
            new Job("E", 8, 6),
            new Job("F", 9, 2),
            new Job("G", 10, 6)
        };

        int[] timeQuanta = { 1, 3, 4, 6 };

        foreach (var tq in timeQuanta)
        {
            var jobs = originalJobs.Select(j => new Job(j.Name, j.ArrivalTime, j.ExecutionTime)).ToList();
            Console.WriteLine($"\n==========================================");
            Console.WriteLine($"Round Robin Scheduling with TQ = {tq}:");
            Console.WriteLine($"==========================================");
            RoundRobinScheduling(jobs, tq);
        }
    }

    static void RoundRobinScheduling(List<Job> jobs, int timeQuantum)
    {
        jobs = jobs.OrderBy(job => job.ArrivalTime).ToList();
        int currentTime = 0;
        Queue<Job> readyQueue = new Queue<Job>();
        int index = 0;
        int completedJobs = 0;

        while (completedJobs < jobs.Count)
        {
            // 1. Add jobs that have arrived by the current time
            while (index < jobs.Count && jobs[index].ArrivalTime <= currentTime)
            {
                readyQueue.Enqueue(jobs[index]);
                index++;
            }

            if (readyQueue.Count == 0)
            {
                if (index < jobs.Count)
                {
                    currentTime = jobs[index].ArrivalTime;
                }
                continue;
            }

            Job currentJob = readyQueue.Dequeue();

            // Record the very first time the job touches the CPU
            if (currentJob.StartTime == -1)
            {
                currentJob.StartTime = currentTime;
            }

            int executionTime = Math.Min(timeQuantum, currentJob.RemainingTime);
            currentJob.RemainingTime -= executionTime;
            currentTime += executionTime;

            // 2. CRITICAL FIX: Check for new arrivals DURING the time slice 
            // before re-adding the current job to the back of the queue.
            while (index < jobs.Count && jobs[index].ArrivalTime <= currentTime)
            {
                readyQueue.Enqueue(jobs[index]);
                index++;
            }

            if (currentJob.RemainingTime == 0)
            {
                currentJob.FinishTime = currentTime;
                currentJob.TurnaroundTime = currentJob.FinishTime - currentJob.ArrivalTime;
                currentJob.NormalizedTurnaroundTime = (double)currentJob.TurnaroundTime / currentJob.ExecutionTime;
                completedJobs++;

                Console.WriteLine($"Job {currentJob.Name}: Finish {currentJob.FinishTime}, " +
                                  $"Turnaround {currentJob.TurnaroundTime}, " +
                                  $"Normalized {currentJob.NormalizedTurnaroundTime:F2}");
            }
            else
            {
                // Job not finished; goes to the back of the queue
                readyQueue.Enqueue(currentJob);
            }
        }
    }
}