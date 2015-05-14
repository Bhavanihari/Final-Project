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
using System.Windows.Navigation;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

using MicroMVVM.Expence.Daily;
using System.Configuration;

namespace ExpenceBook
{
    /// <summary>
    /// Interaction logic for DailyExpenceList.xaml
    /// </summary>
   
    public partial class DailyExpenceList : UserControl
    {
        public DailyExpenceList()
        {
            InitializeComponent();

            FillExpenceList();
        }

        private void FillExpenceList()
        {
            List<DailyExpence> lstDailyExpence = new List<DailyExpence>();

            string loginId = ConfigurationManager.AppSettings["loginUserId"];

            lstDailyExpence = DailyExpence.FillExpenceList(loginId);

            lvDailyExpence.DataContext = lstDailyExpence;
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            string recordId = txtRecordId.Text.Trim();

            DailyExpence dailyExpence = new DailyExpence();

            dailyExpence.ExpenceId = recordId;

            dailyExpence.Delete();

            FillExpenceList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string transactionType = ComboBox.Text;

            List<DailyExpence> lstDailyExpence = new List<DailyExpence>();

            string loginId = ConfigurationManager.AppSettings["loginUserId"];

            lstDailyExpence = DailyExpence.FillExp(loginId, transactionType);

            lvDailyExpence1.DataContext = lstDailyExpence;
        }

     }
}
