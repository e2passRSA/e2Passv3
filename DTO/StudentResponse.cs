using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class StudentResponse : Student
    {
        public ICollection<Test> Tests { get; set; }
    }
}
