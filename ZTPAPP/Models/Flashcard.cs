using System.ComponentModel.DataAnnotations;

namespace projekt.Models
{
    public class Flashcard
    {
        public int Id { get; set; }
        public string SourceWord { get; set; }
        public string TranslatedWord { get; set; }
        public List<FlashcardSet>? FlashcardSets { get; set; }

        public Flashcard DeepCopy()
        {
            Flashcard clone = new Flashcard();
            clone.SourceWord = SourceWord;
            clone.TranslatedWord = TranslatedWord;

            return clone;
        }

        internal Flashcard ShallowCopy()
        {
            return (Flashcard)this.MemberwiseClone();
        }
    }
}