using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public class AuditableEntity : BaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
