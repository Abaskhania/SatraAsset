namespace SatraAsset.Model
{
    public class Personel
    {
        public int ID { get; set; }

        public int ClerkID { get; set; }
        public string NCD { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }

        public ServiceLocation Location { get; set; }

        public ICollection<AssetRecipient> AssetPersonels { get; } = new List<AssetRecipient>();
        //public ICollection<Asset> Assets { get; } = new List<Asset>();

    }
}
