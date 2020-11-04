using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(ModelMetaData))]
    public partial class Model
    { }
    public class ModelMetaData
    {
        public string Trim { get; set; }
        public string Engine { get; set; }
        public string Models { get; set; }
    }
}
