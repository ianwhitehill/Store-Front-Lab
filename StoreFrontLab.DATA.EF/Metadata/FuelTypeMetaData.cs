using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(FuelTypeMetaData))]
    public partial class FuelType
    { }
    public class FuelTypeMetaData
    {
        [Required(ErrorMessage = "* Fuel Type is Required")]
        [StringLength(50, ErrorMessage = "* Fuel Type must be 50 characters or less.")]
        [Display(Name = "Fuel Type")]
        public string FuelTypes { get; set; }
    }
}
