using System.ComponentModel.DataAnnotations;

namespace backend.Models;
public class User
{
    [Key] 
    public int Id { get; set; }
    public required  string Username { get; set; }
    public required string PasswordHash { get; set; } // Stocké hashé, jamais en clair
}