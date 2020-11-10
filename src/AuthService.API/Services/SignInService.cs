using AuthService.API.Services.Response;
using AuthService.Application.UseCases;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace AuthService.API.Services
{
    public class SignInService
    {
        private readonly ISignInUseCase _signInUseCase;

        public SignInService(ISignInUseCase signInUseCase)
        {
            _signInUseCase = signInUseCase;
        }

        [FunctionName("SignInByEmail")]
        public async Task<IActionResult> SignInByEmailAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sign-in-by-email")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var signInByEmailRequest = JsonConvert.DeserializeObject<SignInByEmailRequest>(requestBody);

            var invalidParameters = new SignInByEmailRequest();

            Email email = null;
            Password password = null;

            try
            {
                email = signInByEmailRequest.Email;
            }
            catch (ParameterException e)
            {
                invalidParameters.Email = e.Message;
            }

            try
            {
                password = signInByEmailRequest.Password;
            }
            catch (ParameterException e)
            {
                invalidParameters.Password = e.Message;
            }

            if (email == null ||
                password == null)
            {
                return new BadRequestObjectResult(
                    new ErrorParametersResponse
                    {
                        InvalidParametersObject = invalidParameters
                    });
            }

            try
            {
                var token = await _signInUseCase.SignInByEmail(
                    email: email,
                    password: password);

                return new OkObjectResult(TokenResponse.Load(token));
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }

        [FunctionName("SignInByPhone")]
        public async Task<IActionResult> SignInByPhoneAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sign-in-by-phone")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var signInByPhoneRequest = JsonConvert.DeserializeObject<SignInByPhoneRequest>(requestBody);

            var invalidParameters = new SignInByPhoneRequest();

            Phone phone = null;
            Password password = null;

            try
            {
                phone = signInByPhoneRequest.Phone;
            }
            catch (ParameterException e)
            {
                invalidParameters.Phone = e.Message;
            }

            try
            {
                password = signInByPhoneRequest.Password;
            }
            catch (ParameterException e)
            {
                invalidParameters.Password = e.Message;
            }

            if (phone == null ||
                password == null)
            {
                return new BadRequestObjectResult(
                    new ErrorParametersResponse
                    {
                        InvalidParametersObject = invalidParameters
                    });
            }

            try
            {
                var token = await _signInUseCase.SignInByPhone(
                    phone: phone,
                    password: password);

                return new OkObjectResult(TokenResponse.Load(token));
            }
            catch (UnauthorizedException)
            {
                return new UnauthorizedResult();
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }

        public class SignInByEmailRequest
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        public class SignInByPhoneRequest
        {
            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }
    }
}
