using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This sample program runs through most of ProDoctivity's API operations.");
            Console.WriteLine();
            Console.WriteLine("1) In order to save a document in ProDoctivity, take a look at SavingDocuments/Sample.cs");
            Console.WriteLine("now executing it with sample data ...");
            var newDocumentHandle = new SavingDocuments.Sample().SaveDocument();
            Console.WriteLine(String.Format("Executed. The new document\'s handle is {0}", newDocumentHandle));
            //Console.WriteLine("Press <Enter> to continue");
            //Console.ReadLine();
            Console.WriteLine("2) Now we are going to save a new version of that same document");
            Console.WriteLine("now executing it with sample data ...");
            var newVersionHandle = new SavingDocuments.Sample().SaveDocument(newDocumentHandle);
            Console.WriteLine(String.Format("Executed. The new document\'s handle is {0}", newVersionHandle));
            //Console.WriteLine("Press <Enter> to continue");
            //Console.ReadLine();
            Console.WriteLine("3) Now we are going to query ProDoctivity's API for that same last document, take a look at QueryingDocuments/Sample.cs");
            Console.WriteLine("now executing it with sample data ...");
            var queriedDocument = new QueryingDocuments.Sample().GetDocument(newVersionHandle);
            Console.WriteLine(String.Format("Executed. now some info regarding this document:"));
            Console.WriteLine(String.Format("   DocumentHandle: {0}", queriedDocument.DocumentHandle));
            Console.WriteLine(String.Format("   Document Type: {0}", queriedDocument.DocumentType));
            Console.WriteLine(String.Format("   Creation Date: {0}", queriedDocument.CreationDate));
            Console.WriteLine(String.Format("   Created By: {0}", queriedDocument.CreatedByName));
            Console.WriteLine(" Keywords:");
            queriedDocument.KeywordData.Keywords.ForEach(k =>
            {
                Console.WriteLine("     {0} => {1}", queriedDocument.KeywordData.KeywordMap[k.KeywordHandle].Name, k.Value);
            });
            Console.WriteLine(" Binary data can be found in it's Pages collection");
            //Console.WriteLine("Press <Enter> to continue");
            //Console.ReadLine();
            Console.WriteLine("4) Getting sample context xml data for a given template \"ProDoctivity Sample Template\", take a look at SampleContext/Sample.cs");
            Console.WriteLine("now executing it ...");
            var sample = new SampleContext.Sample().SampleContext();
            Console.WriteLine(String.Format("Sample data is:"));
            Console.WriteLine(sample);
            Console.WriteLine("5) Generating template with sample data, take a look at GenerateDocument/Sample.cs");
            Console.WriteLine("now executing it ...");
            var response = new GenerateDocument.Sample().GenerateDocument(sample);
            Console.WriteLine(String.Format("Generated file's length is:"));
            Console.WriteLine(response.Data.Length);
            Console.WriteLine("6) Viewing our generated document given our generation token, take a look at SeeGeneratedDocuments/Sample.cs");
            Console.WriteLine("now executing it. A browser should launch when you press <Enter> ...");
            Console.WriteLine("Press <Enter> to continue");
            new SeeGenerateDocuments.Sample().SeeGenerateDocument(response.GenerationToken);
            Console.ReadLine();

        }
    }
}
