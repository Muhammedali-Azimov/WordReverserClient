using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Determine the input file path..");
            string inFilePath = Console.ReadLine();
            string[] strArray = System.IO.File.ReadAllLines(inFilePath);
            //c://users/mehemmedeli.e/desktop/in.txt
            
            using (var client = new HttpClient())
            {
                var endpoint = new Uri("https://localhost:44351/api/WordReverser");
                var json = JsonConvert.SerializeObject(strArray);
                var sendContent = new StringContent(json, Encoding.UTF8, "application/json");

                var result = client.PostAsync(endpoint, sendContent).Result;
                var resultContent = result.Content.ReadAsStringAsync().Result;

                Console.WriteLine("Determine the output path..");
                string outFilePath = Console.ReadLine();
                if (File.Exists(outFilePath))
                {
                    File.Delete(outFilePath);
                }
                using (StreamWriter sw = File.CreateText(outFilePath))
                {
                    var reversedValues = JsonConvert.DeserializeObject<string[]>(resultContent);
                    foreach (var item in reversedValues)
                    {
                        sw.WriteLine(item);
                    }

                }

            }
        }
    }
}
