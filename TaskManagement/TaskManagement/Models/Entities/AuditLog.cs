namespace TaskManagement.Models.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public string? UserId { get; set; }
        public string Endpoint { get; set; } = null!;
        public string Method { get; set; } = null!;
        public string IpAddress { get; set; } = null!;

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
