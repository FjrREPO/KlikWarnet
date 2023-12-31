using System;
using System.Net;
using System.Windows.Forms;
using efpe.Controller;
using efpe.Model.Entity;
using efpe.View.Client.User.Login;

namespace efpe.View.Client.Pembayaran
{
    public partial class Pembayaran : Form
    {
        private readonly ItemEntity _selectedItem;
        private readonly PembayaranController _pembayaranController;
        private readonly ItemController _itemController;
        private readonly Timer _timer;
        private double _originalPrice;
        private bool _isPromoCodeApplied = false;

        public Pembayaran(ItemEntity selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            _pembayaranController = new PembayaranController();
            _itemController = new ItemController();

            InitializeComboBoxes();
            InitializeDateTimePicker();

            _timer = new Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void InitializeComboBoxes()
        {
            comboBoxDurasi.Items.AddRange(new[] { "1 Jam", "2 Jam", "3 Jam", "4 Jam", "5 Jam", "6 Jam" });
            comboBoxMetodePembayaran.Items.AddRange(new[] { "Dana", "Ovo", "Gopay", "ShopeePay" });
        }

        private void InitializeDateTimePicker()
        {
            dateTimePickerWaktuMulai.MinDate = DateTime.Now;
            dateTimePickerWaktuMulai.ValueChanged += DateTimePickerWaktuMulai_ValueChanged;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dateTimePickerWaktuMulai.ValueChanged -= DateTimePickerWaktuMulai_ValueChanged;
            dateTimePickerWaktuMulai.Value = DateTime.Now;
            dateTimePickerWaktuMulai.ValueChanged += DateTimePickerWaktuMulai_ValueChanged;
        }

        private void DateTimePickerWaktuMulai_ValueChanged(object sender, EventArgs e)
        {
            UpdateEndTime();
        }

        private void UpdateEndTime()
        {
            DateTime waktuMulai = dateTimePickerWaktuMulai.Value;

            if (int.TryParse(DurasiKeValue(), out int durasi))
            {
                DateTime waktuSelesai = waktuMulai.AddHours(durasi);
                string formattedTime = $"{waktuSelesai:HH:mm:ss}";
                labelWaktuSelesai.Text = formattedTime;

                if (!_isPromoCodeApplied)
                {
                    UpdateHarga();
                }
            }
            else
            {
                MessageBox.Show("Invalid duration input. Please select a valid duration from the list.");
            }
        }

        private void UpdateHarga()
        {
            int sesuaiDurasi = Convert.ToInt32(DurasiKeValue());

            _originalPrice = _selectedItem.VipAtauReguler == "VIP" ? 6000 * sesuaiDurasi : 8000 * sesuaiDurasi;

            labelHarga.Text = _originalPrice.ToString();
        }

        private string DurasiKeValue()
        {
            switch (comboBoxDurasi.Text)
            {
                case "1 Jam": return "1";
                case "2 Jam": return "2";
                case "3 Jam": return "3";
                case "4 Jam": return "4";
                case "5 Jam": return "5";
                case "6 Jam": return "6";
                default: return "0";
            }
        }

        private void Pembayaran_Load(object sender, EventArgs e)
        {
            string username = GetCookieValue("Username");
            string email = GetCookieValue("Email");

            labelUsername.Text = username;
            labelEmail.Text = email;
            labelNomorKomputer.Text = _selectedItem.NomorKomputer.ToString();
            labelVIPAtauReguler.Text = _selectedItem.VipAtauReguler;
        }

        private string GetCookieValue(string cookieName)
        {
            Uri uri = new Uri("http://localhost");
            CookieCollection cookieCollection = ((Login)Application.OpenForms["Login"]).GetCookies().GetCookies(uri);

            foreach (Cookie cookie in cookieCollection)
            {
                if (cookie.Name == cookieName)
                {
                    return cookie.Value;
                }
            }

            return null;
        }

        private void comboBoxDurasi_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEndTime();
        }

        private void Pembayaran_FormClosed(object sender, FormClosedEventArgs e)
        {
            _timer.Stop();
        }

        private void btnPakaiKodePromo_Click(object sender, EventArgs e)
        {
            double discountedPrice = 0;

            string selectedDuration = comboBoxDurasi.Text;

            switch (textBoxKodePromo.Text)
            {
                case "warnet2jam" when selectedDuration == "2 Jam":
                    discountedPrice = _originalPrice * 90 / 100;
                    _isPromoCodeApplied = true;
                    break;

                case "warnet4jam" when selectedDuration == "4 Jam":
                    discountedPrice = _originalPrice * 80 / 100;
                    _isPromoCodeApplied = true;
                    break;

                case "warnet6jam" when selectedDuration == "6 Jam":
                    discountedPrice = _originalPrice * 70 / 100;
                    _isPromoCodeApplied = true;
                    break;

                default:
                    MessageBox.Show("Kode promo tidak valid atau kadaluwarsa", "Informasi", MessageBoxButtons.OK);
                    _isPromoCodeApplied = false;
                    return;
            }

            labelHarga.Text = discountedPrice.ToString();
        }

        private void btnBayar_Click(object sender, EventArgs e)
        {
            DateTime waktuPembayaran = DateTime.Now;
            string username = labelUsername.Text;
            string email = labelEmail.Text;
            int nomorKomputer = Convert.ToInt32(labelNomorKomputer.Text);
            string vipAtauReguler = labelVIPAtauReguler.Text;
            string metodePembayaran = comboBoxMetodePembayaran.Text;
            string kodePromo = textBoxKodePromo.Text;
            string harga = labelHarga.Text;
            int durasi = Convert.ToInt32(DurasiKeValue());
            DateTime waktuMulai = dateTimePickerWaktuMulai.Value;
            DateTime waktuSelesai = waktuMulai.AddHours(durasi);

            int digunakan = 1;

            if (string.IsNullOrEmpty(kodePromo))
            {
                kodePromo = string.Empty;
            }

            if (!string.IsNullOrEmpty(metodePembayaran) && waktuMulai != null && !string.IsNullOrEmpty(harga))
            {
                PembayaranEntity newItem = new PembayaranEntity
                {
                    WaktuPembayaran = waktuPembayaran,
                    Username = username,
                    Email = email,
                    NomorKomputer = nomorKomputer,
                    VipAtauRegular = vipAtauReguler,
                    MetodePembayaran = metodePembayaran,
                    KodePromo = kodePromo,
                    Harga = harga,
                    Durasi = durasi,
                    WaktuMulai = waktuMulai,
                    WaktuSelesai = waktuSelesai
                };

                bool successAdd = _pembayaranController.AddData(newItem);

                if (successAdd)
                {
                    MessageBox.Show("Pembayaran anda telah berhasil!", "Informasi", MessageBoxButtons.OK);
                    _itemController.UpdateDigunakan(nomorKomputer, digunakan);
                    this.Hide();
                    View.Client.Pesan.Pesan form = new View.Client.Pesan.Pesan();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Pembayaran anda gagal :(", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Masih ada data yang kosong!", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
