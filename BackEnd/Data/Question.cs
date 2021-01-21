using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Question : DTO.Question<string>
    {
        public ICollection<QuestionAnswer> QuestionAnswers { get; set; }
        public ICollection<QuestionPQuestion> QuestionPQuestions { get; set; }    
    }
}
