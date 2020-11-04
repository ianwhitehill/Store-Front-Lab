using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFrontLab.DATA.EF
{
    [MetadataType(typeof(MovieMetaData))]
    public partial class Movie
    { }
    public class MovieMetaData
    {
        [Required(ErrorMessage = "* Movie Title is Required")]
        [StringLength(50, ErrorMessage = "* Movie Title must be 50 characters or less.")]
        public string MovieTitle { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "Date Not Provided")]
        public System.DateTime ReleaseDate { get; set; }
    }
}
