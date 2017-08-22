
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.BuilderProperties;

namespace VolunteerTracker.Models.Volunteer
{
    public class Volunteer
    {
        // ID
        [Display(Name = "Volunteer ID")]
        public int VolunteerId { get; set; }

        // FIRST NAME
        [Required]
        [Display(Name = "First Name")]
        [StringLength(35)]
        public string FirstName { get; set; }

        // LAST NAME
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(35)]
        public string LastName { get; set; }

        // AVAILABILITY
        [Display(Name = "Availability")]
        public DayAvailable AvailableDay { get; set; }

        // PREFERRED CENTER
        [Display(Name = "Preferred Center")]
        public Center CenterPreferred { get; set; }

        // EDUCATION
        [Display(Name = "Education")]
        public EducationLevel HighestEducation { get; set; }

        // SKILL
        [Display(Name = "Skill")]
        public Skill Skill { get; set; }

        // LICENSE
        [Display(Name = "License")]
        public License CurrentLicense { get; set; }

        // USERNAME
        [Required]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        // PASSWORD
        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }

        // ADDRESS
        [Display(Name = "Address")]
        public string Address { get; set; }

        // PHONE (HOME)
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public string HomeNumber { get; set; }

        // PHONE (CELL)
        [Display(Name = "Cell Phone")]
        [DataType(DataType.PhoneNumber)]
        public string CellNumber { get; set; }

        // PHONE (WORK)
        [Display(Name = "Work Phone")]
        [DataType(DataType.PhoneNumber)]
        public string WorkNumber { get; set; }

        // EMAIL
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(255)]
        public string Email { get; set; }

        // EMERGENCY CONTACT
        [Display(Name = "Emergency Contact")]
        public string EmergencyContact { get; set; }

        // DRIVER'S LICENSE
        [Display(Name = "Driver's License")]
        public bool HasDriversLicense { get; set; }

        // SOCIAL SECURITY CARD
        [Display(Name = "Social Security Card")]
        public bool HasSsCard { get; set; }

        // STATUS
        [Required]
        [Display(Name = "Status")]
        public Status Status { get; set; }
    }

    public enum DayAvailable
    {
        Monday = 1,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }

    public enum Center
    {
        [Display(Name = "Florida State College at Jacksonville")] FSCJ = 1,
        [Display(Name = "Florida State University")] FSU,
        [Display(Name = "Jacksonville University")] JU,
        [Display(Name = "St. Johns River State College")] SJRSC,
        [Display(Name = "University of Central Florida")] UCF,
        [Display(Name = "University of North Florida")] UNF,
        [Display(Name = "University of Florida")] UF
    }

    public enum EducationLevel
    {
        [Display(Name = "High school")] Highschool=1,
        [Display(Name = "Some college")] College,
        [Display(Name = "Associate's degree")] Associates,
        [Display(Name = "Bachelor's degree")] Bachelors,
        [Display(Name = "Master's degree")] Masters,
        [Display(Name = "Doctorate or professional degree")] Doctorate
    }

    public enum Skill
    {
        Analysis=1,
        Communication,
        Counseling,
        Empathy,
        Leadership,
        [Display(Name = "Public speaking")] PublicSpeaking,
        Planning,
        [Display(Name = "Problem solving")] ProblemSolving,
        Programming,
        Teaching,
        [Display(Name = "Time management")] TimeManagement,
        Writing
    }
    
    public enum License
    {
        [Display(Name = "Architecture & Interior Design")] Architecture = 1,
        [Display(Name = "Certified Public Accounting")] Accounting,
        Construction,
        Engineering,
        [Display(Name = "Health Care")] HealthCare,
        [Display(Name = "Landscape Architecture")] LandscapeArchitecture,
        [Display(Name = "Real Estate")] RealEstate,
        [Display(Name = "Social Work")] SocialWork
    }

    public enum Status
    {
        Approved = 1,
        Pending,
        Disapproved,
        Inactive
    }
}
