using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Answer : DTO.Answer<string>
    {
        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; } = new List<QuestionAnswer>();
    }
}
