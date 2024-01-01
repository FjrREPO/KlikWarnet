using efpe.Controller;
using System;
using System.Net;
using System.Windows.Forms;
using efpe.View.Admin.CRUD;

namespace efpe.View.Client.User.Login
{
    public partial class Login : Form
    {
        UserController _userController;
        private CookieContainer cookies;

        public Login()
        {
            InitializeComponent();
            cookies = new CookieContainer();
            _userController = new UserController();
            txtUsernameOrEmail.KeyDown += TxtUsernameOrEmail_KeyDown;
            txtPassword.KeyDown += TxtPassword_KeyDown;
            labelDaftar.Cursor = Cursors.Hand;
            btnLogin.Cursor = Cursors.Hand;
            pictureBox14.Cursor = Cursors.Hand;
        }

        private void TxtUsernameOrEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.SelectNextControl((Control)sender, true, true, true, true);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                btnLogin_Click(sender, e);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameOrEmail = txtUsernameOrEmail.Text;
            string password = txtPassword.Text;

            bool loginResult = _userController.AuthenticateUser(usernameOrEmail, password);

            if (loginResult)
            {
                MessageBox.Show("Login successful!");
                Tuple<string, string> userCredentials = _userController.GetUsernameAndEmail(usernameOrEmail);
                SaveToCookies("Username", userCredentials.Item1);
                SaveToCookies("Email", userCredentials.Item2);

                if (usernameOrEmail == "admin" && password == "admin")
                {
                    this.Hide();
                    View.Admin.Beranda.Beranda form = new View.Admin.Beranda.Beranda();
                    form.Show();
                }
                else
                {
                    this.Hide();
                    View.Client.Beranda.Beranda form = new View.Client.Beranda.Beranda();
                    form.Show();
                }
            }
            else
            {
                MessageBox.Show("Pastikan username atau email dan password anda valid!", "Information", MessageBoxButtons.OK);
            }
        }

        private void SaveToCookies(string key, string value)
        {
            Cookie cookie = new Cookie(key, value);

            cookie.Domain = "localhost";
            cookie.Path = "/";

            cookie.Expires = DateTime.Now.AddDays(1);

            cookies.Add(new Uri("http://localhost"), cookie);
        }

        public CookieContainer GetCookies()
        {
            return cookies;
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home form = new Home();
            form.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            User.Register.Register form = new User.Register.Register();
            form.Show();
        }
    }
}
