using AuthService.Domain.Exceptions;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Phone
    {
        private string _text;

        public Phone(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ParameterException("Phone is required");

            try
            {
                var phoneNumber = PhoneNumbers.PhoneNumberUtil.GetInstance().Parse(text, null);
                _text = PhoneNumbers.PhoneNumberUtil.GetInstance().Format(phoneNumber, PhoneNumbers.PhoneNumberFormat.E164);
            }
            catch (PhoneNumbers.NumberParseException e)
            {
                throw new ParameterException(e.Message);
            }
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Phone(string text)
        {
            return new Phone(text);
        }

        public static implicit operator string(Phone name)
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

            if (obj is Phone)
            {
                return ((Phone)obj)._text == _text;
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
