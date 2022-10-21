using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HERCULES.EF.Models
{
    [Table("SCHOOL")]
    public partial class School
    {
        public School()
        {
            Courses = new HashSet<Course>();
            Enrollments = new HashSet<Enrollment>();
            Sections = new HashSet<Section>();
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        public int SchoolId { get; set; }
        [Required]
        [Column("SCHOOL_NAME")]
        [StringLength(30)]
        public string SchoolName { get; set; }
        [Required]
        [Column("CREATED_BY")]
        [StringLength(30)]
        public string CreatedBy { get; set; }
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Column("MODIFIED_BY")]
        [StringLength(30)]
        public string ModifiedBy { get; set; }
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty(nameof(Course.School))]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty(nameof(Enrollment.School))]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty(nameof(Section.School))]
        public virtual ICollection<Section> Sections { get; set; }
        [InverseProperty(nameof(Student.School))]
        public virtual ICollection<Student> Students { get; set; }
    }
}
