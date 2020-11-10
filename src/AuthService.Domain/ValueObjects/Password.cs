using AuthService.Domain.Exceptions;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Password
    {
        private string _text;

        public Password(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ParameterException("Password is required");

            if (text.Length < 6)
            {
                throw new ParameterException("Password must be at least 6 characters.");
            }

            _text = text;
        }

        public PasswordHash Hash()
        {
            return PasswordHash.Hash(_text);
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Password(string text)
        {
            return new Password(text);
        }

        public static implicit operator string(Password password)
        {
            return password._text;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj is Password)
            {
                return ((Password)obj)._text == _text;
            }
            else if (obj is string)
            {
                return obj.ToString() == _text;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return _text.GetHashCode();
        }
    }
}
