using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HERCULES.Shared.DTO
{
    public class CourseDto
    {
        public int CourseNo { get; set; }
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        public decimal? Cost { get; set; }
        public int? Prerequisite { get; set; }
        [Required]
        [StringLength(30)]
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        [StringLength(30)]
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int SchoolId { get; set; }
        public int? PrerequisiteSchoolId { get; set; }

    }
}
