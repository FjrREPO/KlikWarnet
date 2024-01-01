using efpe.View.Admin.CRUD;
using efpe.View.Admin.CRUD.Pembayaran;
using efpe.View.Client.Pesan;
using efpe.View.Client.Promo;
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
            btnLogin.Cursor = Cursors.Hand;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login form = new Login();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            efpe.View.Admin.CRUD.Promo.Promo form = new efpe.View.Admin.CRUD.Promo.Promo();
            form.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            efpe.View.Client.Promo.Promo form = new efpe.View.Client.Promo.Promo();
            form.Show();
        }
    }
}
