using System;
using System.Collections.Generic;
using System.Text;

namespace PUCMinasTCC.Domain.Entity
{
    public class BaseEntity
    {
        public virtual DateTime DataOperacao { get; set; }
        public virtual int IdUsuarioOperacao { get; set; }
    }
}
