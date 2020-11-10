using AuthService.Domain.Exceptions;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace AuthService.Domain.ValueObjects
{
    public sealed class Email
    {
        private string _text;

        public Email(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ParameterException("Email is required");

            text = text.Trim();

            if (!IsValidEmail(text))
            {
                throw new ParameterException("Not a valid email");
            }

            _text = text;
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return _text.ToString();
        }

        public static implicit operator Email(string text)
        {
            return new Email(text);
        }

        public static implicit operator string(Email email)
        {
            return email._text;
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

            if (obj is Email)
            {
                return ((Email)obj)._text == _text;
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
