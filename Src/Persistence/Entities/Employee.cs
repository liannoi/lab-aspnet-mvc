using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HumanResources.Persistence
{
    [Table("Employee")]
    public class Employee
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            EmpPromotions = new HashSet<EmpPromotion>();
        }

        public int EmployeeId { get; set; }

        [Required] [StringLength(100)] public string FirstName { get; set; }

        [Required] [StringLength(100)] public string LastName { get; set; }

        [Column(TypeName = "date")] public DateTime DateBirthday { get; set; }

        [StringLength(24)] public string INN { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpPromotion> EmpPromotions { get; set; }
    }
}