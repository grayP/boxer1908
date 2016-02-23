using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.Models
{
    public class Regattas
    {
        public int RegattaID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public string Result { get; set; }

    }

    public class Regatta
    {
        public int RegattaID { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public int Year { get; set; }
        public DateTime StartDate { get; set; }
        public string Result { get; set; }

    }
}
