namespace TrackTV.Logic
{
    public interface IAppSettings
    {
        string ApiKey { get; }

        string BannerDirectory { get; }

        string BannerPath { get; }

        string Name { get; }

        string PosterDirectory { get; }

        string PosterPath { get; }
    }
}