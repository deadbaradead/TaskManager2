using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.DataService.Database.DbModels
{
    public partial class webpages_OAuthMembership
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(30)]
        public string Provider { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(100)]
        public string ProviderUserId { get; set; }

        public int UserId { get; set; }
    }
}
