namespace TrackTv.Services.Genres.Models
{
    public class FullGenre
    {
        public FullGenre(int genreId, string genreName)
        {
            this.GenreId = genreId;
            this.GenreName = genreName;
        }

        public FullGenre()
        {
        }

        public int GenreId { get; set; }

        public string GenreName { get; set; }
    }
}