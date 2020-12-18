using AplicacoesDistribuidasAPI.Domain.Entities;
using AplicacoesDistribuidasAPI.Domain.Entities.Response;
using AplicacoesDistribuidasAPI.Domain.Interfaces.Services.Product;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Application.Controllers.Product
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices] IProductService service)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseBase<IEnumerable<ProductEntity>> responseBase = new ResponseBase<IEnumerable<ProductEntity>>();
                var result = await service.GetAll();

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
    }
}
