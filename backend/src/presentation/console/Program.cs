// See https://aka.ms/new-console-template for more information
using System.Collections.Concurrent;
using Dumpify;

BlockingCollection<string> queue = [];


new Thread(() => {
    foreach(var item in queue.GetConsumingEnumerable()) 
    {
        Console.WriteLine("print");
        Console.WriteLine(item);
    }
}).Start();


while (true) {
    Console.WriteLine("please input");
    var input = Console.ReadLine();
    queue.Add(input);
    Thread.Sleep(100);
}


// public class DatabaseFacade : IDisposable
// {
//     private readonly BlockingCollection<(string item, TaskCompletionSource<string> result)> _queue =
//         new BlockingCollection<(string item, TaskCompletionSource<string> result)>();
//     private readonly Task _processItemsTask;
 
//     public DatabaseFacade() => _processItemsTask = Task.Run(ProcessItems);
 
//     public void Dispose() => _queue.CompleteAdding();
 
//     public Task SaveAsync(string command)
//     {
//         var tcs = new TaskCompletionSource<string>();
//         _queue.Add((item: command, result: tcs));
//         return tcs.Task;
//     }
 
//     private async Task ProcessItems()
//     {
//         foreach (var item in _queue.GetConsumingEnumerable())
//         {
//             Console.WriteLine($"DatabaseFacade: executing '{item.item}'...");
 
//             // Waiting a bit to emulate some IO-bound operation
//             await Task.Delay(100);
//             item.result.SetResult("OK");
//             Console.WriteLine("DatabaseFacade: done.");
//         }
//     }
// }


