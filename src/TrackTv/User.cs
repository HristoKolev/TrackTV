namespace TrackTv
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public virtual ICollection<Show> Shows { get; set; } = new List<Show>();

        public string Username { get; set; }
    }

    public class Show
    {
        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        public AirDay? AirDay { get; set; }

        public TimeSpan? AirTime { get; set; }

        public string Banner { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Episode> Episodes { get; set; } = new List<Episode>();

        public DateTime? FirstAired { get; set; }

        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

        public string ImdbId { get; set; }

        public string Language { get; set; }

        public long LastUpdated { get; set; }

        public string Name { get; set; }

        public virtual Network Network { get; set; }

        public string Poster { get; set; }

        public int TvDbId { get; set; }
    }

    public class Episode
    {
        public string Description { get; set; }

        public int Id { get; set; }

        public string ImdbId { get; set; }

        public long LastUpdated { get; set; }

        public int Number { get; set; }

        public string Title { get; set; }

        public int TvDbId { get; set; }
    }

    public class Network
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Genre
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Actor
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}