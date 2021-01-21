using BackEnd.Data;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public QuestionsController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpPost("textBased")]
        public async Task<ActionResult<QuestionResponse>> PostTextQuestion(DTO.Question<string> input)

    }
}
