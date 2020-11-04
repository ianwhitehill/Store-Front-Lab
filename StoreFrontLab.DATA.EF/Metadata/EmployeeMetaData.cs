using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(EmployeeMetaData))]
    public partial class Employee
    { }
    public class EmployeeMetaData
    {
        [Required(ErrorMessage = "* First Name is Required")]
        [StringLength(50, ErrorMessage = "* First Name must be 50 characters or less.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "* Last Name is Required")]
        [StringLength(50, ErrorMessage = "* Last Name must be 50 characters or less.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "* Job Title is Required")]
        [StringLength(50, ErrorMessage = "* Job Title must be 50 characters or less.")]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "Date Not Provided")]
        public System.DateTime DOH { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "Date Not Provided")]
        public Nullable<System.DateTime> DOB { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-Price Not Given-]")]
        public Nullable<decimal> HourlyPay { get; set; }
        public int ManagerID { get; set; }
        public int DepartmentID { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "* Phone Number is Required")]
        [StringLength(50, ErrorMessage = "* Phone Number 10 characters or less.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "* Address is Required")]
        [StringLength(50, ErrorMessage = "* Address 50 characters or less.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "* City is Required")]
        [StringLength(50, ErrorMessage = "* City 50 characters or less.")]
        public string City { get; set; }
        [Required(ErrorMessage = "* State is Required")]
        [StringLength(50, ErrorMessage = "* State 2 character.")]
        public string State { get; set; }
        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "* Zip Code is Required")]
        public int Zip { get; set; }
        [Required(ErrorMessage = "* Email is Required")]
        [StringLength(50, ErrorMessage = "* Email 50 characters or less.")]
        public string Email { get; set; }
    }
}
