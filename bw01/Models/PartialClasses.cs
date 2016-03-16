using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataRepository.Models;
using System.Web.Mvc;

namespace bw01.Models
{
    public class PartialClasses
    {

        public partial class DocumentViewModel
        {
            [AllowHtml]
            public string DocText { get; set; }
        }
    }
}
