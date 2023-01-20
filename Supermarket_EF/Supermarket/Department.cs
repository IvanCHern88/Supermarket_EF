using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supermarket_EF.Supermarket
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int Location_Id { get; set; }
        [ForeignKey("Location_Id")]
        public virtual Location Location { get; set; }
        public virtual List<Employee> Employees { get; set; }= new List<Employee>();
    }
}
