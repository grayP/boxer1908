using DataRepository.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DataRepository
{


    
    [MetadataType(typeof(RegattaMetaData))]
    public partial class Regatta
    {

      





    }


    [MetadataType(typeof(CrewMemberMetaData))]
    public partial class CrewMember
    {
    }
}