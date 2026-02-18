namespace Pubinno.Api.Models
{
    public class PourRequest
    {
        public Guid EventId { get; set; }

        public string DeviceId { get; set; } = null!;
        public string LocationId { get; set; } = null!;
        public string ProductId { get; set; } = null!;

        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }

        public int VolumeMl { get; set; }
    }

}
