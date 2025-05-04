namespace backend.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int Score { get; set; } // entre 1 et 5

        // Clé étrangère
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }
    }
}
