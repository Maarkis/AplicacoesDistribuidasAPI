using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AplicacoesDistribuidasAPI.Domain.Dtos.Authentication
{
    public class AuthenticationDto
    {
        [Required(ErrorMessage = "Email é obrigátorio para login")]
        [EmailAddress(ErrorMessage = "Email é inválido")]
        public string Email { get; set; }
    }
}
