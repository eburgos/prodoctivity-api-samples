using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp.SavingDocuments
{
    class Sample
    {
        const string MSWORD_CONTENT_TYPE = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        /// <summary>
        /// Saves a document using Prodoctivity API.
        /// </summary>
        /// <remarks>
        /// In this sample we are saving a word document into ProDoctivity by using the SaveDocumentWithKeywords operation
        /// </remarks>
        /// <returns>The new document's handle</returns>
        public int SaveDocument()
        {
            using (ProdoctivityService.ProdoctivityServiceSoapClient client = new ProdoctivityService.ProdoctivityServiceSoapClient())
            {
                //Getting user and password from app.config
                string  user     = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"    ],
                        password = System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"];
                var data = new ProdoctivityService.ArrayOfBase64Binary();
                //Loading data from a sample document that is in Resource.resx
                data.Add(Resource.Document1);
                var documentData = new ProdoctivityService.DocumentData()
                {
                    Data = new ProdoctivityService.KeywordData()
                    {
                        //Creating a list of keywords with only one keyword
                        Keywords = new List<ProdoctivityService.Keyword>(
                                new ProdoctivityService.Keyword[] {
                                    new ProdoctivityService.Keyword() { 
                                        DataType= ProdoctivityService.DataTypes.Alphanumeric, //This keyword's data type is Alphanumeric, although there are more
                                        KeywordHandle=1,  //Keyword with handle 1 is a sample keyword. Do not use this in production
                                        Value="Test"    //Sample "Test" value
                                    }
                                }
                            )
                    }
                };
                var newDocument = client.SaveDocumentWithKeywords(
                    user, 
                    password, 
                    null, // null agent because we are doing direct authentication
                    1 /* Sample Pages, do not use this in production */, 
                    data,
                    0, // lastVersionDocumentHandle = 0 means this is a new document because there was no prior version of it
                    MSWORD_CONTENT_TYPE, // MSWord content-type. This value is important if you are saving a docx document
                    documentData
                );
                return newDocument.DocumentHandle;
            }
        }

        /// <summary>
        /// Saves a document using ProDoctivity's API given it's parent in order to save a new version of it.
        /// </summary>
        /// <remarks>
        /// In this sample we are saving a word document into ProDoctivity by using the SaveDocumentWithKeywords operation
        /// </remarks>
        /// <param name="originalDocumentHandle">Document handle of it's parent version</param>
        /// <returns>The new document's handle</returns>
        public int SaveDocument(int originalDocumentHandle)
        {
            using (ProdoctivityService.ProdoctivityServiceSoapClient client = new ProdoctivityService.ProdoctivityServiceSoapClient())
            {
                //Getting user and password from app.config
                string user = System.Configuration.ConfigurationManager.AppSettings["prodoctivityUser"],
                        password = System.Configuration.ConfigurationManager.AppSettings["prodoctivityPassword"];
                var data = new ProdoctivityService.ArrayOfBase64Binary();
                //Loading data from a sample document that is in Resource.resx
                data.Add(Resource.Document1);
                var documentData = new ProdoctivityService.DocumentData()
                {
                    Data = new ProdoctivityService.KeywordData()
                    {
                        //Creating a list of keywords with only one keyword
                        Keywords = new List<ProdoctivityService.Keyword>(
                                new ProdoctivityService.Keyword[] {
                                    new ProdoctivityService.Keyword() { 
                                        DataType= ProdoctivityService.DataTypes.Alphanumeric, //This keyword's data type is Alphanumeric, although there are more
                                        KeywordHandle=1,  //Keyword with handle 1 is a sample keyword. Do not use this in production
                                        Value="Test"    //Sample "Test" value
                                    }
                                }
                            )
                    }
                };
                var newDocument = client.SaveDocumentWithKeywords(
                    user,
                    password,
                    null, // null agent because we are doing direct authentication
                    1 /* Sample Pages, do not use this in production */,
                    data,
                    originalDocumentHandle, // lastVersionDocumentHandle. This means that this new document is a newer version of the provided one. This is very important in order to save a new version
                    MSWORD_CONTENT_TYPE, // MSWord content-type. This value is important if you are saving a docx document
                    documentData
                );
                return newDocument.DocumentHandle;
            }
        }
    }
}
