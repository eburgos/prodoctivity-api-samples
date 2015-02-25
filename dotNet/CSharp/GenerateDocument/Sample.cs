using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharp.GenerateDocument
{
    class Sample
    {
        /// <summary>
        /// Generates a document given a template and some data
        /// </summary>
        /// <returns></returns>
        public ProdoctivityService.DocumentGenerationResponse GenerateDocument(String xmlData)
        {
            using (ProdoctivityService.ProdoctivityServiceSoapClient client = new ProdoctivityService.ProdoctivityServiceSoapClient())
            {
                //Getting user and password from app.config
                string  user     = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"    ],
                        password = System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"];
                int templateHandle = int.Parse(System.Configuration.ConfigurationManager.AppSettings["sampleTemplateHandle"]) /* ProDoctivity Sample Template */;
                var request = new ProdoctivityService.ProdoctivityDocumentGenerationRequest()
                {
                    Contexts = new List<ProdoctivityService.GenerationContext>(
                        new ProdoctivityService.GenerationContext[] {
                            new ProdoctivityService.GenerationContext() { 
                                Data=XElement.Parse(xmlData), //This is where your data is located
                                Documents= new List<ProdoctivityService.Document>(
                                    new ProdoctivityService.Document[] {
                                        new ProdoctivityService.Document() {
                                            TemplateKey="word" + templateHandle, //This template's key must match "word + <the key of your template>
                                            Dispositions=new List<ProdoctivityService.DocumentDisposition>(
                                                new ProdoctivityService.DocumentDisposition[] {
                                                    new ProdoctivityService.DocumentDisposition() {
                                                        DispositionKey="prodoctivity", //For this specific endpoint your disposition name MUST be "prodoctivity"
                                                        Parameters=XElement.Parse("<data xmlns=\"http://documentgeneration.prodoctivity.com/2011/\"><DocumentKey></DocumentKey><KeywordMap></KeywordMap></data>")
                                                    }
                                                }
                                            )
                                        }
                                    }
                                ),
                                Id="MyGenerationId" //This is your own personal ID, you set here whatever you want. This is just a way to distinguish a document from another one in your response
                            }
                        }
                    ),
                    Dispositions=new List<ProdoctivityService.Disposition>(
                        new ProdoctivityService.Disposition[] {
                            new ProdoctivityService.Disposition() {
                                ClassName="ProdoctivityDocumentGenerationLib.ProdoctivityInsertDisposition, ProdoctivityDocumentGenerationLib", 
                                Key="prodoctivity", 
                                GlobalParameters=XElement.Parse("<data xmlns=\"http://documentgeneration.prodoctivity.com/2011/\"></data>")
                            } //This one MUST be sent as is, sorry
                        }
                    ),
                    DocumentHandle=templateHandle,
                    UserName="fluency", //Find out what are your fluency generator credentials. Chances are that if you didn't change anything it's probably this
                    Password="1234",
                    ProdoctivityUsername=user,
                    ProdoctivityPassword=password,
                    ProdoctivityAgent=null,
                    ResultType=ProdoctivityService.ClientRequestResultType.Inline //Inline result means that this client will wait for fluency to generate the document and return it in Response.Data. This only applies for one template and one context
                };
                return client.GenerateDocumentFromProdoctivityDocumentHandle(request);
            }
        }
    }
}
