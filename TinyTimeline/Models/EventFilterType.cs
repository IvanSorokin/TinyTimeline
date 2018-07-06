using System.ComponentModel.DataAnnotations;

namespace TinyTimeline.Models
{
    public enum EventFilterType
    {
        [Display(Name = "All")]
        All,
        
        [Display(Name = "Positive")]
        Positive,
        
        [Display(Name = "Negative")]
        Negative,
        
        [Display(Name = "Debatable")]
        Debatable,
        
        [Display(Name = "Discussable")]
        Discussable
    }
}