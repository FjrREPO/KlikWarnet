using System;
using System.Windows.Forms;
using efpe.Controller;
using efpe.View.Admin.CRUD.User.Login;

namespace efpe.View.Client.User.Register
{
    public partial class Register : Form
    {
        private readonly UserController _userController;

        public Register()
        {
            InitializeComponent();
            _userController = new UserController();
        }

        private void btnDaftar_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtKonfirmasiPassword.Text;
            string vipAtauReguler = "Reguler";

            if (password.Equals(confirmPassword))
            {
                bool registrationResult = _userController.RegisterUser(username, email, password, vipAtauReguler);

                if (registrationResult)
                {
                    MessageBox.Show("Registration successful!");
                    this.Hide();
                    View.Client.User.Login.Login form = new View.Client.User.Login.Login();
                    form.Show();
                }
                else
                {
                    MessageBox.Show("Registration failed. Please try again.");
                }
            }
            else
            {
                MessageBox.Show("Password and Confirm Password do not match. Please check and try again.");
            }
        }
    }
}
