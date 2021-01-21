using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Infrastructure
{
    public static class EntityExtension
    {
        public static StudentResponse MapStudentResponse(this Data.Student student)
            => new StudentResponse
            {
                Id = student.Id,
                Name = student.Name,
                Bio = student.Bio,
                EmailAddress = student.EmailAddress,
                ProfileImageUrl = student.ProfileImageUrl,
                Tests = student.Tests?.Select(t => new DTO.Test
                {
                    AverageGrade = t.AverageGrade,
                    QuestionPaperId = t.QuestionPaperId
                }).ToList(),
                Username = student.Username
            };

        public static DTO.AnswerResponse MapAnswerResponse(this Data.Answer answer)
            => new DTO.AnswerResponse
            {
                Id = answer.Id,
                AnswerContent = answer.AnswerContent,
                QuestionId = answer.QuestionId,
                Question = answer.QuestionAnswers?.Select(s => new DTO.Question<string>
                {
                    Id = s.QuestionId,
                    QuestionContent = s.Question.QuestionContent,
                    QuestionPoints = s.Question.QuestionPoints
                }).FirstOrDefault()
            };

        public static DTO.QuestionResponse MapQuestionResponse(this Data.Question question)
            => new DTO.QuestionResponse
            {
                Id = question.Id,
                QuestionContent = question.QuestionContent,
                QuestionPoints = question.QuestionPoints,
                QuestionPapers = question.QuestionPQuestions?.Select(qp => new DTO.QuestionPaper
                {
                    Id = qp.QuestionPaperId,
                    Description = qp.QuestionPaper.Description
                }).ToList(),
                Answers = question.QuestionAnswers?.Select(a => new DTO.Answer<string>
                {
                    Id = a.AnswerId,
                    AnswerContent = a.Answer.AnswerContent
                }).ToList()
            };
    }
}
