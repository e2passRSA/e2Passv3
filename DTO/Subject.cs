using System;

namespace DTO
{
    public class Subject
    {
        public int Id { get; set; }
        public virtual string Title { get; set; }
        public string ImageUrl { get; set; }
        public int? QuestionId { get; set; }
        public DateTimeOffset?  CreatedOn { get; set; }
    }
}
