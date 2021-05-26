using CsvImporter.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter
{
    class Util
    {
        public static string GetUrl()
        {
            var ruta = System.IO.Path.GetFullPath(@"..\..\..\");
            var file = "Configs\\AzureCsv.json";
            Url u = new Url();
            using (StreamReader r = new StreamReader(ruta + file))
            {
                string json = r.ReadToEnd();
                u = JsonConvert.DeserializeObject<Url>(json);
            }
            return u.UrlCsv;
        }
    }
}
