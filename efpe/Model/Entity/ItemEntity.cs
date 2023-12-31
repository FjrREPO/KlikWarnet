namespace efpe.Model.Entity
{
    public class ItemEntity
    {
        int nomor, id, nomorKomputer, digunakan;
        string vipAtauReguler;
        byte[] image;

        public ItemEntity() { }

        public ItemEntity(int nomor, int id, int nomorKomputer, string vipAtauReguler, int digunakan, byte[] image)
        {
            this.nomor = nomor;
            this.id = id;
            this.nomorKomputer = nomorKomputer;
            this.vipAtauReguler = vipAtauReguler;
            this.digunakan = digunakan;
            this.image = image;
        }

        public int Nomor { get => nomor; set => nomor = value; }
        public int Id { get => id; set => id = value; }
        public int NomorKomputer { get => nomorKomputer; set => nomorKomputer = value; }
        public string VipAtauReguler { get => vipAtauReguler; set => vipAtauReguler = value; }
        public int Digunakan { get => digunakan; set => digunakan = value; }
        public byte[] Image { get => image; set => image = value; }
    }
}
