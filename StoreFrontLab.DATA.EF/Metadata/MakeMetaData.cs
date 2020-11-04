using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(MakeMetaData))]
    public partial class Make
    { }
    public class MakeMetaData
    {
        public string Makes { get; set; }
    }
}
