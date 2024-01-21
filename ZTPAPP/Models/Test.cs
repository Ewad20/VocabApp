namespace projekt.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FlashcardSet>? FlashcardSets { get; set; }
        public string Type { get; set; } = "TEST";

    }
}
