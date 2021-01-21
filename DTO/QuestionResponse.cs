using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class QuestionResponse : Question<string>
    {
        public ICollection<Answer<string>> Answers { get; set; }
        public ICollection<QuestionPaper> QuestionPapers { get; set; }

    }
}
