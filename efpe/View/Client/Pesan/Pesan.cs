using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using efpe.Controller;
using efpe.Model.Entity;
using efpe.View.Client.User.Login;

namespace efpe.View.Client.Pesan
{
    public partial class Pesan : Form
    {
        private readonly ItemController _itemController = new ItemController();
        private readonly PembayaranController _pembayaranController = new PembayaranController();
        private List<ItemEntity> _allItems;
        private List<ItemEntity> _filteredItems;
        UserController _userController;
        CookieContainer cookies = ((Login)Application.OpenForms["Login"]).GetCookies();

        public Pesan()
        {
            InitializeComponent();
            LoadDataAndCreatePanels();
            labelReguler_Click(null, EventArgs.Empty);
            _userController = new UserController();
        }

        private void LoadDataAndCreatePanels()
        {
            _allItems = _itemController.GetItems();
            _filteredItems = new List<ItemEntity>(_allItems);

            if (_allItems != null && _allItems.Count > 0)
            {
                foreach (ItemEntity item in _allItems)
                {
                    Panel newPanel = CreatePanel(item);
                    flowLayoutVIP.Controls.Add(newPanel);
                }
                panelReguler.BackColor = Color.FromArgb(0, 192, 192);
                panelVIP.BackColor = Color.Transparent;
                AdjustFlowLayoutPanelHeight();
            }
            else
            {
                MessageBox.Show("No data found.");
            }
        }

        private void AdjustFlowLayoutPanelHeight()
        {
            int totalHeight = flowLayoutVIP.Controls.Cast<Control>().Sum(control => control.Height + flowLayoutVIP.Padding.Vertical);
            flowLayoutVIP.Height = totalHeight;
        }

        private Panel CreatePanel(ItemEntity item)
        {
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = new System.Drawing.Size(150, 250);

            Label label1 = new Label();
            label1.Text = $"Komputer {item.NomorKomputer}";
            label1.Location = new System.Drawing.Point(10, 10);

            PictureBox picture = new PictureBox();
            using (MemoryStream stream = new MemoryStream(item.Image))
            {
                picture.Image = new Bitmap(stream);
            }

            picture.SizeMode = PictureBoxSizeMode.StretchImage;
            picture.Size = new System.Drawing.Size(80, 80);
            picture.Location = new System.Drawing.Point(10, 30);

            Label labelDigunakan = new Label();
            labelDigunakan.Location = new System.Drawing.Point(10, 140);
            UpdateLabelDigunakan(labelDigunakan, item.NomorKomputer);

            Button buttonPesan = new Button();
            buttonPesan.Text = "Pesan";
            buttonPesan.Location = new System.Drawing.Point(10, 200);

            buttonPesan.Tag = item;
            buttonPesan.Click += buttonPesan_Click;

            panel.Controls.Add(label1);
            panel.Controls.Add(picture);
            panel.Controls.Add(labelDigunakan);
            panel.Controls.Add(buttonPesan);

            return panel;
        }

        private void UpdateLabelDigunakan(Label labelDigunakan, int nomorKomputer)
        {
            DateTime waktuSelesai = _pembayaranController.GetWaktuSelesai(nomorKomputer);
            DateTime currentDateTime = DateTime.Now;

            if (waktuSelesai != default(DateTime) && waktuSelesai >= currentDateTime)
            {
                int digunakan = 1;
                _itemController.UpdateDigunakan(nomorKomputer, digunakan);
                labelDigunakan.Text = "PC sedang\nDigunakan";
            }
            else
            {
                int digunakan = 0;
                _itemController.UpdateDigunakan(nomorKomputer, digunakan);
                labelDigunakan.Text = "PC dapat\nDipesan";
            }
        }


        private void buttonPesan_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ItemEntity selectedItem = (ItemEntity)clickedButton.Tag;
            if (selectedItem.Digunakan == 0)
            {
                DialogResult result = MessageBox.Show($"Anda akan memesan PC {selectedItem.NomorKomputer}, Tekan OK untuk lanjut!", "Informasi", MessageBoxButtons.OKCancel);
                if (result == DialogResult.OK)
                { 
                    this.Hide();
                    View.Client.Pembayaran.Pembayaran form = new View.Client.Pembayaran.Pembayaran(selectedItem);
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("PC ini sedang digunakan, Mohon pilih PC lainnya", "Informasi", MessageBoxButtons.OK);
            }
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

        private void labelVIP_Click(object sender, EventArgs e)
        {

            string email = GetCookieValue(cookies, "Email");

            if (_userController.GetVipAtauReguler(email) == "VIP")
            {
                _filteredItems = _allItems.Where(item => item.VipAtauReguler == "VIP").ToList();
                UpdatePanels();
                panelReguler.BackColor = Color.Transparent;
                panelVIP.BackColor = Color.FromArgb(0, 192, 192);
            }
            else
            {
                MessageBox.Show("Anda harus menjadi VIP untuk membuka fitur ini.", "Informasi", MessageBoxButtons.OK);
            }
        }

        private void labelReguler_Click(object sender, EventArgs e)
        {
            _filteredItems = _allItems.Where(item => item.VipAtauReguler == "Reguler").ToList();
            UpdatePanels();
            panelReguler.BackColor = Color.FromArgb(0, 192, 192);
            panelVIP.BackColor = Color.Transparent;
        }

        private void UpdatePanels()
        {
            flowLayoutVIP.Controls.Clear();

            foreach (ItemEntity item in _filteredItems)
            {
                Panel newPanel = CreatePanel(item);
                flowLayoutVIP.Controls.Add(newPanel);
            }

            AdjustFlowLayoutPanelHeight();
        }
    }
}
