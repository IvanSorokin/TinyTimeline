using System;
using System.ComponentModel.DataAnnotations;

namespace TinyTimeline.Models
{
    public class SessionInfoModel
    {        
        [DataType(DataType.Date)]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime SessionCreateDate { get; set; }
        
        public Guid SessionId { get; set; }
        
        public string SessionName { get; set; }
    }
}