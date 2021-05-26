using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvImporter.Model
{
    class Stocks
    {
        public int PointOfSale { get; set; }
        public int Product { get; set; }
        public DateTime Date { get; set; }
        public int Stock { get; set; }
    }
}
