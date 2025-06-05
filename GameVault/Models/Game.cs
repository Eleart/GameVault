using System.ComponentModel.DataAnnotations;

namespace GameVault.Models;

public class Game
{
    [Key]
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Plateform { get; set; }
    public States State { get; set; }
}
