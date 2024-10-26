using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfServiceSupportPortal.Data.Model
{
    public class Products
    {
        [Key]
        public int ID { get; set; }
        public string? BrandModuleNo { get; set; }
        public DateTime StartedDate { get; set; }
        public string? Location { get; set; }
        public string? Tag { get; set;}
        public string? McSerialNo { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public int? CompanyId { get; set; }
        [ValidateNever]
        [ForeignKey("CompanyId")]
        public Company? Company { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }

    }
}
