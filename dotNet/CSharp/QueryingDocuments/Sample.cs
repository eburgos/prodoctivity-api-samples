using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.QueryingDocuments
{
    class Sample
    {
        /// <summary>
        /// Queries a document by it's document handle
        /// </summary>
        /// <returns>The new document's handle</returns>
        public ProdoctivityService.ProdoctivityDocument GetDocument(int documentHandle)
        {
            using (ProdoctivityService.ProdoctivityServiceSoapClient client = new ProdoctivityService.ProdoctivityServiceSoapClient())
            {
                //Getting user and password from app.config
                string  user     = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"    ],
                        password = System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"];
                var document = client.GetDocument(
                    user,
                    password,
                    null,
                    documentHandle,
                    false // Setting this to false means that we are getting that specific version. Setting it to true means getting it's latest version regardless of which one you provided
                );
                return document;
            }
        }
    }
}
