using System;

namespace AplicacoesDistribuidasAPI.Domain.Entities
{
    public class ProductEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public int Amount { get; set; }

        public static implicit operator ProductEntity(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
