namespace SelfServiceSupportPortal.Data.Model.ViewModels
{
    public class RegisterComplainVM
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public string? Remark { get; set; }
        public string? UserID { get; set; }
        public DateTime RegisterdDate { get; set; }
        public string? Status { get; set; }
        public Products? Products { get; set; }
        public ProductComplaint? ProductComplaint { get; set; }
        public List<ProductComplaint>? ProductComplaints {get;set;}
    }
}