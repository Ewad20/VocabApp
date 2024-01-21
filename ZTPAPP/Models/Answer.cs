namespace projekt.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public TestHistory? Test { get; set; }
        public Flashcard? Flashcard { get; set; }
        public string? GivenAnswer { get; set; }
    }
}