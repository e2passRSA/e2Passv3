namespace DTO
{
    public class AnswerResponse : Answer<string>
    {
        public Question<string> Question { get; set; }
    } 
}
