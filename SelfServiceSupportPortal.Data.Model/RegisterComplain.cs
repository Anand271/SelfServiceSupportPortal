namespace SelfServiceSupportPortal.Data.Model
{
    public class RegisterComplain
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public string? Remark { get; set; }
        public DateTime RegisterdDate { get; set; }
        public string? Status { get; set; } 
        public string? Comment { get; set; } 
        public string? Phonenumber { get; set; }
    }
}
