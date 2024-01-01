using efpe.View.Client.User.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efpe.View.Admin.Beranda
{
    public partial class Beranda : Form
    {
        public Beranda()
        {
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.FlatAppearance.BorderSize = 0;
            SetRoundedButton(button1, 20);
            SetRoundedButton(button2, 20);
            SetRoundedButton(button3, 20);
        }
        private void SetRoundedButton(Button button, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(0, 0, radius, radius, 180, 90);
            path.AddArc(button.Width - radius, 0, radius, radius, 270, 90);
            path.AddArc(button.Width - radius, button.Height - radius, radius, radius, 0, 90);
            path.AddArc(0, button.Height - radius, radius, radius, 90, 90);
            path.CloseFigure();

            Region region = new Region(path);
            button.Region = region;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.CRUD.Item item = new View.Admin.CRUD.Item();
            item.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.CRUD.Pembayaran.LaporanPembayaran LPembayaran = new View.Admin.CRUD.Pembayaran.LaporanPembayaran();
            LPembayaran.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Admin.CRUD.Promo.Promo promo = new View.Admin.CRUD.Promo.Promo();
            promo.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Show();
        }
    }
}
