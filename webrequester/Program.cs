using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using Newtonsoft.Json;

namespace webrequester
{
    class Data
    {
        public string d { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {


            for (int i = 2823; i >= 0; i -= 6)
            {
                WebRequest req = WebRequest.Create("");

                CookieContainer cookieContainer = new CookieContainer();

                req.ContentType = "application/json";
                req.Method = "POST";
                req.Headers.Add(HttpRequestHeader.Cookie,
                    @"ezCMPCCS=true; _ga=GA1.2.50626744.1563714376; __utmz=78814998.1563714377.1.1.utmcsr=google|utmccn=(organic)|utmcmd=organic|utmctr=(not%20provided); ezosuigeneris=e4771058409e7fcd8009fdc7eb5555a3; __gads=ID=e56a11ba59e5da6d:T=1563714377:S=ALNI_MbJag20hFVc-iXb4iTZkn1152LdYw; __qca=P0-503647459-1563714379059; __utma=78814998.50626744.1563714376.1563714377.1563794963.2; ASP.NET_SessionId=3g1hkxaownyswlutbtgpdb4z; active_template::41353=pub_site.1563932046; __atuvc=17%7C30");

                req.Headers.Add("X-Requested-With", "XMLHttpRequest");

                var body = "{ divIndex:";


                body = body + i + "}";
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

                    var asciiString = DecodeEncodedNonAsciiCharacters(responseFromServer);

                    Encoding ascii = Encoding.ASCII;

                    var x = JsonConvert.DeserializeObject<Data>(asciiString);
                    File.AppendAllText(@"e:\output2.txt", x.d);
                }
                resp.Close();
                newStream.Close();
                req = null;
                Console.WriteLine(i);
            }



            // Close the response.  



        }

        static string DecodeEncodedNonAsciiCharacters(string value)
        {
            return Regex.Replace(
                value,
                @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m =>
                {
                    return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
                });
        }
    }
}
