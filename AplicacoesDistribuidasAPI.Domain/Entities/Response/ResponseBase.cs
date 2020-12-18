using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace AplicacoesDistribuidasAPI.Domain.Entities.Response
{
    public class ResponseBase<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Result { get; set; }
    }
}
