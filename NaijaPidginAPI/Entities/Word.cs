using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaijaPidginAPI.Entities
{
    public class Word : BaseEntity
    { 
        public int WordId { get; set; }

        [Required]
        public string WordInput { get; set; }
        [Required]
        public string Definition { get; set; }
        [Required]
        public int WordClassId { get; set; }
        public WordClass WordClass { get; set; }
        [Required]
        public string Sentence { get; set; }

        public bool IsApproved { get; set; } 
        //public bool IsRejected { get; set; }
        public DateTime PostedOn { get; set; } = DateTime.Now;
        public int? ApprovedBy { get; set; }
        //public int DeletedBy { get; set; }
        public int? PostedBy { get; set; }



    }
}
