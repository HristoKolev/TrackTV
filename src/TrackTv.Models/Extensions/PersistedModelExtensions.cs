namespace TrackTv.Models.Extensions
{
    public static class PersistedModelExtensions
    {
        public static bool IsPersisted(this IPersistedModel model)
        {
            return model.Id != default(int);
        }
    }
}