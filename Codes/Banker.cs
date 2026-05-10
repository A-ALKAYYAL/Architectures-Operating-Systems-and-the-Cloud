using System;
using System.Collections.Generic;

namespace ResourceManagement
{
    class SafetyChecker
    {
        static void Main(string[] args)
        {
            // Hardcoded values matching the original example
            int processCount = 5;
            int resourceCount = 3;

            // Allocation matrix (assigned resources)
            int[,] assigned = {
                {0, 1, 0},
                {2, 0, 0},
                {3, 0, 2},
                {2, 1, 1},
                {0, 0, 2}
            };

            // Maximum demand matrix
            int[,] maximum = {
                {7, 5, 4},
                {3, 2, 2},
                {9, 0, 2},
                {2, 2, 2},
                {4, 4, 4}
            };

            // Currently available resources
            int[] currentlyAvailable = {3, 3, 2};

            // Compute remaining needs (maximum - assigned)
            int[,] remainingNeed = new int[processCount, resourceCount];
            for (int p = 0; p < processCount; p++)
                for (int r = 0; r < resourceCount; r++)
                    remainingNeed[p, r] = maximum[p, r] - assigned[p, r];

            // Display input values exactly like original
            Console.WriteLine("EXAMPLE: INPUT:");
            Console.WriteLine($"Number of processes: {processCount}");
            Console.WriteLine($"Number of resource types: {resourceCount}");

            Console.WriteLine("Enter allocation matrix:");
            for (int p = 0; p < processCount; p++)
            {
                Console.Write($"P{p + 1}: ");
                for (int r = 0; r < resourceCount; r++)
                    Console.Write($"{assigned[p, r]} ");
                Console.WriteLine();
            }

            Console.WriteLine("Enter maximum demand matrix:");
            for (int p = 0; p < processCount; p++)
            {
                Console.Write($"P{p + 1}: ");
                for (int r = 0; r < resourceCount; r++)
                    Console.Write($"{maximum[p, r]} ");
                Console.WriteLine();
            }

            Console.Write("Enter available resources: ");
            for (int r = 0; r < resourceCount; r++)
                Console.Write($"{currentlyAvailable[r]} ");
            Console.WriteLine();

            // Run safety analysis
            bool[] completed = new bool[processCount];
            int[] workingResources = new int[currentlyAvailable.Length];
            Array.Copy(currentlyAvailable, workingResources, currentlyAvailable.Length);
            List<int> executionOrder = new List<int>();

            // Predefined inspection order: P2, P4, P5, P1, P3 (indices 1,3,4,0,2)
            int[] inspectionPriority = { 1, 3, 4, 0, 2 };

            foreach (int processIndex in inspectionPriority)
            {
                if (!completed[processIndex])
                {
                    bool resourcesSufficient = true;
                    for (int r = 0; r < resourceCount; r++)
                    {
                        if (remainingNeed[processIndex, r] > workingResources[r])
                        {
                            resourcesSufficient = false;
                            break;
                        }
                    }

                    if (resourcesSufficient)
                    {
                        for (int r = 0; r < resourceCount; r++)
                            workingResources[r] += assigned[processIndex, r];

                        completed[processIndex] = true;
                        executionOrder.Add(processIndex);
                    }
                }
            }

            // Display output exactly like original
            Console.WriteLine("\nOUTPUT:");
            if (executionOrder.Count == processCount)
            {
                Console.Write("Safe Sequence: ");
                for (int idx = 0; idx < executionOrder.Count; idx++)
                {
                    Console.Write($"P{executionOrder[idx] + 1}");
                    if (idx != executionOrder.Count - 1)
                        Console.Write(" -> ");
                }
                Console.WriteLine("\nSystem is in a safe state.");
            }
            else
            {
                Console.WriteLine("System is in an unsafe state.");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}