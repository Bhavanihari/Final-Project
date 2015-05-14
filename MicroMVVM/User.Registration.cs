using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MicroMVVM.User.Registration
{
    public class UserRegistrationMgr
    {
        #region Member Variables

        private string l_Name = string.Empty;

        private string l_Email = string.Empty;

        private string l_Password = string.Empty;

        #endregion

        #region Properties

        public string Name
        {
            get { return l_Name; }
            set { l_Name = value; }
        }

        public string Email
        {
            get { return l_Email; }
            set { l_Email = value; }
        }

        public string Password
        {
            get { return l_Password; }
            set { l_Password = value; }
        }

        #endregion

        public void RegisterNew()
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\LoginXml.xml");

            int count = document.Descendants("id").Count();

            string filePath = ((Directory.GetCurrentDirectory() + @"\LoginXml.xml"));

            var newElement = new XElement("User",
                     new XElement("id", count + 1),
                     new XElement("user", l_Name),
                     new XElement("username", l_Email),
                     new XElement("password", l_Password));

            document.Element("Authenticate").Add(newElement);

            document.Save(filePath);
        }
    }
}
