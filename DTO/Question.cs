namespace DTO
{
    public class Question<T>
    {
        public int Id { get; set; }
        public virtual T QuestionContent { get; set; }
        public virtual string QuestionPoints { get; set; }
        public int? QuestionPaperId { get; set; }
    }
}
