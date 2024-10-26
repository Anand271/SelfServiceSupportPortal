using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SelfServiceSupportPortal.Data.Model.ViewModels
{
    public class ProductVM
    {
        public int ID { get; set; }
        public string? BrandModuleNo { get; set; }
        public DateTime StartedDate { get; set; }
        public string? Location { get; set; }
        public string? Tag { get; set; }
        public string? McSerialNo { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public int? CompanyId { get; set; }
        public string? CompanyName { get; set;}
        [ValidateNever]
        public IList<Company>? CompanyList { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsReadOnlyPage { get; set; }
    }
}
