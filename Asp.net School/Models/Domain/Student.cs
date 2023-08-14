using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asp.net_School.Models.Domain
{
    [Table("Student", Schema = "public")]
    public class Student
    {
        [Key]   
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
