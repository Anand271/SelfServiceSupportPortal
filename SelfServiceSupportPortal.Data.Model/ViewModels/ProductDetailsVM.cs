using System.ComponentModel.DataAnnotations;

namespace SelfServiceSupportPortal.Data.Model.ViewModels
{
    public class ProductDetailsVM
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? BrandModuleNo { get; set; }
        public string? Location { get; set; }
        public List<int>? ComplainIds { get; set; }
        public string? ComplainDescription { get; set; }
        public DateTime RegisterdDate { get; set; }
        public string? Tag { get; set; }
        public string? McSerialNo { get; set; }
        public string? Department { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        [Required]
        public string? Phonenumber { get; set; }

    }
}
