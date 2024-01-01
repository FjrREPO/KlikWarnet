using efpe.Controller;
using efpe.Model.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efpe.View.Admin.CRUD.Promo.AddPromo
{
    public partial class AddPromo : Form
    {
        PromoController _promoController = new PromoController();

        public AddPromo()
        {
            InitializeComponent();
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
            string kodePromo = txtKodePromo.Text;
            int durasi = DurasiValue();
            int diskon;
            DateTime expired = dateTimePickerExpired.Value;

            if (!int.TryParse(txtBoxDiskon.Text, out diskon) || diskon < 1 || diskon > 100)
            {
                MessageBox.Show("Diskon yang anda masukkan tidak valid, pilih dari 1% - 100%", "Informasi", MessageBoxButtons.OK);
                return;
            }

            PromoEntity newitem = new PromoEntity
            {
                KodePromo = kodePromo,
                Durasi = durasi,
                Diskon = diskon,
                Expired = expired
            };

            if (!string.IsNullOrEmpty(kodePromo) && durasi != 0 && expired != null)
            {
                try
                {
                    bool successAdd = _promoController.AddPromo(newitem);

                    if (successAdd) 
                    {
                        MessageBox.Show("Data berhasil ditambahkan!", "Informasi", MessageBoxButtons.OK);
                        this.Hide();
                        View.Admin.CRUD.Promo.Promo form = new View.Admin.CRUD.Promo.Promo();
                        form.Show();
                    }
                    else
                    {
                        MessageBox.Show("Data gagal ditambahkan!", "Informasi", MessageBoxButtons.OK);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error = {ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.CRUD.Promo.Promo form = new View.Admin.CRUD.Promo.Promo();
            form.Show();
        }
    }
}
