using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository
{
    public class MarkerList
    {
        public List<Marker> markers { get; set; }
    }

    public class Marker
    {
        public int ID { get; set; }
        public decimal? latitude { get; set; }
        public decimal? longitude { get; set; }
        public string phone { get; set; }
    }
}
