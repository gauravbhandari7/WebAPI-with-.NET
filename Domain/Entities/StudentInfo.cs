using Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class StudentInfo : AuditableEntity
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
