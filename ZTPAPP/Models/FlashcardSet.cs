﻿namespace projekt.Models
{
    public class FlashcardSet
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public List<Flashcard>? Flashcards { get; set; }
        public List<Test>? Tests { get; set; }
    }
}