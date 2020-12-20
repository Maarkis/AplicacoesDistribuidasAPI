using AplicacoesDistribuidasAPI.Domain.Dtos.Authentication;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Application.Controllers.Authentication
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _serviceAuthentication;
        public AuthenticationController(IAuthenticationService serviceAuthentication)
        {
            _serviceAuthentication = serviceAuthentication;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Login([FromBody] AuthenticationDto authentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (authentication == null)
                return BadRequest();

            try
            {
                object result = await _serviceAuthentication.FindByLogin(authentication);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
    }
}
