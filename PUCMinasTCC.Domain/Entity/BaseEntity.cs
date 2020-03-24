using PUCMinasTCC.FrameworkDAO.Attributes;
using System;

namespace PUCMinasTCC.Domain.Entity
{
    public class BaseEntity
    {
        public virtual DateTime DataOperacao { get; set; }
        [Column("IdUsuarioOperacao")]
        public virtual int IdUsuarioOperacao { get; set; }
        [Column("NomeUsuario")]
        public  virtual string NomeUsuario { get; set; }
    }
}
