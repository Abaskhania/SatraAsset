using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SatraAsset.Model
{
    public class AssetPropertyValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Value { get; set; }

        public int AssetPropertyId { get; set; }
        public AssetProperty AssetProperty { get; set; }

        public int AssetId { get; set; }
        public Asset Asset { get; set; }


    }
}
