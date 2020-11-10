using AuthService.API.Services.Response;
using AuthService.Application.UseCases;
using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using TomcatSadis.Security.AccessTokenHandler;

namespace AuthService.API.Services
{
    public class ChangePasswordService
    {
        private readonly IChangePasswordUseCase _changePasswordUseCase;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public ChangePasswordService(IChangePasswordUseCase changePasswordUseCase, IAccessTokenProvider accessTokenProvider)
        {
            _changePasswordUseCase = changePasswordUseCase;
            _accessTokenProvider = accessTokenProvider;
        }

        [FunctionName("ChangePassword")]
        public async Task<IActionResult> SignInByEmailAsync(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "change-password")] HttpRequest req,
            ILogger log)
        {
            var accessToken = _accessTokenProvider.ValidateToken(req);

            if (accessToken.Status != AccessTokenStatus.Valid)
            {
                return new UnauthorizedResult();
            }

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var changePasswordRequest = JsonConvert.DeserializeObject<ChangePasswordRequest>(requestBody);

            var invalidParameters = new ChangePasswordRequest();

            Password oldPassword = null;
            Password newPassword = null;

            try
            {
                oldPassword = changePasswordRequest.OldPassword;
            }
            catch (ParameterException e)
            {
                invalidParameters.OldPassword = e.Message;
            }

            try
            {
                newPassword = changePasswordRequest.NewPassword;
            }
            catch (ParameterException e)
            {
                invalidParameters.NewPassword = e.Message;
            }

            if (oldPassword == null ||
                newPassword == null)
            {
                return new BadRequestObjectResult(
                    new ErrorParametersResponse
                    {
                        InvalidParametersObject = invalidParameters
                    });
            }

            try
            {
                await _changePasswordUseCase.ChangePassword(
                    userId: ObjectId.Parse(accessToken.Principal.GetUserId()),
                    oldPassword: oldPassword,
                    newPassword: newPassword);

                return new OkResult();
            }
            catch (DomainException e)
            {
                return new BadRequestObjectResult(
                    ErrorResponse.BadRequest(
                        message: e.Message
                    ));
            }
        }

        public class ChangePasswordRequest
        {
            [JsonProperty("oldPassword")]
            public string OldPassword { get; set; }

            [JsonProperty("newPassword")]
            public string NewPassword { get; set; }
        }
    }
}
