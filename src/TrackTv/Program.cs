namespace TrackTv
{
    using System.Threading.Tasks;

    public class Program
    {
        private static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        private static async Task MainAsync(string[] args) => await new DataEntryProgram().DoAsync().ConfigureAwait(false);
    }
}