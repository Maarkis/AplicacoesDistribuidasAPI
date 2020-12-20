using System;
using System.Collections.Generic;
using System.Text;

namespace AplicacoesDistribuidasAPI.Domain.Entities.User
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        //public string CPF { get; set; }
        public string Email { get; set; }
    }
}
