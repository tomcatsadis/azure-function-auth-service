using AuthService.Domain.Exceptions;
using System.Text.RegularExpressions;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Name
    {
        private string _text;

        public Name(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                throw new ParameterException("Name is required.");
            }

            _text = text.Trim();

            /*
             * remove multiple spaces separator to single space separator
             * example: "Albert   Brucelee" to "Albert Brucelee"
             * */
            _text = Regex.Replace(_text, " {2,}", " ");
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Name(string text)
        {
            return new Name(text);
        }

        public static implicit operator string(Name name)
        {
            return name._text;
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

            if (obj is Name)
            {
                return ((Name)obj)._text == _text;
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
