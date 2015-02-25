using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SampleContext
{
    class Sample
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns>The new document's handle</returns>
        public string SampleContext()
        {
            using (ProdoctivityService.ProdoctivityServiceSoapClient client = new ProdoctivityService.ProdoctivityServiceSoapClient())
            {
                //Getting user and password from app.config
                string  user     = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"    ],
                        password = System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"];

                var xml = client.GetTemplateSampleContextXml(user, password, null, int.Parse(System.Configuration.ConfigurationManager.AppSettings["sampleTemplateHandle"]) /* ProDoctivity Sample Template */);
                return xml.ToString();
            }
        }
    }
}
