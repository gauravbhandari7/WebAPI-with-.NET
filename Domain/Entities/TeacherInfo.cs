using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TeacherInfo : AuditableEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int Salary { get; set; }
    }
}
