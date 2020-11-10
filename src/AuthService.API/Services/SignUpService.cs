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
    public class SignUpService
    {
        private readonly ISignUpUseCase _signUpUseCase;

        public SignUpService(ISignUpUseCase signUpUseCase)
        {
            _signUpUseCase = signUpUseCase;
        }

        [FunctionName("SignUpByEmail")]
        public async Task<IActionResult> SignUpByEmailAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sign-up-by-email")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var signUpByEmailRequest = JsonConvert.DeserializeObject<SignUpByEmailRequest>(requestBody);

            var invalidParameters = new SignUpByEmailRequest();

            Email email = null;
            Password password = null;
            Name firstName = null;
            Name lastName = null;

            try
            {
                email = signUpByEmailRequest.Email;
            }
            catch (ParameterException e)
            {
                invalidParameters.Email = e.Message;
            }

            try
            {
                password = signUpByEmailRequest.Password;
            }
            catch (ParameterException e)
            {
                invalidParameters.Password = e.Message;
            }

            try
            {
                firstName = signUpByEmailRequest.FirstName;
            }
            catch (ParameterException e)
            {
                invalidParameters.FirstName = e.Message;
            }

            try
            {
                lastName = signUpByEmailRequest.LastName;
            }
            catch (ParameterException e)
            {
                invalidParameters.LastName = e.Message;
            }

            if (email == null ||
                password == null ||
                firstName == null ||
                lastName == null)
            {
                return new BadRequestObjectResult(
                    new ErrorParametersResponse
                    {
                        InvalidParametersObject = invalidParameters
                    });
            }

            try
            {
                var user = await _signUpUseCase.SignUpByEmail(
                    email: email,
                    password: password,
                    firstName: firstName,
                    lastName: lastName);

                return new CreatedResult(
                    "", 
                    UserResponse.Load(user));
            }
            catch (ConflictException e)
            {
                return new ConflictObjectResult(
                    ErrorResponse.Conflict(
                        message: e.Message
                    ));
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }

        [FunctionName("SignUpByPhone")]
        public async Task<IActionResult> SignUpByPhoneAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "sign-up-by-phone")] HttpRequest req,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var signUpByPhoneRequest = JsonConvert.DeserializeObject<SignUpByPhoneRequest>(requestBody);

            var invalidParameters = new SignUpByPhoneRequest();

            Phone phone = null;
            Password password = null;
            Name firstName = null;
            Name lastName = null;

            try
            {
                phone = signUpByPhoneRequest.Phone;
            }
            catch (ParameterException e)
            {
                invalidParameters.Phone = e.Message;
            }

            try
            {
                password = signUpByPhoneRequest.Password;
            }
            catch (ParameterException e)
            {
                invalidParameters.Password = e.Message;
            }

            try
            {
                firstName = signUpByPhoneRequest.FirstName;
            }
            catch (ParameterException e)
            {
                invalidParameters.FirstName = e.Message;
            }

            try
            {
                lastName = signUpByPhoneRequest.LastName;
            }
            catch (ParameterException e)
            {
                invalidParameters.LastName = e.Message;
            }

            if (phone == null ||
                password == null ||
                firstName == null ||
                lastName == null)
            {
                return new BadRequestObjectResult(
                    new ErrorParametersResponse
                    {
                        InvalidParametersObject = invalidParameters
                    });
            }

            try
            {
                var user = await _signUpUseCase.SignUpByPhone(
                    phone: phone,
                    password: password,
                    firstName: firstName,
                    lastName: lastName);

                return new CreatedResult(
                    $"/users/{user.Id}",
                    UserResponse.Load(user));
            }
            catch (ConflictException e)
            {
                return new ConflictObjectResult(
                    ErrorResponse.Conflict(
                        message: e.Message
                    ));
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }

        public class SignUpByEmailRequest
        {
            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }
        }

        public class SignUpByPhoneRequest
        {
            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }

            [JsonProperty("firstName")]
            public string FirstName { get; set; }

            [JsonProperty("LastName")]
            public string LastName { get; set; }
        }
    }
}
