using AuthService.Domain.Entities;
using AuthService.Domain.Settings;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace AuthService.Domain.UnitTests.Util
{
    public static class TokenFactory
    {
        /*
         * Before we test the program, first we manually create the access token using this data:
         * JWT payload data:
         * {
         *   "sub": "5fa663d3a659c23543dcc156",
         *   "given_name": "Albert",
         *   "family_name": "Brucelee",
         *   "nbf": 1577836800,
         *   "exp": 1893456000,
         *   "iss": "SimpleChat.AuthService",
         *   "aud": "SimpleChat.AccessTokenHandler"
         * }
         * 
         * Private key: MIIEpQIBAAKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQABAoIBAQCvHH1HMJu6oWhW0Xl9S2IWpl1VaS5mHLH9O+zezbGfxB9F7scOjFksZuq7vynu59J81SrJlUyrjXALviYhLClDVTfCCksjWzk+MAF0P4dBLFB5G7vk4LUMiBZEeqQtkgRJB61iffrOCMqcEPKkfXHtyajBSODVIhXQor5xT3H6oG5yBq3gU7Hjv/I0/fRS+apk8o1Kc0+vyH28ofjssFa2f7kHisQ4r8U0VEIHR3fSzfDPRgvCENLI/4p4Srosvo1s+8bumkFaT5Uu7WipgpCCTkir9qOIHs0tO2mx3yfL5l6OhoF0baRR0kBnOELMqfbcEvykFXf5lMaTlKoNkGilAoGBAM5BiJ4EQQQAnFjEfb5SdLkhr7yYSIvqG1WzrfIzZJXkRic263zd5zrOWJ0KjWxKrm4s+tx2h58tn4fyX88qU15o6JP7gfdn7fTROcDQDIClSwj1A87qr31vcbfdk4tZXMUY2NSpIyjXvrhEV5Tki6ltiRr/Z/XQSQRnBRPh8qPbAoGBAPmuaw/HclY365/n609HRIWsFiwP6SU6AChLYDF8Jy5h1Ay/z3bOnCUA2VyLac3N1S21J2F43x7r7ZIZndBHRqyG8PQ7eAi2xgBow2/+uYzbXqnV8xNPprEaait4srg4LXNr68BxkaB9pSKlQDjba9Sknh2zDCH4DPl6GRL76dnrAoGAVVuFoG9+QJvIGGxo5YTWTW+FfPVAwLaHzXXovN1L+URZTA1Mc4aaFRokTgl58aEOgwYWY3qiMdv9s5M411Fa4y1jYyqo5KycK78SuZrgMbU2UmSjyNQTAERIuaD3WKoI3ICl64x7woFWqoeN/05+BgYQwZ2FdpNWRH4l+c5+ThUCgYEA09ivskWxt4HfBGBNAYkglKJ8dYpScWmS9J1v0szambobp22f49hOF/9ubd1EOoCong2Uill6Rsw7WLkvc6bmSkB8dV137TzXJ0W8fWwNl3wSB9wmUH5GmwIkjxnr2e6gccxhzXkO7LNux8SahyN1jIjYN67RXokOySZV3AuuUV0CgYEAgZMsBbQW5uwEXkkgjYeINv+QCCcS9SSI7BS3UUjNOutZSNU0mFWgM1LWet9AWy5NIYWJF0mJWaUsGdv8G6e9MVtR4JrSEG7WLNaZjTKGKoUuxJlSa1NM7PEKhjFySTk9rgeTQdsGXhwZ/rFdQyIa17s0jaunvvviciRwQAqaz6o=
         * 
         * the generated access token is:
         * eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjVmNzIzOGU4Nzc2Zjg4YzVjNTVmMjQ1OCIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJBbGJlcnQgQnJ1Y2VsZWUiLCJuYmYiOjE2MDE3ODQxNTgsImV4cCI6MTYzMjg4ODE1OCwiaXNzIjoiTWFrYW5LdXkuQXV0aFNlcnZpY2UiLCJhdWQiOiJNYWthbkt1eS5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.EKztbAMuEA1TP9NbgRI5XfT70_ROKnRbLPCfPACbiV3_RhnbM-76XodWVSOeNzpXul4PJj2aCoSmtY9Lw-dvtUUxzR5s_cDolfXFMDIrc4qs2bFVur-FK6_5Y5ay6ga4t-PBtVwSQQKvOsSV2v6EmfowBpLjrP3NweuCu4rejn0Kts3SVVAgDryL3UTJwBqEktNZGO7khe2IsnfUe17uXLLE-Pm2LZ5nWVOxybSzdjCyyVM8p8_2mSqM0wJqC0l-AIarB_RDDpd3u5lBo9AyEedDUDMe8Alg9T540XWxMacjb-1ZoQAJKqQQ2nSJXk-772W3sBwFUkVLNO2MUe4lJQ
         * 
         * In order for our program to generate the same access token,
         * we input the same payload data to our program.
         * 
         * "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier" is for user id
         * "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" is for user full name
         * nbf is jwt not before in seconds (count from 1970-01-01 00:00:00)
         * exp is jwt expires in seconds (count from 1970-01-01 00:00:00)
         * iss is jwt issuer
         * aud is jwt audience
         * 
         * */
        /*
         * Before we run this test, we manually generate the access token. 
         * We use https://jwt.io/ to generate it.
         * 
         * For example, the JWT payload data is:
         * {
         *   "sub": "5fa663d3a659c23543dcc156",
         *   "given_name": "Albert",
         *   "family_name": "Brucelee",
         *   "nbf": 1577836800,
         *   "exp": 1893456000,
         *   "iss": "SimpleChat.AuthService",
         *   "aud": "SimpleChat.AccessTokenHandler"
         * }
         * 
         * And the RSA key pair is:
         * Private Key: MIIEpQIBAAKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQABAoIBAQCvHH1HMJu6oWhW0Xl9S2IWpl1VaS5mHLH9O+zezbGfxB9F7scOjFksZuq7vynu59J81SrJlUyrjXALviYhLClDVTfCCksjWzk+MAF0P4dBLFB5G7vk4LUMiBZEeqQtkgRJB61iffrOCMqcEPKkfXHtyajBSODVIhXQor5xT3H6oG5yBq3gU7Hjv/I0/fRS+apk8o1Kc0+vyH28ofjssFa2f7kHisQ4r8U0VEIHR3fSzfDPRgvCENLI/4p4Srosvo1s+8bumkFaT5Uu7WipgpCCTkir9qOIHs0tO2mx3yfL5l6OhoF0baRR0kBnOELMqfbcEvykFXf5lMaTlKoNkGilAoGBAM5BiJ4EQQQAnFjEfb5SdLkhr7yYSIvqG1WzrfIzZJXkRic263zd5zrOWJ0KjWxKrm4s+tx2h58tn4fyX88qU15o6JP7gfdn7fTROcDQDIClSwj1A87qr31vcbfdk4tZXMUY2NSpIyjXvrhEV5Tki6ltiRr/Z/XQSQRnBRPh8qPbAoGBAPmuaw/HclY365/n609HRIWsFiwP6SU6AChLYDF8Jy5h1Ay/z3bOnCUA2VyLac3N1S21J2F43x7r7ZIZndBHRqyG8PQ7eAi2xgBow2/+uYzbXqnV8xNPprEaait4srg4LXNr68BxkaB9pSKlQDjba9Sknh2zDCH4DPl6GRL76dnrAoGAVVuFoG9+QJvIGGxo5YTWTW+FfPVAwLaHzXXovN1L+URZTA1Mc4aaFRokTgl58aEOgwYWY3qiMdv9s5M411Fa4y1jYyqo5KycK78SuZrgMbU2UmSjyNQTAERIuaD3WKoI3ICl64x7woFWqoeN/05+BgYQwZ2FdpNWRH4l+c5+ThUCgYEA09ivskWxt4HfBGBNAYkglKJ8dYpScWmS9J1v0szambobp22f49hOF/9ubd1EOoCong2Uill6Rsw7WLkvc6bmSkB8dV137TzXJ0W8fWwNl3wSB9wmUH5GmwIkjxnr2e6gccxhzXkO7LNux8SahyN1jIjYN67RXokOySZV3AuuUV0CgYEAgZMsBbQW5uwEXkkgjYeINv+QCCcS9SSI7BS3UUjNOutZSNU0mFWgM1LWet9AWy5NIYWJF0mJWaUsGdv8G6e9MVtR4JrSEG7WLNaZjTKGKoUuxJlSa1NM7PEKhjFySTk9rgeTQdsGXhwZ/rFdQyIa17s0jaunvvviciRwQAqaz6o=
         * Public Key: MIIBCgKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQAB
         * 
         * The generated access token is:
         * eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZmE2NjNkM2E2NTljMjM1NDNkY2MxNTYiLCJnaXZlbl9uYW1lIjoiQWxiZXJ0IiwiZmFtaWx5X25hbWUiOiJCcnVjZWxlZSIsIm5iZiI6MTU3NzgzNjgwMCwiZXhwIjoxODkzNDU2MDAwLCJpc3MiOiJTaW1wbGVDaGF0LkF1dGhTZXJ2aWNlIiwiYXVkIjoiU2ltcGxlQ2hhdC5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.NcTBDbcnD3oBBl5xSUwVufYyCee6ymlpAbgal0UEv707mubspl47tYbSFVYGTWmBaXHemJTJ-Q3KEWc3rbLwtkDj3eVTtyE6R0usf5aa7uacezPVp_P2iygGu7_myislfz2hE-d0YHCrOEH0yH3lCn-hMSSpQb5k0XBgHSFRSGXmtyVtPU84ffwOuFY5pT8myVfbvT2kEKbrpT1haR3oHfk0Cs2jhMNEPfmJzg4YZzQQdHng6kA9n0vqhtI1c898m7qcayXmxXXSGP5J9izdwULoHLabadLdwu1nIaXLlEWsH2cvWMdylI5sBMEup9QkYhc0GROogSwZIofW5fLcqw
         * 
         * So in this tests, we test our program to encode access token,
         * and check if access token generated by the program matches with expected access token above.
         * 
         * */
        private static readonly DateTime JwtStartDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static IEnumerable<object[]> Data()
        {
            return new List<object[]>
            {
                new object[] {
                    new AccessTokenSettings
                    {
                        PrivateKey = "MIIEpQIBAAKCAQEAySpErqgsPfd6dPU+wmNI6A2NQcYRM49wct4JGolOdZFmY3nr+7mwzSCItxfd7FgeyUr8mz7WeSQ71Lc4APrD46/VYnUnD5z5s5qHrBvIl9UuD22ywdSGqZ5k8QMDk890eXIu2VIMZOkBT0bBa8ztN4eEO8wLtkto96IxHnbZbCQWV7taXK+ERiLYA505huvmgjEqwYhSPhg0jC6F09Yli7weYFEzkAb+KiSQVRuFcJ9gAmJDbZ7GlIP8SlNeneinOt2zUnePtr5m+YRjiFdExLn0v9LaMuqBEgRy9OJnrbr28t2mF9yUTvG7u1qGNj1E2R9eByBM7T0p7vir8fQNCQIDAQABAoIBAQCvHH1HMJu6oWhW0Xl9S2IWpl1VaS5mHLH9O+zezbGfxB9F7scOjFksZuq7vynu59J81SrJlUyrjXALviYhLClDVTfCCksjWzk+MAF0P4dBLFB5G7vk4LUMiBZEeqQtkgRJB61iffrOCMqcEPKkfXHtyajBSODVIhXQor5xT3H6oG5yBq3gU7Hjv/I0/fRS+apk8o1Kc0+vyH28ofjssFa2f7kHisQ4r8U0VEIHR3fSzfDPRgvCENLI/4p4Srosvo1s+8bumkFaT5Uu7WipgpCCTkir9qOIHs0tO2mx3yfL5l6OhoF0baRR0kBnOELMqfbcEvykFXf5lMaTlKoNkGilAoGBAM5BiJ4EQQQAnFjEfb5SdLkhr7yYSIvqG1WzrfIzZJXkRic263zd5zrOWJ0KjWxKrm4s+tx2h58tn4fyX88qU15o6JP7gfdn7fTROcDQDIClSwj1A87qr31vcbfdk4tZXMUY2NSpIyjXvrhEV5Tki6ltiRr/Z/XQSQRnBRPh8qPbAoGBAPmuaw/HclY365/n609HRIWsFiwP6SU6AChLYDF8Jy5h1Ay/z3bOnCUA2VyLac3N1S21J2F43x7r7ZIZndBHRqyG8PQ7eAi2xgBow2/+uYzbXqnV8xNPprEaait4srg4LXNr68BxkaB9pSKlQDjba9Sknh2zDCH4DPl6GRL76dnrAoGAVVuFoG9+QJvIGGxo5YTWTW+FfPVAwLaHzXXovN1L+URZTA1Mc4aaFRokTgl58aEOgwYWY3qiMdv9s5M411Fa4y1jYyqo5KycK78SuZrgMbU2UmSjyNQTAERIuaD3WKoI3ICl64x7woFWqoeN/05+BgYQwZ2FdpNWRH4l+c5+ThUCgYEA09ivskWxt4HfBGBNAYkglKJ8dYpScWmS9J1v0szambobp22f49hOF/9ubd1EOoCong2Uill6Rsw7WLkvc6bmSkB8dV137TzXJ0W8fWwNl3wSB9wmUH5GmwIkjxnr2e6gccxhzXkO7LNux8SahyN1jIjYN67RXokOySZV3AuuUV0CgYEAgZMsBbQW5uwEXkkgjYeINv+QCCcS9SSI7BS3UUjNOutZSNU0mFWgM1LWet9AWy5NIYWJF0mJWaUsGdv8G6e9MVtR4JrSEG7WLNaZjTKGKoUuxJlSa1NM7PEKhjFySTk9rgeTQdsGXhwZ/rFdQyIa17s0jaunvvviciRwQAqaz6o=",
                        Issuer = "SimpleChat.AuthService",
                        Audience = "SimpleChat.AccessTokenHandler",
                        NotBefore = JwtStartDateTime.AddSeconds(1577836800),  //2020-01-01 00:00:00
                        Expires = JwtStartDateTime.AddSeconds(1893456000), //2030-01-01 00:00:00
                        ExpiresIn = 311040000 //10 years
                    },
                    User.Load(
                        id: ObjectId.Parse("5fa663d3a659c23543dcc156"),
                        firstName: "Albert",
                        lastName: "Brucelee",
                        passwordHash: "test123",
                        email: "test@gmail.com",
                        isEmailVerified: false,
                        lastLoginUsingEmail: DateTime.UtcNow,
                        phone: "+6281234567891",
                        isPhoneVerified: false,
                        lastLoginUsingPhone: DateTime.UtcNow,
                        signUpDate: DateTime.UtcNow,
                        claims: null
                    ),
                    "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiI1ZmE2NjNkM2E2NTljMjM1NDNkY2MxNTYiLCJnaXZlbl9uYW1lIjoiQWxiZXJ0IiwiZmFtaWx5X25hbWUiOiJCcnVjZWxlZSIsIm5iZiI6MTU3NzgzNjgwMCwiZXhwIjoxODkzNDU2MDAwLCJpc3MiOiJTaW1wbGVDaGF0LkF1dGhTZXJ2aWNlIiwiYXVkIjoiU2ltcGxlQ2hhdC5BY2Nlc3NUb2tlbkhhbmRsZXIifQ.NcTBDbcnD3oBBl5xSUwVufYyCee6ymlpAbgal0UEv707mubspl47tYbSFVYGTWmBaXHemJTJ-Q3KEWc3rbLwtkDj3eVTtyE6R0usf5aa7uacezPVp_P2iygGu7_myislfz2hE-d0YHCrOEH0yH3lCn-hMSSpQb5k0XBgHSFRSGXmtyVtPU84ffwOuFY5pT8myVfbvT2kEKbrpT1haR3oHfk0Cs2jhMNEPfmJzg4YZzQQdHng6kA9n0vqhtI1c898m7qcayXmxXXSGP5J9izdwULoHLabadLdwu1nIaXLlEWsH2cvWMdylI5sBMEup9QkYhc0GROogSwZIofW5fLcqw" //access token
                }
            };
        }

        /*
         * we use settings custom class, so we can mock the expires and the notBefore datetime
         * */
        private class AccessTokenSettings : IAccessTokenSettings
        {
            public string PrivateKey { get; set; }

            public string Issuer { get; set; }

            public string Audience { get; set; }

            public uint ExpiresIn { get; set; }

            public DateTime Expires { get; set; }

            public DateTime NotBefore { get; set; }

            public void ValidateAttributes() { }
        }
    }
}
