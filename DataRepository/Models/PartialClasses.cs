using DataRepository.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;




namespace DataRepository
{

     public partial class Location
    {
       public int Take { get; set; }
    }

    [MetadataType(typeof(RegattaMetaData))]
    public partial class Regatta
    {

      





    }


    [MetadataType(typeof(CrewMemberMetaData))]

    public partial class CrewMember
    {
    }


   
}