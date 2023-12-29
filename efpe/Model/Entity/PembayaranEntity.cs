using System;

namespace efpe.Model.Entity
{
    public class PembayaranEntity
    {
        string vipAtauRegular, metodePembayaran, kodePromo, harga, username, email;
        int id, durasi, nomorKomputer;
        DateTime waktuMulai, waktuSelesai;

        public PembayaranEntity() { }

        public PembayaranEntity(int id, string vipAtauRegular, string metodePembayaran, string kodePromo, string harga, int durasi, DateTime waktuMulai, DateTime waktuSelesai, int nomorKomputer, string username, string email)
        {
            this.id = id;
            this.vipAtauRegular = vipAtauRegular;
            this.metodePembayaran = metodePembayaran;
            this.kodePromo = kodePromo;
            this.harga = harga;
            this.durasi = durasi;
            this.waktuMulai = waktuMulai;
            this.waktuSelesai = waktuSelesai;
            this.nomorKomputer = nomorKomputer;
            this.username = username;
            this.email = email;
        }

        public int Id { get => id; set => id = value; }
        public string VipAtauRegular { get => vipAtauRegular; set => vipAtauRegular = value; }
        public string MetodePembayaran { get => metodePembayaran; set => metodePembayaran = value; }
        public string KodePromo { get => kodePromo; set => kodePromo = value; }
        public string Harga { get => harga; set => harga = value; }
        public int Durasi { get => durasi; set => durasi = value; }
        public DateTime WaktuMulai { get => waktuMulai; set => waktuMulai = value; }
        public DateTime WaktuSelesai { get => waktuSelesai; set => waktuSelesai = value; }
        public int NomorKomputer { get => nomorKomputer; set => nomorKomputer = value; }
        public string Username { get => username; set => username = value; }
        public string Email { get => email; set => email = value; }
    }
}