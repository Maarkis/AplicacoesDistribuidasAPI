using AplicacoesDistribuidasAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AplicacoesDistribuidasAPI.Domain.Interfaces.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAll();
        Task<ProductEntity> Get(Guid id);
        Task<ProductEntity> Post(ProductEntity product);
        Task<ProductEntity> Put(ProductEntity product);
        Task<bool> Delete(Guid id);
    }
}
