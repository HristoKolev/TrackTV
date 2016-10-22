namespace TrackTv.Models.Joint
{
    public class ShowsGenres
    {
        public ShowsGenres(Genre genre)
        {
            this.Genre = genre;
        }

        public ShowsGenres()
        {
        }

        public Genre Genre { get; set; }

        public int GenreId { get; set; }

        public Show Show { get; set; }

        public int ShowId { get; set; }
    }
}