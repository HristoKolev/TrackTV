namespace TrackTv.Data.Models
{
    using System.Collections.Generic;

    public class Genre
    {
        public Genre(string genreName)
        {
            this.GenreName = genreName;
        }

        public Genre()
        {
        }

        public int GenreId { get; set; }

        public string GenreName { get; set; }

        public virtual ICollection<ShowsGenres> ShowsGenres { get; } = new List<ShowsGenres>();
    }
}