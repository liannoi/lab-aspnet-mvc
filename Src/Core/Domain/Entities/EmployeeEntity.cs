using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanResources.Domain.Common;

namespace HumanResources.Domain.Entities
{
    public class EmployeeEntity : BaseEntity
    {
        [Required] [StringLength(100)] public string FirstName { get; set; }
        [Required] [StringLength(100)] public string LastName { get; set; }
        [Column(TypeName = "date")] public DateTime DateBirthday { get; set; }
        [StringLength(24)] public string Inn { get; set; }

        public ICollection<EmployeePromotionEntity> Promotions { get; set; }
    }
}