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


        /// <summary>
        /// Lista todos os usuários do sistema - Necessita de autenticação.
        /// </summary>
        /// <returns>Os usuários</returns>
        /// <response code="200">Retorna os usuários cadastrados</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [Authorize(Roles = "Editor")]
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

        /// <summary>
        /// Obtêm um usuário do sistema pelo Id - Necessita de autenticação.
        /// </summary>
        /// <returns>Um usuário</returns>
        /// <response code="200">Retorna um usuário cadastrado obtido pelo Id</response>        
        /// <response code="401">Não autorizado</response>
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



        /// <summary>
        /// Cria um usuário no sistema - Não necessita de autenticação.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST /User
        ///     {        
        ///        "name": "João",
        ///        "email": "joao@gmail.com"
        ///     }
        ///
        /// </remarks>
        /// <param name="user"></param>
        /// <returns>Um novo item criado</returns>
        /// <response code="201">Usuário criado com sucesso</response>
        /// <response code="400">Se o usuário não for criado</response>        
        /// <response code="500">Erro interno</response>
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




        /// <summary>
        /// Edita um usuário do sistema - Necessita de autenticação.
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     PUT /User
        ///     {        
        ///        "Id": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
        ///        "name": "João",
        ///        "email": "joao@gmail.com"
        ///     }
        ///
        /// </remarks>
        /// <returns>Edição de um usuário</returns>
        /// <param name="user"></param>
        /// <response code="200">Retorna um usuário editado com sucesso</response>             
        /// <response code="401">Não autorizado</response>
        /// <response code="500">Erro interno</response>
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



        /// <summary>
        /// Deleta um usuário do sistema pelo Id - Necessita de autenticação.
        /// </summary>
        /// <returns>Usuário deletado</returns>
        /// <param name="id"></param>
        /// <response code="200">Usuário deletado com sucesso</response>     
        /// <response code="401">Não autorizado</response>
        /// <response code="500">Erro interno</response>
        [Authorize(Roles = "Editor")]
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
