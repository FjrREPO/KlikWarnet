using efpe.Controller;
using efpe.Model.Entity;
using efpe.View.Client.User.Login;
using System;
using System.Net;
using System.Windows.Forms;

namespace efpe.View.Client.Pembayaran
{
    public partial class Pembayaran : Form
    {
        private ItemEntity _selectedItem;
        CookieContainer cookies = ((Login)Application.OpenForms["Login"]).GetCookies();
        private Timer timer;

        public Pembayaran(ItemEntity selectedItem)
        {
            InitializeComponent();
            _selectedItem = selectedItem;
            comboBoxDurasi.Items.Add("1 Jam");
            comboBoxDurasi.Items.Add("2 Jam");
            comboBoxDurasi.Items.Add("3 Jam");
            comboBoxDurasi.Items.Add("4 Jam");
            comboBoxDurasi.Items.Add("5 Jam");
            comboBoxDurasi.Items.Add("6 Jam");

            comboBoxMetodePembayaran.Items.Add("Dana");
            comboBoxMetodePembayaran.Items.Add("Ovo");
            comboBoxMetodePembayaran.Items.Add("Gopay");
            comboBoxMetodePembayaran.Items.Add("ShopeePay");

            dateTimePickerWaktuMulai.MinDate = DateTime.Now;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;


            dateTimePickerWaktuMulai.ValueChanged += DateTimePickerWaktuMulai_ValueChanged;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            dateTimePickerWaktuMulai.ValueChanged -= DateTimePickerWaktuMulai_ValueChanged;
            dateTimePickerWaktuMulai.Value = DateTime.Now;
            dateTimePickerWaktuMulai.ValueChanged += DateTimePickerWaktuMulai_ValueChanged;
            UpdateEndTime();
        }

        private void DateTimePickerWaktuMulai_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePickerWaktuMulai.Value < DateTime.Now)
            {
                MessageBox.Show("Mohon pilih waktu setelah waktu pada saat ini.", "Invalid Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePickerWaktuMulai.Value = DateTime.Now;
            }
            UpdateEndTime();
        }

        private void UpdateEndTime()
        {
            DateTime waktuMulai = dateTimePickerWaktuMulai.Value;

            if (int.TryParse(DurasiKeValue(), out int durasi))
            {
                DateTime waktuSelesai = waktuMulai.AddHours(durasi);

                string formattedTime = $"{waktuSelesai.Hour:D2}:{waktuSelesai.Minute:D2}:{waktuSelesai.Second:D2}";

                labelWaktuSelesai.Text = formattedTime;
            }
            else
            {
                MessageBox.Show("Invalid duration input. Please select a valid duration from the list.");
            }
        }

        private string DurasiKeValue()
        {
            if (comboBoxDurasi.Text == "1 Jam")
            {
                return "1";
            }
            else if (comboBoxDurasi.Text == "2 Jam")
            {
                return "2";
            }
            else if (comboBoxDurasi.Text == "3 Jam")
            {
                return "3";
            }
            else if (comboBoxDurasi.Text == "4 Jam")
            {
                return "4";
            }
            else if (comboBoxDurasi.Text == "5 Jam")
            {
                return "5";
            }
            else if (comboBoxDurasi.Text == "6 Jam")
            {
                return "6";
            }
            else
            {
                return "0";
            }
        }

        private void Pembayaran_Load(object sender, EventArgs e)
        {
            string username = GetCookieValue(cookies, "Username");
            string email = GetCookieValue(cookies, "Email");
            labelUsername.Text = $"{username}";
            labelEmail.Text = $"{email}";
            labelNomorKomputer.Text = $"{_selectedItem.NomorKomputer}";
            labelVIPAtauReguler.Text = $"{_selectedItem.VipAtauReguler}";
        }

        private string GetCookieValue(CookieContainer cookies, string cookieName)
        {
            Uri uri = new Uri("http://localhost");

            CookieCollection cookieCollection = cookies.GetCookies(uri);

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
            timer.Stop();
        }
    }
}
