using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(OrderMetaData))]
    public partial class Order
    { }
    public class OrderMetaData
    {
        public int CustomerID { get; set; }
        public string VIN { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "Date Not Provided")]
        public System.DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "Date Not Provided")]
        public System.DateTime EndDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:c}", NullDisplayText = "[-Price Not Given-]")]
        public decimal TotalCost { get; set; }

    }
}
