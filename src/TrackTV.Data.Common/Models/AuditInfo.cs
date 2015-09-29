namespace TrackTV.Data.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using TrackTV.Data.Common.Models.Contracts;

    public abstract class AuditInfo : IAuditInfo, IDeletableEntity
    {
        public AuditInfo()
        {
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Specifies whether or not the CreatedOn property should be automatically set.
        /// </summary>
        [NotMapped]
        public bool PreserveCreatedOn { get; set; }

        public DateTime? DeletedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}