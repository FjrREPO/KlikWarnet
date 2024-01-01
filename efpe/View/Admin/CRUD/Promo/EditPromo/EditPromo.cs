using efpe.Controller;
using efpe.Model.Entity;
using System;
using System.Windows.Forms;

namespace efpe.View.Admin.CRUD.Promo.EditPromo
{
    public partial class EditPromo : Form
    {
        private PromoEntity selectedItem;
        private readonly PromoController _promoController = new PromoController();

        public EditPromo(PromoEntity selectedItem)
        {
            InitializeComponent();
            this.selectedItem = selectedItem;
            txtKodePromo.Text = selectedItem.KodePromo.ToString();
            comboBoxDurasi.Text = selectedItem.Durasi.ToString();
            txtBoxDiskon.Text = selectedItem.Diskon.ToString();
            dateTimePickerExpired.Value = selectedItem.Expired;

            comboBoxDurasi.Items.Add("1 Jam");
            comboBoxDurasi.Items.Add("2 Jam");
            comboBoxDurasi.Items.Add("3 Jam");
            comboBoxDurasi.Items.Add("4 Jam");
            comboBoxDurasi.Items.Add("5 Jam");
            comboBoxDurasi.Items.Add("6 Jam");
        }

        private int DurasiValue()
        {
            if (comboBoxDurasi.Text == "1 Jam")
            {
                return 1;
            }
            else if (comboBoxDurasi.Text == "2 Jam")
            {
                return 2;
            }
            else if (comboBoxDurasi.Text == "3 Jam")
            {
                return 3;
            }
            else if (comboBoxDurasi.Text == "4 Jam")
            {
                return 4;
            }
            else if (comboBoxDurasi.Text == "5 Jam")
            {
                return 5;
            }
            else if (comboBoxDurasi.Text == "6 Jam")
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            int diskon;

            if (!int.TryParse(txtBoxDiskon.Text, out diskon) || diskon < 1 || diskon > 100)
            {
                MessageBox.Show("Diskon yang anda masukkan tidak valid, pilih dari 1% - 100%", "Informasi", MessageBoxButtons.OK);
                return;
            }

            selectedItem.KodePromo = txtKodePromo.Text;
            selectedItem.Durasi = DurasiValue();
            selectedItem.Diskon = diskon;
            selectedItem.Expired = dateTimePickerExpired.Value;

            bool successEdit = _promoController.EditPromo(selectedItem);

            if (successEdit)
            {
                MessageBox.Show("Data berhasil di update!", "Informasi", MessageBoxButtons.OK);
                this.Hide();
                View.Admin.CRUD.Promo.Promo form = new View.Admin.CRUD.Promo.Promo();
                form.Show();
            }
            else
            {
                MessageBox.Show("Data gagal di update!", "Informasi", MessageBoxButtons.OK);
            }
        }
    }
}
