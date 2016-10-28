﻿namespace TrackTV.Data
{
    using Microsoft.EntityFrameworkCore;

    using TrackTv.Models;

    public interface ICoreDataContext
    {
        DbSet<Actor> Actors { get; }

        DbSet<Episode> Episodes { get; }

        DbSet<Genre> Genres { get; }

        DbSet<Network> Networks { get; }

        DbSet<Show> Shows { get; }
    }
}