namespace WebAppBlazor.Models;

public class TalentTreeDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public List<TalentTreeDto> Children { get; set; } = new();
    public int? ParentId { get; set; }
    public bool IsUnlocked { get; set; }
}
