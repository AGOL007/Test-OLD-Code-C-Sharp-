using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TestBase.Validation
{
    public static class JsonToXmlConversion
    {
        public static string JsonToXml(string jsonFileText)
        {
            XmlDocument xml = JsonConvert.DeserializeXmlNode(jsonFileText, "AutomationTest");
            string doc = xml.InnerXml;

            return doc;
        }

    }
}
