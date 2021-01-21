using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class Student : DTO.Student
    {
        public ICollection<Test> Tests { get; set; }
    }
}
