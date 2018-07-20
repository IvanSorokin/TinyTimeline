using System;

namespace Domain.Objects
{
    public class AuthToken
    {
        public AuthToken(Guid value, UserRole role)
        {
            Value = value;
            Role = role;
        }

        public Guid Value { get; }
        public UserRole Role { get; }
    }
}