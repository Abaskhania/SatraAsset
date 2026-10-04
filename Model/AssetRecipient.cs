using Azure;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System.ComponentModel.DataAnnotations;

namespace SatraAsset.Model
{
    public class AssetRecipient
    {
        [Key]
        public int Id { get; set; }
        public int AssetId { get; set; }
        public int PersonelId { get; set; }
        public Asset Asset { get; set; } 
        public Personel Personel { get; set; }

        public bool IsVerif { get; set; } = false;
        public string VerifDate { get; set; } = "";
        public string VerifUser { get; set; } = "";
        public string CreateAt { get; set; }
        public string CreateUser { get; set; }
        public string ReturnAt { get; set; }


    }
}
