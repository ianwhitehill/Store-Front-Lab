using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(ManagerMetaData))]
    public partial class Manager
    { }
    public class ManagerMetaData
    {
        [Required(ErrorMessage = "* Manager Name is Required")]
        [StringLength(50, ErrorMessage = "* Manager Name must be 50 characters or less.")]
        public string ManagerName { get; set; }
        public Nullable<int> ManagerEmployeeID { get; set; }
    }
}
