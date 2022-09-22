namespace Teban.Domain.Base
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string? ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
    }
}
