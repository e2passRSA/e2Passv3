using BackEnd.Data;
using BackEnd.Infrastructure;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ImageUploader _imageUploader;

        public AnswersController(ApplicationDbContext db, ImageUploader imageUploader)
        {
            _db = db;
            _imageUploader = imageUploader;
        }


        [HttpPost("TextBased")]
        public async Task<ActionResult<AnswerResponse>> PostTextAnswer(DTO.Answer<string> input)
        {
            var question = await _db.Questions.FindAsync(input.QuestionId);

            if (question == null)
                return NotFound(new ErrorModel
                {
                    Reason = "Question with the provided QuestionId does not exist",
                    Error = "Question Not Found"
                });

            var answer = new Data.Answer
            {
                AnswerContent = input.AnswerContent,
                QuestionId = input.QuestionId,
                IsAnswerCorrect = input.IsAnswerCorrect,
            };
            _db.Answers.Add(answer);

            question.QuestionAnswers = new List<QuestionAnswer>();

            question.QuestionAnswers.Add(new QuestionAnswer
            {
                Question = question,
                Answer = answer
            });
            var result = answer.MapAnswerResponse();
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
        }

        [HttpPost("ImageBased")]
        public async Task<ActionResult<AnswerResponse>> PostImageAnswer(DTO.Answer<IFormFile> input)
        {
            var question = await _db.Questions.FindAsync(input.QuestionId);

            if (question == null)
                return NotFound(new ErrorModel
                {
                    Reason = "Question with the provided QuestionId does not exist",
                    Error = "Question Not Found"
                });

            var imageResponse = await _imageUploader.DataLoaderAsync(input.AnswerContent, "answers");

            if (!imageResponse.IsSuccess)
                return BadRequest(new ErrorModel
                {
                    Error = imageResponse.Data,
                    Reason = imageResponse.Message
                });

            var answer = new Data.Answer
            {
                AnswerContent = imageResponse.Data,
                QuestionId = input.QuestionId,
                IsAnswerCorrect = input.IsAnswerCorrect,
            };
            _db.Answers.Add(answer);

            question.QuestionAnswers = new List<QuestionAnswer>();

            question.QuestionAnswers.Add(new QuestionAnswer
            {
                Question = question,
                Answer = answer
            });
            var result = answer.MapAnswerResponse();
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { Id = result.Id }, result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{questionId}")]
        public async Task<ActionResult<AnswerResponse>> Get(int questionId, int answerId)
        {
            var answer = await _db.Answers
                .AsNoTracking()
                .Where(s => s.QuestionId == questionId)
                .Include(s => s.QuestionAnswers)
                .ThenInclude(qa => qa.Question)
                .Select(s => s.MapAnswerResponse())
                .SingleOrDefaultAsync(s => s.Id == answerId);

            if (answer == null)
                return NotFound(new ErrorModel
                {
                    Error = "Answer Not Found",
                    Reason = "Invalid AnswerId entered"
                });

            return answer;
        }


        [HttpGet("question/{questionId}")]
        public async Task<ActionResult<List<AnswerResponse>>> GetAnswers(int questionId)
        {
            var answers = await _db.Answers
                .AsNoTracking()
                .Where(q => q.QuestionId == questionId)
                .Include(q => q.QuestionAnswers)
                .ThenInclude(qa => qa.Question)
                .Select(q => q.MapAnswerResponse())
                .ToListAsync();

            return answers;
        }

        public class ErrorModel
        {
            public string Reason { get; set; }
            public string Error { get; set; }
        }
    }
}
