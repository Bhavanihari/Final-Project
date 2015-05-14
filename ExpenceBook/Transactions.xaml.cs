using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Configuration;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;

using ExpenceBook.Properties;
using MicroMVVM.Expence.Daily;

namespace ExpenceBook
{
    /// <summary>
    /// Interaction logic for DailySpending.xaml
    /// </summary>
    public partial class DailySpending : UserControl
    {
        public DailySpending()
        {
            InitializeComponent();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string expenceDate = string.Empty;

            string expenceFor = string.Empty;

            decimal amountSpend = 0;

            string creditedBy = string.Empty;

            string payee = string.Empty;

           
            try
            {
                if (calEnpenceDate.Text.Length == 0)
                {
                    errormessage.Text = "Enter Expence Date";

                    calEnpenceDate.Focus();
                }
                else if (textBoxExpenceFor.Text.Length == 0 || !Regex.IsMatch(textBoxExpenceFor.Text, @"^[a-zA-Z ]*$"))
                {
                    errormessage.Text = "Enter Expence For";

                    textBoxExpenceFor.Focus();
                }
                else if (textAmountSpend.Text.Length == 0 || !Regex.IsMatch(textAmountSpend.Text, @"^[0-9]*$"))
                {
                    errormessage.Text = "Enter Amount Spend";

                    textAmountSpend.Focus();
                }
                else if (ComboBox1.Text.Length == 0 )
                {
                    errormessage.Text = "Enter Spend By";

                    ComboBox1.Focus();
                }

                else if (textPayee.Text.Length == 0 || !Regex.IsMatch(textPayee.Text, @"^[a-zA-Z ]*$"))
                {
                    errormessage.Text = "Enter Payee";

                    textPayee.Focus();
                }

                else
                {
                    expenceDate = calEnpenceDate.Text;

                    expenceFor = textBoxExpenceFor.Text;

                    amountSpend = Convert.ToDecimal(textAmountSpend.Text);

                    creditedBy = ComboBox1.Text;

                    payee = textPayee.Text; 

                    DailyExpence dailyExpence = new DailyExpence();

                    dailyExpence.ExpenceDate = expenceDate;

                    dailyExpence.ExpenceFor = expenceFor;

                    dailyExpence.AmountSpend = amountSpend;

                    dailyExpence.SpendBy = creditedBy;

                    dailyExpence.Payee = payee;

                    Settings setting = new Settings();

                    Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    dailyExpence.LoginId = Convert.ToInt32(configuration.AppSettings.Settings["loginUserId"].Value);

                    int returnVal = dailyExpence.Insert();

                    if (returnVal > 0)
                    {
                        MessageBox.Show("Record Added Successfully", "Success");

                        Reset();
                    }
                }
            }
            finally
            { }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

       
        public void Reset()
        {
            calEnpenceDate.Text = "";

            textBoxExpenceFor.Text = "";

            textAmountSpend.Text = "";

            ComboBox1.Text = "";
        }

    }
}
