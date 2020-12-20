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

        [Authorize("Bearer")]
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IUserService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseBase<IEnumerable<UserEntity>> responseBase = new ResponseBase<IEnumerable<UserEntity>>();

                IEnumerable<UserEntity> result = await _service.GetAll();

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

        [Authorize("Bearer")]
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
                ResponseBase<UserEntity> responseBase = new ResponseBase<UserEntity>();
                UserEntity result = await service.Get(id);

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

        [Authorize("Bearer")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                UserEntity result = await _service.Post(user);

                if (result == null)
                    return BadRequest();

                ResponseBase<UserEntity> responseBase = new ResponseBase<UserEntity>();
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

        [Authorize("Bearer")]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserEntity user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserEntity result = await _service.Put(user);

                if (result == null)
                    return BadRequest();

                ResponseBase<UserEntity> responseBase = new ResponseBase<UserEntity>();
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
