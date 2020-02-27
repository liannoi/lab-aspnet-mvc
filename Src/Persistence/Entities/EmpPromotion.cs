using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanResources.Persistence
{
    [Table("EmpPromotion")]
    public class EmpPromotion
    {
        public int EmpPromotionId { get; set; }

        public int EmployeeId { get; set; }

        public int JobTitleId { get; set; }

        [Column(TypeName = "date")] public DateTime HireDate { get; set; }

        [Column(TypeName = "money")] public decimal? Salary { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual JobTitle JobTitle { get; set; }
    }
}