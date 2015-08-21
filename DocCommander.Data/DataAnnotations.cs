using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocCommander.Data
{
    [MetadataType(typeof(NumConfigMetaData))]
    public partial class NumConfig
    {

    }

    public class NumConfigMetaData
    {
        public string Name { get; set; }
        public bool IsHierarchy { get; set; }
        [Display(Name ="Part 1")]
        public string Part1ListName { get; set; }
        [Display(Name ="Part 2")]
        public string Part2ListName { get; set; }
        [Display(Name = "Part 3")]
        public string Part3ListName { get; set; }
        [Display(Name = "Part 4")]
        public string Part4ListName { get; set; }
        [Display(Name = "Part 5")]
        public string Part5ListName { get; set; }
    }

}
