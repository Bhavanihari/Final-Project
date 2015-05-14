using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace MicroMVVM.User.Login
{
    public class LoginMgr
    {
        public int AuthenticateLogin(string username, string pwd)
        {
            int loginId = 0;

            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

            XDocument document = XDocument.Load(appPath + @"\LoginXml.xml");

            var query = (from r in document.Descendants("User").Where
                                        (r => (string)r.Element("username") == username && (string)r.Element("password") == pwd)
                         select (string)r.Element("id"));

            bool qry = query.Any();

            if (qry == true)
            {
                loginId = Convert.ToInt32(query.First());
            }
            return loginId;
        }
    }
}
