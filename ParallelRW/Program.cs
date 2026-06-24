namespace ParallelRW
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var tasks = new Task[110];

            // 100 читателей
            for (int i = 0; i < 100; i++)
            {
                int id = i;

                tasks[i] = Task.Run(async () =>
                {
                    for (int j = 0; j < 50; j++)
                    {
                        var value = StaticServer.GetCount();

                        Console.WriteLine(
                            $"Reader {id}: {value}");

                        await Task.Delay(Random.Shared.Next(10, 50));
                    }
                });
            }

            // 10 писателей
            for (int i = 0; i < 10; i++)
            {
                int id = i;

                tasks[100 + i] = Task.Run(async () =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        StaticServer.AddToCount(1);

                        Console.WriteLine(
                            $"Writer {id}: +1");

                        await Task.Delay(Random.Shared.Next(100, 300));
                    }
                });
            }

            await Task.WhenAll(tasks);

            Console.WriteLine();
            Console.WriteLine($"Final count = {StaticServer.GetCount()}");
        }
    }
}
