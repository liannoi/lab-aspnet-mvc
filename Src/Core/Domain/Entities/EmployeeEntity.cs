using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Domain.Entities
{
    public class EmployeeEntity
    {
        public int EmployeeId { get; set; }
        [Required] [StringLength(100)] public string FirstName { get; set; }
        [Required] [StringLength(100)] public string LastName { get; set; }
        [Column(TypeName = "date")] public DateTime DateBirthday { get; set; }
        [StringLength(24)] public string Inn { get; set; }
    }
}