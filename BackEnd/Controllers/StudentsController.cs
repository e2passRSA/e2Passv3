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
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly ImageUploader _imageUploader;

        public StudentsController(ImageUploader imgUploader, ApplicationDbContext db)
        {
            _db = db;
            _imageUploader = imgUploader;
        }


        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        [HttpPost]
        public async Task<ActionResult<StudentResponse>> Post(DTO.Student input, IFormFile file)
        {
            var studentExist = await _db.Students.Where(s => s.Username == input.Username).FirstOrDefaultAsync();
            if (studentExist != null)
                return Conflict(input);

            var imageResponse = await _imageUploader.DataLoaderAsync(file, "userProfile");
            if (!imageResponse.IsSuccess)
                return BadRequest(imageResponse.Message);

            var student = new Data.Student
            {
                Name = input.Name,
                Bio = input.Bio,
                ProfileImageUrl = imageResponse.Data,
                Username = input.Username,
                EmailAddress = input.EmailAddress,

            };
            _db.Students.Add(student);
            await _db.SaveChangesAsync();
            var result = student.MapStudentResponse();
            return CreatedAtAction(nameof(GetStudent), new { username = result.Username }, result);
        }

        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        [HttpGet("{username}")]
        public async Task<ActionResult<StudentResponse>> GetStudent(string username)
        {
            
        }
    }
}
