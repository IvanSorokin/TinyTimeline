namespace Domain.Objects
{
    public class AuthToken
    {
        public AuthToken(string value, UserRole role)
        {
            Value = value;
            Role = role;
        }

        public string Value { get; }
        public UserRole Role { get; }
    }
}