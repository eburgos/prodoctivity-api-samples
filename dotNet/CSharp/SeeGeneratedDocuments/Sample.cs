using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharp.SeeGenerateDocuments
{
    class Sample
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void SeeGenerateDocument(String generationToken)
        {
            String endpoint = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUrl"];
            
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.FileName = endpoint + "?u=" + System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"] + "&p=" + System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"] + "&ReturnURL=/Action.aspx?action=/view/documents/" + generationToken;
            p.Start();
        }
    }
}
