

namespace DataRepository
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public  class RegattaMetaData
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Regatta name cannot be empty")]
        [Display(Description = "Regatta Name")]
        [StringLength(255, MinimumLength = 4, ErrorMessage = "Regatta Name must be more than {2} and less than {1} characters")]
        public string RegattaName { get; set; }
        [Range(typeof(DateTime), "1 Jan 2000", "31 Dec 2050", ErrorMessage = "Date must be between {1} and {2}.")]
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<int> RegattaYear { get; set; }
        public string Result { get; set; }
    }

    public class CrewMemberMetaData
    {
        [Required(ErrorMessage="Crew Name cannot be empty")]
        public string CrewName { get; set; }
        [EmailAddress]
        public string CrewEmail { get; set; }   
    }
}
