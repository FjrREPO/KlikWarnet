using System;

namespace efpe.Model.Entity
{
    public class PromoEntity
    {
        int id, durasi, nomor, diskon;
        string kodePromo;
        DateTime expired;

        public PromoEntity() { }

        public PromoEntity(int nomor, int id, int durasi, int diskon, string kodePromo, DateTime expired)
        {
            this.nomor = nomor;
            this.id = id;
            this.durasi = durasi;
            this.diskon = diskon;
            this.kodePromo = kodePromo;
            this.expired = expired;
        }

        public int Nomor { get => nomor; set => nomor = value; }
        public int Id { get => id; set => id = value; }
        public int Durasi { get => durasi; set => durasi = value; }
        public int Diskon { get => diskon; set => diskon = value; }
        public string KodePromo { get => kodePromo; set => kodePromo = value; }
        public DateTime Expired { get => expired; set => expired = value; }
    }
}
