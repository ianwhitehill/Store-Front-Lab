using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(CarMetaData))]
    public partial class Car
    { }
    public class CarMetaData
    {
        [Required(ErrorMessage = "* VIN is Required")]
        [StringLength(50, ErrorMessage = "* VIN must be 50 characters or less.")]
        public string VIN { get; set; }
        [Required(ErrorMessage = "* Year is Required")]        
        public int Year { get; set; }
        [Required(ErrorMessage = "* Make is Required")]
        [Display(Name = "Make")]
        public int MakeID { get; set; }
        [Required(ErrorMessage = "* Movie is Required")]
        [Display(Name = "Movie")]
        public int MovieID { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-Price Not Given-]")]
        public decimal DailyCost { get; set; }
        [Required(ErrorMessage = "* Fuel Type is Required")]
        [Display(Name = "Fuel Type")]
        public int FuelTypeID { get; set; }
        [Required(ErrorMessage = "* Status is Required")]
        [Display(Name = "Status")]
        public int StatusID { get; set; }
        [UIHint("MultilineText")]
        public string Description { get; set; }
        public bool IsAutomatic { get; set; }
        public string Image { get; set; }
    }
}
