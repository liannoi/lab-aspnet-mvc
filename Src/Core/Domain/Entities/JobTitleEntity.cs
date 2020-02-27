using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HumanResources.Domain.Common;

namespace HumanResources.Domain.Entities
{
    public class JobTitleEntity : BaseEntity
    {
        [Required] [StringLength(100)] public string Name { get; set; }

        public ICollection<EmployeePromotionEntity> EmployeePromotions { get; set; }
    }
}