namespace Code_Home_Work_3_6
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Logger log = new Logger();

            var start1 = new Starter(log);
            var start2 = new Starter(log);

            await Task.WhenAll(start1.Start(), start2.Start());
        }
    }
}