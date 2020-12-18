using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace AplicacoesDistribuidasAPI.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]

        public Guid Id { get; set; }

        public DateTime? UpdateAt { get; set; }

        private DateTime? _createAt;


        public DateTime? CreateAt
        {
            get { return _createAt; }
            set { _createAt = value == null ? DateTime.UtcNow : value; }
        }
    }
}
