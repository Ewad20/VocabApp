namespace projekt.Models
{
    public class TestHistory
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public Test? Test { get; set; }
        public int points { get; set; }
        public List<Answer>? Answers { get; set; }

    }
}