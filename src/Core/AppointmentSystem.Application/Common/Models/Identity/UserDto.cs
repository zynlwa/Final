using System.Text.Json.Serialization;

public record UserDto
{
    public string Id { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string UserName { get; init; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? DoctorId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? PatientId { get; set; }
    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
