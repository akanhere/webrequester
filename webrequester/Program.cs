using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web;

namespace webrequester
{
    class Program
    {
        static void Main(string[] args)
        {
            WebRequest req = WebRequest.Create("https://hindi.sanjeevkapoor.com/AllRecipesHindi.aspx/GetDataFromServer");
            req.ContentType = "application/json";
            req.Method = "POST";
            req.Headers.Add(HttpRequestHeader.Cookie, @"ezCMPCCS=true; _ga=GA1.2.50626744.1563714376; __utmz=78814998.1563714377.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); ezosuigeneris=e4771058409e7fcd8009fdc7eb5555a3; __gads=ID=e56a11ba59e5da6d:T=1563714377:S=ALNI_MbJag20hFVc-iXb4iTZkn1152LdYw; __qca=P0-503647459-1563714379059; __utma=78814998.50626744.1563714376.1563714377.1563794963.2; ezovuuidtime_41353=1563794968; ASP.NET_SessionId=3g1hkxaownyswlutbtgpdb4z; _gid=GA1.2.223916375.1563931270; ezoadgid_41353=-1; ezoref_41353=; ezoab_41353=mod1; active_template::41353=pub_site.1563931372; __atuvc=16%7C30; __atuvs=5d37b284444db31700c");
            var body = "{divIndex: 1000}";
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] byte1 = encoding.GetBytes(body);
            req.ContentLength = byte1.Length;
            Stream newStream = req.GetRequestStream();

            newStream.Write(byte1, 0, byte1.Length);

            var resp = req.GetResponse();

            using (Stream dataStream = resp.GetResponseStream())
            {
                // Open the stream using a StreamReader for easy access.  
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.  
                string responseFromServer = reader.ReadToEnd();
                // Display the content.  
                File.WriteAllText(@"e:\output.txt", HttpUtility.HtmlDecode(responseFromServer));
            }

            // Close the response.  
            resp.Close();
            newStream.Close();
            req = null;


        }
    }
}
