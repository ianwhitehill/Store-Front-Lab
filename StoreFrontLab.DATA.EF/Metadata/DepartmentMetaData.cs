using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(DepartmentMetaData))]
    public partial class Department
    { }
    public class DepartmentMetaData
    {
        [Display(Name = "Department")]
        [Required(ErrorMessage = "* Department is Required")]
        [StringLength(50, ErrorMessage = "* Department must be 50 characters or less.")]
        public string DepartmentName { get; set; }
    }
}
