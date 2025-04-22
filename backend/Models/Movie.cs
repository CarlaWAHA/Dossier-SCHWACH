namespace backend.Models;
 public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string PosterUrl { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public List<Actor>? Actors { get; set; }
    public List<Comment> Comments { get; set; }= new();
    public List<Rating>? Ratings { get; set; }
    public int Likes { get; set; } = 0;
    public int Dislikes { get; set; } = 0;
    public int Views { get; set; } = 0;

}
