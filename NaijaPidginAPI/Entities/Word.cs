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
        public int WordClassId { get; set; }
        public WordClass WordClass { get; set; }
        [Required]
        public string Sentence { get; set; }

        public DateTime PostedOn { get; set; }
        //[ForeignKey("User")]
        //public int PostedBy { get; set; }
        //public User User { get; set; }
    }
}
