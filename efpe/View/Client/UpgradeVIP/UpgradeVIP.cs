using efpe.Controller;
using efpe.View.Client.User.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efpe.View.Client.UpgradeVIP
{
    public partial class UpgradeVIP : Form
    {
        UserController _userController = new UserController();

        public UpgradeVIP()
        {
            InitializeComponent();
            string username = GetCookieValue("Username");
            string email = GetCookieValue("Email");
            labelUsername.Text = username;
            labelEmail.Text = email;
            comboBoxMetodePembayaran.Items.AddRange(new[] { "Dana", "Ovo", "Gopay", "ShopeePay" });
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

        private void btnBayar_Click(object sender, EventArgs e)
        {
            string email = GetCookieValue("Email");
            string vipAtauReguler = "VIP";
            bool result = _userController.UpdateVipAtauRegulerByEmail(email, vipAtauReguler);

            if (result)
            {
                MessageBox.Show("Pembayaran berhasil, Selamat anda telah menjadi VIP", "Informasi", MessageBoxButtons.OK);
                this.Hide();
                View.Client.Beranda.Beranda form = new View.Client.Beranda.Beranda();
                form.Show();
            }
            else
            {
                MessageBox.Show("Pembayaran gagal", "Informasi", MessageBoxButtons.OK);
            }
        }
    }
}
