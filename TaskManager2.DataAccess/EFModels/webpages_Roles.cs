namespace TaskManager2.DataAccess.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class webpages_Roles
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public webpages_Roles()
        {
            UserProfile = new HashSet<UserProfile>();
        }

        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(256)]
        public string RoleName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfile> UserProfile { get; set; }
    }
}
