using System.Collections.Generic;

namespace BackEnd.Data
{
    public class QuestionPaper : DTO.QuestionPaper
    {
        public ICollection<Question> Questions { get; set; }
    }
}
