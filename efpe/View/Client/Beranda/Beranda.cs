using efpe.Controller;
using efpe.Model.Entity;
using efpe.Model.Repository;
using efpe.Repository;
using efpe.View.Client.User.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace efpe.View.Client.Beranda
{
    public partial class Beranda : Form
    {

        private readonly UserController _promoController = new UserController();
        private readonly UserRepository _promoRepository = new UserRepository();
        private List<UserEntity> _allItems;
        private List<UserEntity> _filteredItems;

        public Beranda()
        {
            
            InitializeComponent();
            string username = GetCookieValue("Username");
            label1.Text = $"Selamat Datang {username}";
            button4.FlatStyle = FlatStyle.Flat;
            button4.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.FlatAppearance.BorderSize = 0;
            SetRoundedButton(button4, 40);
            SetRoundedButton(button2, 40);
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

        private void button1_Click(object sender, EventArgs e)
        {
            string username = GetCookieValue("Username");
            UserEntity userData = _promoRepository.GetData(username);

            if(userData.VipAtauReguler == "Reguler")
            {
                this.Hide();
                View.Client.UpgradeVIP.UpgradeVIP form = new View.Client.UpgradeVIP.UpgradeVIP();
                form.Show();
            }
            else
            {
                MessageBox.Show("Anda telah menjadi VIP :)", "Informasi", MessageBoxButtons.OK);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Client.Pesan.Pesan form = new View.Client.Pesan.Pesan();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            View.Client.Promo.Promo promo = new View.Client.Promo.Promo();
            promo.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
            Home home = new Home();
            home.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
