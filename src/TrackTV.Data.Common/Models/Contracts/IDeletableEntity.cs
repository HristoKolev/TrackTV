namespace TrackTV.Data.Common.Models.Contracts
{
    using System;

    public interface IDeletableEntity
    {
        DateTime? DeletedOn { get; set; }

        bool IsDeleted { get; set; }
    }
}