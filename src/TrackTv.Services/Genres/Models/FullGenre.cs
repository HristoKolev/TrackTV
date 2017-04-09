namespace TrackTv.Services.Genres.Models
{
    public class FullGenre
    {
        public FullGenre(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public FullGenre()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}