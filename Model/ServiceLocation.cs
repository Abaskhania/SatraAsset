using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatraAsset.Model
{
    public class ServiceLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "نام محل استقرار")]
        public string Name { get; set; } = string.Empty;

        public ServiceLocation Parent { get; set; }
        public int? ParentID { get; set; }
       
    }
}
