namespace BackEnd.Data
{
    public class QuestionAnswer
    {
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        public int AnswerId { get; set; }
        public Answer Answer { get; set; }
    }

    public class SubjectQuestionPaper
    {
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public int QuestionPaperId { get; set; }
        public QuestionPaper QuestionPaper { get; set; }
    }

    public class QuestionPQuestion
    {
        public int QuestionPaperId { get; set; }
        public QuestionPaper QuestionPaper { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
