﻿namespace TrackTV.Data.Repositories.Contracts
{
    using System.Threading.Tasks;

    using TrackTv.Models;

    public interface IActorsRepository
    {
        Task<Actor[]> GetActorsByTheTvDbIdsAsync(int[] ids);
    }
}