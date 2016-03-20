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
        public string lat { get; set; }
        public string lng { get; set; }
        public string html { get; set; }
        public string label { get; set; }
    }
}
