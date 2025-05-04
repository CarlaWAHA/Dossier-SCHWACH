using System.Collections.Generic;

namespace backend.Models
{
    public class Actor
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;


        // Navigation property vers les films
    }
}
