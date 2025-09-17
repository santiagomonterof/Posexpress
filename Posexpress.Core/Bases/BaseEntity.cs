using System.Text.Json.Serialization;

namespace OKR.Domain.Bases;

public abstract class BaseEntity
{
    public long? Id { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }
}
