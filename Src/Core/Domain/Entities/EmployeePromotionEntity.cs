using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Domain.Entities
{
    public class EmployeePromotionEntity
    {
        public int EmpPromotionId { get; set; }
        public int EmployeeId { get; set; }
        public int JobTitleId { get; set; }
        [Column(TypeName = "date")] public DateTime HireDate { get; set; }
        [Column(TypeName = "money")] public decimal? Salary { get; set; }
        public string JobName { get; set; }
    }
}