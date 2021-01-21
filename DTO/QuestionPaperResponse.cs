using System.Collections.Generic;

namespace DTO
{
    public class QuestionPaperResponse : QuestionPaper
    {
        public ICollection<Question> Questions { get; set; }
    }
}
