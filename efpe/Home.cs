using efpe.View.Admin.CRUD;
using efpe.View.Admin.CRUD.Pembayaran;
using efpe.View.Client.Pesan;
using efpe.View.Client.User.Login;
using efpe.View.Client.User.Register;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efpe
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Item form = new Item();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register form = new Register();
            form.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Pesan form = new Pesan();
            form.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            LaporanPembayaran form = new LaporanPembayaran();
            form.Show();
        }
    }
}
