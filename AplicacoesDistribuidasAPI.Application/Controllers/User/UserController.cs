using AplicacoesDistribuidasAPI.Domain.Dtos.User;
using AplicacoesDistribuidasAPI.Domain.Entities.Response;
using AplicacoesDistribuidasAPI.Domain.Entities.User;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Application.Controllers.User
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseBase<IEnumerable<UserDto>> responseBase = new ResponseBase<IEnumerable<UserDto>>();

                IEnumerable<UserDto> result = await _service.GetAll();

                result = result.OrderBy(order => order.Name);

                responseBase.Message = "Busca efetuada com sucesso!";
                responseBase.Result = result;
                responseBase.Success = true;

                return Ok(responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("{id}", Name = "GetUserById")]        
        public async Task<ActionResult> Get([FromServices] IUserService service, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseBase<UserDto> responseBase = new ResponseBase<UserDto>();
                UserDto result = await service.Get(id);
                if (result == null)
                {
                    responseBase.Message = "Não foi encontrado usuário";
                    responseBase.Success = false;
                    return NotFound(responseBase);
                }

                responseBase.Message = "Busca realizada com sucesso!";
                responseBase.Result = result;
                responseBase.Success = true;

                return Ok(responseBase);
            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserDtoCreate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserDtoCreateResult result = await _service.Post(user);

                if (result == null)
                    return BadRequest();

                ResponseBase<UserDtoCreateResult> responseBase = new ResponseBase<UserDtoCreateResult>();
                responseBase.Message = "Usuário criado com sucesso!";
                responseBase.Success = true;
                responseBase.Result = result;

                return Created(new Uri(Url.Link("GetUserById", new { id = result.Id })), responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [Authorize(Roles = "Editor")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserDtoUpdate user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserDtoUpdateResult result = await _service.Put(user);

                if (result == null)
                    return BadRequest();

                ResponseBase<UserDtoUpdateResult> responseBase = new ResponseBase<UserDtoUpdateResult>();
                responseBase.Message = "Usuário alterado com sucesso!";
                responseBase.Success = true;
                responseBase.Result = result;


                return Ok(responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        [Authorize("Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                bool result = await _service.Delete(id);

                ResponseBase<bool> responseBase = new ResponseBase<bool>();
                responseBase.Message = "Usuário deletado com sucesso!";
                responseBase.Success = result;
                responseBase.Result = result;


                return Ok(responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

    }
}
