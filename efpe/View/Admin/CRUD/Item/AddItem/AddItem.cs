using efpe.Model.Entity;
using System;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing;
using efpe.Controller;

namespace efpe.View.Admin.CRUD.AddItem
{
    public partial class AddItem : Form
    {
        private ItemController _itemController;
        private byte[] imageBytes;

        public AddItem()
        {
            InitializeComponent();
            _itemController = new ItemController();
            SetRoundedPanel(panel2, 20);
            SetRoundedPanel(panelUploadImage, 20);
            btnTambah.FlatStyle = FlatStyle.Flat;
            btnTambah.FlatAppearance.BorderSize = 0;
            groupBox1.Paint += (sender, e) => e.Graphics.Clear(this.BackColor);
            comboBoxVipAtauReguler.Items.Add("VIP");
            comboBoxVipAtauReguler.Items.Add("Reguler");
        }

        private void SetRoundedPanel(Panel panel, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(panel.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(panel.Width - radius, panel.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, panel.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            panel.Region = new Region(path);
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelUploadImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog1.Title = "Select an Image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.ImageLocation = openFileDialog1.FileName;

                string imagePath = openFileDialog1.FileName;
                imageBytes = File.ReadAllBytes(imagePath);
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            int itemNomorKomputer = Convert.ToInt32(txtNomorKomputer.Text);

            if (string.IsNullOrEmpty(itemNomorKomputer.ToString()))
            {
                MessageBox.Show("Masih ada data yang kosong.");
                return;
            }

            ItemEntity newItem = new ItemEntity
            {
                NomorKomputer = itemNomorKomputer,
                Digunakan = false,
                Image = imageBytes
            };

            if (comboBoxVipAtauReguler.Text == "VIP")
            {
                newItem.VipAtauReguler = "VIP";
            }
            else if (comboBoxVipAtauReguler.Text == "Reguler")
            {
                newItem.VipAtauReguler = "Reguler";
            }
            else
            {
                MessageBox.Show("Pilih jenis VIP atau Reguler.");
                return;
            }

            bool success = _itemController.AddItem(newItem);

            if (success)
            {
                MessageBox.Show("Item berhasil ditambahkan!");
                this.Close();
            }
            else
            {
                MessageBox.Show("Item gagal ditambahkan.");
            }
        }

        private void pictureBoxImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files (*.jpg; *.jpeg; *.png; *.gif)|*.jpg; *.jpeg; *.png; *.gif";
            openFileDialog1.Title = "Select an Image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBoxImage.ImageLocation = openFileDialog1.FileName;

                string imagePath = openFileDialog1.FileName;
                imageBytes = File.ReadAllBytes(imagePath);
            }
        }
    }
}
