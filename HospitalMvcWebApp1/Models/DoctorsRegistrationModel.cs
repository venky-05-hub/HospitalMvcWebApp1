namespace HospitalMvcWebApp1.Models
{
    public class DoctorsRegistrationModel
    {
        public Guid DId { get; set; }
        public string? DoctorsName { get; set; }

        public long MobileNo { get; set; }

        public string? Specialization { get; set; }
        public string? Degrees { get; set; }
    }
}
