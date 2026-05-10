using System;
using System.Collections.Generic;

class CacheEntry
{
    public int VirtualPage;
    public int PhysicalPage;
}

class QuickTLB
{
    private List<CacheEntry> storage = new List<CacheEntry>();
    private int maxSize = 4;

    public int? Search(int vpn)
    {
        foreach (var item in storage)
        {
            if (item.VirtualPage == vpn)
                return item.PhysicalPage;
        }
        return null;
    }

    public void Store(int vpn, int ppn)
    {
        if (storage.Count >= maxSize)
            storage.RemoveAt(0);
        storage.Add(new CacheEntry { VirtualPage = vpn, PhysicalPage = ppn });
    }
}

class Runner
{
    static void Main()
    {
        QuickTLB tlbObj = new QuickTLB();
        int translationCount = 0;
        int hitCount = 0;

        int[] requests = { 2, 3, 2, 1, 4, 2, 3, 4, 5, 2 };

        foreach (int v in requests)
        {
            translationCount++;
            int? result = tlbObj.Search(v);

            if (result.HasValue)
            {
                hitCount++;
                Console.WriteLine($"Hit: VPN {v} -> PPN {result.Value}");
            }
            else
            {
                Console.WriteLine($"Miss: VPN {v}, inserting into TLB");
                tlbObj.Store(v, v);
            }
        }

        double ratio = (double)hitCount / translationCount;
        Console.WriteLine($"\nTotal Translations: {translationCount}");
        Console.WriteLine($"Total Hits: {hitCount}");
        Console.WriteLine($"Hit Ratio: {ratio:P2}");
    }
}