using System.ComponentModel.DataAnnotations;

namespace Pubinno.Api.Entities
{
    public class PourEvent
    {
        [Key]
        public Guid EventId { get; set; } // Primary Key handles Idempotency
        public string DeviceId { get; set; }
        public string LocationId { get; set; }
        public string ProductId { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndedAt { get; set; }
        public int VolumeMl { get; set; }
    }
}
