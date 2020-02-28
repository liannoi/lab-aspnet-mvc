using System.ComponentModel.DataAnnotations;

namespace HumanResources.Domain.Entities
{
    public class JobTitleEntity
    {
        public int JobTitleId { get; set; }
        [Required] [StringLength(100)] public string NameJobTitle { get; set; }
    }
}