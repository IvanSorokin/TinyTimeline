using System;
using System.ComponentModel.DataAnnotations;

namespace TinyTimeline.Models
{
    public class TimelineEventModel
    {
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public string Text { get; set; }

        public int Positive { get; set; }

        public int Negative { get; set; }

        public Guid Id { get; set; }
    }
}