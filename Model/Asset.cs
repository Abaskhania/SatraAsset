using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatraAsset.Model
{
    public class Asset
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "شناسه")]
        public int Id { get; set; }

        [Required(ErrorMessage ="لطفا نام اموال را وارد نمایید")]
        [MaxLength(200)]
        [Display(Name = "نام اموال")]
        public string Name { get; set; } = string.Empty;

        
        [Required(ErrorMessage = "لطفا شماره اموال را وارد نمایید")]
        [MaxLength(100)]
        [Display(Name = "شماره اموال")]
        public string AssetCode { get; set; } = string.Empty;

        [MaxLength(500)]
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        [Display(Name = "دسته بندی")]
        public Category? Category { get; set; }
       
        [Display(Name = "کد دسته بندی")]
        [Required(ErrorMessage ="لطفا دسته بندی را انتخاب کنید")]
        public int CategoryID { get; set; }

        
        [Display(Name = "محل استقرار")]
        public ServiceLocation? Location { get; set; }

        [Required(ErrorMessage = "لطفا محل استقرار اموال را وارد نمایید")]
        [Display(Name = "کد محل استقرار")]
        public int LocationID { get; set; }

        [MaxLength(100)]
        [Display(Name = "تولیدکننده")]
        public string? Manufacturer { get; set; }

        [MaxLength(100)]
        [Display(Name = "مدل")]
        public string? Model { get; set; }

        [MaxLength(100)]
        [Display(Name = "شماره سریال")]
        public string? SerialNumber { get; set; }
        [Required]
        [Display(Name = "تاریخ خرید")]
        public string? PurchaseAt { get; set; }

        [Display(Name = "قیمت خرید")]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "تاریخ پایان گارانتی")]
        public string? WarrantyExpiryAt { get; set; }

        [Display(Name = "وضعیت")]
        public AssetStatus Status { get; set; } = AssetStatus.Active;

        [Display(Name = "تاریخ ایجاد")]
        public string? CreatedAt { get; set; }

        [Display(Name = "تاریخ آخرین ویرایش")]
        public string? UpdatedAt { get; set; }

        public ICollection<AssetRecipient> AssetPersonels { get; } = new List<AssetRecipient>();
        public ICollection<AssetPropertyValue> AssetProperties { get; set; } = new List<AssetPropertyValue>();
        //public ICollection<Personel> Personels { get; } = new List<Personel>();
    }
    public enum AssetStatus
    {
        [Display(Name = "فعال")]
        Active,

        [Display(Name = "غیرفعال")]
        Inactive,

        [Display(Name = "در حال تعمیر")]
        UnderMaintenance,

        [Display(Name = "اسقاطی")]
        Retired,

        [Display(Name = "مفقود شده")]
        Lost
    }
}
