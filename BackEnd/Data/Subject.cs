using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Subject : DTO.Subject
    {
        public ICollection<QuestionPaper> QuestionPapers { get; set; }
    }
}
