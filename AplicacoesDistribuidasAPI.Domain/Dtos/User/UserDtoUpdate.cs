using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AplicacoesDistribuidasAPI.Domain.Dtos.User
{
    public class UserDtoUpdate
    {
        [Required(ErrorMessage = "Id é campo obrigátorio")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Nome é campo obrigátorio")]
        [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email é campo obrigatório")]
        [EmailAddress(ErrorMessage = "Emailem formato inválido")]
        public string Email { get; set; }
    }
}
