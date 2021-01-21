namespace DTO
{
    public class Answer<T>
    {
        public int Id { get; set; }
        public virtual T AnswerContent { get; set; }
        public int QuestionId { get; set; }
        public bool IsAnswerCorrect { get; set; }

    }
}
