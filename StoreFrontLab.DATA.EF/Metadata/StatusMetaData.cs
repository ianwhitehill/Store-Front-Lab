using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(StatusMetaData))]
    public partial class Status
    { }
    public class StatusMetaData
    {
        [Display(Name = "Status")]
        [Required(ErrorMessage = "* Status is Required")]
        [StringLength(50, ErrorMessage = "* Status must be 50 characters or less.")]
        public string StatusType { get; set; }
    }
}
