using NZ.HRM.Domain.Common;

namespace NZ.Shared.Domain.Common
{
    public abstract class BaseEntityWithSortOrder : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// BaseEntity.
        /// </summary>
        public BaseEntityWithSortOrder() : base()
        {
            this.SortOrder = 1000; // Default sort order
        }

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        public int SortOrder { get; set; }
    }
}
