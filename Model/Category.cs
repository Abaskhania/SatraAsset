using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatraAsset.Model
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        [Display(Name = "نام دسته بندی")]
        public string Name { get; set; } = string.Empty;

        public Category? Parent { get; set; }
        public int? ParentID { get; set; }

        public ICollection<AssetProperty> AssetProperties { get; set; }

    }
}
