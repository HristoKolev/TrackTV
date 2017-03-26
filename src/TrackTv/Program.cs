namespace TrackTv
{
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        public async Task MainAsync(string[] args)
        {
            // await new ServicesProgram().DoAsync();
            await new DataEntryProgram().DoAsync().ConfigureAwait(false);
        }
    }
}