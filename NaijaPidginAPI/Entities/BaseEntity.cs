using System;

namespace NaijaPidginAPI.Entities
{
    public class BaseEntity
    {
        //public int CreatedBy { get; set; }
        //public DateTime DateTimeCreated { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public int? LastUpdatedBy { get; set; }
    }
}
