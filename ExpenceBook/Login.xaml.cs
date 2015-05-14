using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Configuration;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using MicroMVVM.User.Login;


namespace ExpenceBook
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        UserRegistration registration = new UserRegistration();

        mdiWindow masterWindow = new mdiWindow();

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string username;

            string pwd;

            int LoginStatus = 0;

            username = textBoxEmail.Text;

            pwd = passwordBox1.Password;

            LoginMgr login = new LoginMgr();

            LoginStatus = login.AuthenticateLogin(username, pwd);

            if (LoginStatus > 0)
            {
                Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                configuration.AppSettings.Settings["loginUserId"].Value = LoginStatus.ToString();
                configuration.Save();

                ConfigurationManager.RefreshSection("appSettings");

                this.Close();

                masterWindow.Show();
            }
            else
            {
                MessageBox.Show("Please check user name or password");
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();

            Close();
        }
    }
}
