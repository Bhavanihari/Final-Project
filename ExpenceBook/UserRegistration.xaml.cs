using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

using MicroMVVM.User.Registration;

namespace ExpenceBook
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserRegistration : Window
    {
        public UserRegistration()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string name = string.Empty;

            string email = string.Empty;

            string password = string.Empty;

            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";

                textBoxEmail.Focus();
            }

            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";

                textBoxEmail.Select(0, textBoxEmail.Text.Length);

                textBoxEmail.Focus();
            }

            else
            {
                name = textBoxFirstName.Text + " " + textBoxLastName.Text;

                email = textBoxEmail.Text;

                password = passwordBox1.Password;

                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";

                    passwordBox1.Focus();
                }

                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Enter Confirm password.";

                    passwordBoxConfirm.Focus();
                }

                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Confirm password must be same as password.";

                    passwordBoxConfirm.Focus();
                }
                else
                {
                    UserRegistrationMgr registration = new UserRegistrationMgr();

                    registration.Name = name;

                    registration.Email = email;

                    registration.Password = password;

                    registration.RegisterNew();

                    Reset();

                    Login login = new Login();

                    login.Show();

                    Close();
                }
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();

            login.Show();

            Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();

            login.Show();

            Close();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";

            textBoxLastName.Text = "";

            textBoxEmail.Text = "";

            passwordBox1.Password = "";

            passwordBoxConfirm.Password = "";
        }
    }
}
