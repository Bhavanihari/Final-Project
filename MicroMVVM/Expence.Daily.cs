using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MicroMVVM.Expence.Daily
{
    public class DailyExpence
    {
        #region Member Variable

        private string l_ExpenceId = string.Empty;

        private string l_ExpenceDate = string.Empty;

        private string l_ExpenceFor = string.Empty;

        private decimal l_AmountSpend = 0;

        private string l_SpendBy = string.Empty;

        private string l_Payee = string.Empty;

        private int l_LoginId = 0;
        private string p1;
        private decimal p2;
        private string p3;

        #endregion

        #region Properties

        public string ExpenceId
        {
            get { return l_ExpenceId; }
            set { l_ExpenceId = value; }
        }

        public string ExpenceDate
        {
            get { return l_ExpenceDate; }
            set { l_ExpenceDate = value; }
        }

        public string ExpenceFor
        {
            get { return l_ExpenceFor; }
            set { l_ExpenceFor = value; }
        }

        public decimal AmountSpend
        {
            get { return l_AmountSpend; }
            set { l_AmountSpend = value; }
        }

        public string SpendBy
        {
            get { return l_SpendBy; }
            set { l_SpendBy = value; }
        }

        public string Payee
        {
            get { return l_Payee; }
            set { l_Payee = value; }
        }

        public int LoginId
        {
            get { return l_LoginId; }
            set { l_LoginId = value; }
        }

        #endregion

        public DailyExpence()
        { }

        
        public DailyExpence(string p1, string p3)
        {
            
            this.ExpenceDate = p1;

            this.AmountSpend = Convert.ToDecimal(p3);
        }

        public int Insert()
        {
            try
            {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

                XDocument document = XDocument.Load(appPath + @"\DailyExpence.xml");

                int count = document.Descendants("expenceid").Count();

                int expenceId = count + 1;

                string filePath = ((Directory.GetCurrentDirectory() + @"\DailyExpence.xml"));

                var newElement = new XElement("expence",
                         new XElement("expenceid", expenceId),
                         new XElement("userid", l_LoginId),
                         new XElement("ExpenceDate", l_ExpenceDate),
                         new XElement("Payee", l_Payee),
                         new XElement("ExpenceFor", l_ExpenceFor),
                         new XElement("AmountSpend", l_AmountSpend),
                         new XElement("SpendBy", l_SpendBy),
                         new XElement("IsDeleted", 0));

                document.Element("dailyExpence").Add(newElement);

                document.Save(filePath);

                return expenceId;
            }
            finally
            { }
        }

        public static List<DailyExpence> FillExpenceList(string loginId)
        {
            List<DailyExpence> lstdailyExpence = new List<DailyExpence>();

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\DailyExpence.xml");

            string filePath = ((Directory.GetCurrentDirectory() + @"\DailyExpence.xml"));

            var custs = from c in document.Descendants("expence") where 
                            (string)c.Element("IsDeleted") == "0" &&
                            (string)c.Element("userid") == loginId
                        select c;

            foreach (var vDailyExpence in custs)
            {
                DailyExpence dailyExpence = new DailyExpence
                {
                    ExpenceId = vDailyExpence.Element("expenceid").Value,

                    ExpenceDate = vDailyExpence.Element("ExpenceDate").Value,

                    ExpenceFor = vDailyExpence.Element("ExpenceFor").Value,

                    Payee = vDailyExpence.Element("Payee").Value,

                    AmountSpend = Convert.ToDecimal(vDailyExpence.Element("AmountSpend").Value),

                    SpendBy = vDailyExpence.Element("SpendBy").Value
                };

                lstdailyExpence.Add(dailyExpence);
            }

            return lstdailyExpence;
        }

        public static List<DailyExpence> FillExp(string loginId, string transactionType)
        {
            List<DailyExpence> lstdailyExpence = new List<DailyExpence>();

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\DailyExpence.xml");

            string filePath = ((Directory.GetCurrentDirectory() + @"\DailyExpence.xml"));

            var custs = from c in document.Descendants("expence")
                        where
                            (string)c.Element("IsDeleted") == "0" &&
                            (string)c.Element("userid") == loginId && (string)c.Element("SpendBy") == transactionType
                        select c;

            foreach (var vDailyExpence in custs)
            {
                DailyExpence dailyExpence = new DailyExpence
                {
                    ExpenceId = vDailyExpence.Element("expenceid").Value,

                    ExpenceDate = vDailyExpence.Element("ExpenceDate").Value,

                    ExpenceFor = vDailyExpence.Element("ExpenceFor").Value,

                    Payee = vDailyExpence.Element("Payee").Value,

                    AmountSpend = Convert.ToDecimal(vDailyExpence.Element("AmountSpend").Value),

                    SpendBy = vDailyExpence.Element("SpendBy").Value
                };

                lstdailyExpence.Add(dailyExpence);
            }

            return lstdailyExpence;
        }


        public void Delete()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\DailyExpence.xml");

            string filePath = ((Directory.GetCurrentDirectory() + @"\DailyExpence.xml"));

            var result = from element in document.Descendants("expence")
                         where (int)element.Element("expenceid") == Convert.ToInt32(l_ExpenceId)
                         select element;
            foreach (var ele in result)
            {
                ele.SetElementValue("IsDeleted", 1);
            }
            document.Save(filePath);
        }

        public static List<DailyExpence> showColumnChart(string transactionType)
        {
           // List<DailyExpence> lstdailyExpence = new List<DailyExpence>();

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\DailyExpence.xml");

            List<DailyExpence> lstdailyExpence = new List<DailyExpence>(from c in document.Descendants("expence")
                        where (string)c.Element("IsDeleted") == "0" &&
                              (string)c.Element("SpendBy") == transactionType
                         select new DailyExpence(
                        
                             c.Element("ExpenceDate").Value,
                             c.Element("AmountSpend").Value
                        ));

            //foreach (var vDailyExpence in custs)
            //{
            //    lstdailyExpence.Add(vDailyExpence);
            //}

            return lstdailyExpence;
        }
    }
}
