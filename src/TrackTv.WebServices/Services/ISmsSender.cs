namespace TrackTv.WebServices.Services
{
    using System.Threading.Tasks;

    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}