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
        private IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }
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

                IEnumerable<ProductEntity> result = await _service.GetAll();

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

        [HttpGet]
        [Route("{id}", Name = "GetById")]
        public async Task<ActionResult> Get([FromServices] IProductService service, Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ResponseBase<ProductEntity> responseBase = new ResponseBase<ProductEntity>();
                ProductEntity result = await service.Get(id);

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

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductEntity product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ProductEntity result = await _service.Post(product);

                if (result == null)
                    return BadRequest();

                ResponseBase<ProductEntity> responseBase = new ResponseBase<ProductEntity>();
                responseBase.Message = "Produto criado com sucesso!";
                responseBase.Success = true;
                responseBase.Result = result;

                return Created(new Uri(Url.Link("GetById", new { id = result.Id })), responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }

        }
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] ProductEntity product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                ProductEntity result = await _service.Put(product);

                if (result == null)
                    return BadRequest();

                ResponseBase<ProductEntity> responseBase = new ResponseBase<ProductEntity>();
                responseBase.Message = "Produto alterado com sucesso!";
                responseBase.Success = true;
                responseBase.Result = result;


                return Ok(responseBase);

            }
            catch (ArgumentException e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

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
                responseBase.Message = "Produto apagado com sucesso!";
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
