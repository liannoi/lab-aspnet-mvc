using System;
using System.ComponentModel.DataAnnotations.Schema;
using HumanResources.Domain.Common;

namespace HumanResources.Domain.Entities
{
    public class EmployeePromotionEntity : BaseEntity
    {
        public int EmployeeId { get; set; }
        public int JobTitleId { get; set; }
        [Column(TypeName = "date")] public DateTime HireDate { get; set; }
        [Column(TypeName = "money")] public decimal? Salary { get; set; }

        public EmployeeEntity Employee { get; set; }
        public JobTitleEntity JobTitle { get; set; }
    }
}