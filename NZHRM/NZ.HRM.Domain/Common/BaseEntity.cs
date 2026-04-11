using NZ.HRM.Domain.Helper;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NZ.HRM.Domain.Common
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseEntity"/> class.
        /// BaseEntity.
        /// </summary>
        public BaseEntity()
        {
            this.Id = IdentityGenerator.Next();
            //this.CreatedOn = DateTime.UtcNow;
            //this.UpdatedOn = DateTime.UtcNow;
            this.IsActive = true;

            // Initialize non-nullable properties with default values
            this.CreatedBy = string.Empty;
            this.UpdatedBy = string.Empty;
        }

        /// <summary>
        /// Gets or sets id.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Column("Id", TypeName = IdentityGenerator.IDDBTYPE)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets createdOn.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets createdBy.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets updatedOn.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets updatedBy.
        /// </summary>
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether isActive.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
