using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace HumanResources.Persistence
{
    [Table("JobTitle")]
    public class JobTitle
    {
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public JobTitle()
        {
            EmpPromotions = new HashSet<EmpPromotion>();
        }

        public int JobTitleId { get; set; }

        [Required] [StringLength(100)] public string NameJobTitle { get; set; }

        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmpPromotion> EmpPromotions { get; set; }
    }
}