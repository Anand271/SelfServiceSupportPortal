namespace SelfServiceSupportPortal.Data.Model.ViewModels
{
    public class ComplainsVM
    {
        public int ID { get; set; }
        public int ComplainId { get; set; }
        public int RegisterComplainId { get; set; }
        public string? Name { get; set; }
        public DateTime RegisterdDate { get; set; }
        public string? Status { get; set; }
        public string? Comment { get; set; }
    }
}
