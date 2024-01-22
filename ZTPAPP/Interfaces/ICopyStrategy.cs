using projekt.Models;

namespace ZTPAPP.Interfaces
{
    public interface ICopyStrategy
    {
        Flashcard Copy(Flashcard original);
    }
}
