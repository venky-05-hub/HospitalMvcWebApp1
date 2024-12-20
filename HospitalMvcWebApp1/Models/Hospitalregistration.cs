namespace HospitalMvcWebApp1.Models
{
    public class Hospitalregistration
    {
        public Guid Id { get; set; }

        public string HospitalType { get; set; }

        public string PrimaryContactName { get; set; }

        public long PrimaryContactNumber { get; set; }

        public long AlternateContactNumber { get; set; }

        public string AlternateContactName { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string District { get; set; }

        public string Password { get; set; }

    }
}
