namespace BlazorWebApi.Models;

public class TalentNode
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? IconUrl { get; set; }
    public bool IsUnlocked { get; set; }

    public int? ParentId { get; set; }
    public TalentNode? Parent { get; set; }

    public List<TalentNode> Children { get; set; } = new();

    public int SpecializationId { get; set; }
    public Specialization? Specialization { get; set; }
}