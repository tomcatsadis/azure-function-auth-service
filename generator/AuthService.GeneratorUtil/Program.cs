using System;
using TomcatSadis.Security.AccessTokenGenerator;

namespace AuthService.GeneratorUtil
{
    class Program
    {
        static void Main(string[] args)
        {
            GenerateRsaKeyPair();
        }

        /// <summary>
        /// Generate RSA Key Pair (Private and Public Key)
        /// example key pair generated:
        /// private key: MIIEpQIBAAKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQABAoIBAQCvHH1HMJu6oWhW0Xl9S2IWpl1VaS5mHLH9O+zezbGfxB9F7scOjFksZuq7vynu59J81SrJlUyrjXALviYhLClDVTfCCksjWzk+MAF0P4dBLFB5G7vk4LUMiBZEeqQtkgRJB61iffrOCMqcEPKkfXHtyajBSODVIhXQor5xT3H6oG5yBq3gU7Hjv/I0/fRS+apk8o1Kc0+vyH28ofjssFa2f7kHisQ4r8U0VEIHR3fSzfDPRgvCENLI/4p4Srosvo1s+8bumkFaT5Uu7WipgpCCTkir9qOIHs0tO2mx3yfL5l6OhoF0baRR0kBnOELMqfbcEvykFXf5lMaTlKoNkGilAoGBAM5BiJ4EQQQAnFjEfb5SdLkhr7yYSIvqG1WzrfIzZJXkRic263zd5zrOWJ0KjWxKrm4s+tx2h58tn4fyX88qU15o6JP7gfdn7fTROcDQDIClSwj1A87qr31vcbfdk4tZXMUY2NSpIyjXvrhEV5Tki6ltiRr/Z/XQSQRnBRPh8qPbAoGBAPmuaw/HclY365/n609HRIWsFiwP6SU6AChLYDF8Jy5h1Ay/z3bOnCUA2VyLac3N1S21J2F43x7r7ZIZndBHRqyG8PQ7eAi2xgBow2/+uYzbXqnV8xNPprEaait4srg4LXNr68BxkaB9pSKlQDjba9Sknh2zDCH4DPl6GRL76dnrAoGAVVuFoG9+QJvIGGxo5YTWTW+FfPVAwLaHzXXovN1L+URZTA1Mc4aaFRokTgl58aEOgwYWY3qiMdv9s5M411Fa4y1jYyqo5KycK78SuZrgMbU2UmSjyNQTAERIuaD3WKoI3ICl64x7woFWqoeN/05+BgYQwZ2FdpNWRH4l+c5+ThUCgYEA09ivskWxt4HfBGBNAYkglKJ8dYpScWmS9J1v0szambobp22f49hOF/9ubd1EOoCong2Uill6Rsw7WLkvc6bmSkB8dV137TzXJ0W8fWwNl3wSB9wmUH5GmwIkjxnr2e6gccxhzXkO7LNux8SahyN1jIjYN67RXokOySZV3AuuUV0CgYEAgZMsBbQW5uwEXkkgjYeINv+QCCcS9SSI7BS3UUjNOutZSNU0mFWgM1LWet9AWy5NIYWJF0mJWaUsGdv8G6e9MVtR4JrSEG7WLNaZjTKGKoUuxJlSa1NM7PEKhjFySTk9rgeTQdsGXhwZ/rFdQyIa17s0jaunvvviciRwQAqaz6o=
        /// public key: MIIBCgKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQAB
        /// </summary>
        static void GenerateRsaKeyPair()
        {
            var keyPair = RSAKeyPairGenerator.GenerateRSAKeyPair();

            Console.WriteLine("##### Generated RSA Key Pair #####");

            Console.WriteLine("-----BEGIN RSA PRIVATE KEY-----");
            Console.WriteLine(keyPair.PrivateKey);
            Console.WriteLine("-----END RSA PRIVATE KEY-----");

            Console.WriteLine();
            Console.WriteLine("-----BEGIN RSA PUBLIC KEY-----");
            Console.WriteLine(keyPair.PublicKey);
            Console.WriteLine("-----END RSA PUBLIC KEY-----");
        }
    }
}
