using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GlossaryOfTerms.Models
{
    [MetadataType(typeof(GlossaryMetadata))]
    public partial class Glossary
    {
    }

    public class GlossaryMetadata
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide the Glossary-Term")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Glossary-Terms must be atleast 2 characters in length")]
        [Remote("IsTermUnique", "Validation", AdditionalFields = "ID, Definition", ErrorMessage = "This {0} already exists in Glossaries.")]
        public string Term { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide the Glossary-Definition")]
        public string Definition { get; set; }
    }
}