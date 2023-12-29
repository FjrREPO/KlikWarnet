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
                    Item form = new Item();
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
    }
}
