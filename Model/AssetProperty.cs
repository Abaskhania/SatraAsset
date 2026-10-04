using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SatraAsset.Model
{
    public class AssetProperty
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Display(Name ="نام خصوصیت")]
        public string Name { get; set; }
        [Display(Name = "نوع مقدار")]
        public PropertyDataType DataType { get; set; }
        [Display(Name = "اجباری")]
        public bool IsRequired { get; set; }
        [Display(Name = "کد دسته بندی")]
        public int CategoryId { get; set; }
        [Display(Name = "دسته بندی")]
        public Category? Category { get; set; }
        [Display(Name = "ترتیب")]
        public int SortOrder { get; set; }
        [Display(Name = "گزینه ها ")]
        public string? OptionsJson { get; set; }
       
        public ICollection<AssetPropertyValue> Values { get; set; }
        = new List<AssetPropertyValue>();

        public List<string> GetOptions()
        {
            if (string.IsNullOrWhiteSpace(this.OptionsJson))
                return new List<string>();

            return JsonSerializer.Deserialize<List<string>>(this.OptionsJson)
                   ?? new List<string>();
        }

    }

    public enum PropertyDataType
    {
        [Display(Name = "رشته ای")]
        String,
        [Display(Name = "عددی")]
        Number,
        [Display(Name = "تاریخ")]
        Date,
        [Display(Name = "منطقی")]
        Boolean,
        [Display(Name = "گزینه ای")]
        Select

    }
}
