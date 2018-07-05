using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TinyTimeline.Models
{
    public class SessionModel
    {
        public Guid SessionId { get; set; }
         
        public DateTime Date { get; set; }
        
        public string Name { get; set; }
        public IEnumerable<TimelineEventModel> Events { get; set; }
        public string Conclusion { get; set; }
        public string Plans { get; set; }
        
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreateDate { get; set; }
    }
}