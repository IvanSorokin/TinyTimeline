using System;

namespace Domain.Objects
{
    public class SessionInfo
    {
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid Id { get; set; }
    }
}