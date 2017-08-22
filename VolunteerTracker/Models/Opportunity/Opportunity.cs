using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VolunteerTracker.Models.Volunteer;

namespace VolunteerTracker.Models.Opportunity
{
    public class Opportunity
    {
        // ID
        [Display(Name = "Opportunity ID")]
        public int OpportunityId { get; set; }

        // OPPORTUNITY NAME
        [Required]
        [Display(Name = "Opportunity Name")]
        public string OpportunityName { get; set; }

        // OPPORTUNITY DATE
        [Required]
        [Display(Name = "Opportunity Date")]
        [DataType(DataType.Date)]
        public DateTime OpportunityDate { get; set; }

        // EVENT DAY
        [Required]
        [Display(Name="Event Day")]
        public DayOfEvent EventDay { get; set; }

        // EVENT CENTER
        [Required]
        [Display(Name="Event Center")]
        public Center EventCenter { get; set; }
    }

    public enum DayOfEvent
    {
        Monday=1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
}