using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PUCMinasTCC.Domain.Entity
{
    public class Usuario : BaseEntity
    {
        public int IdUsuario { get; set; }

        [Column("NomeUsuario")]
        public string Nome { get; set; }
        public string Login { get; set; }
        public long CPF { get; set; }
        public DateTime DataCadastro { get; set; }
        public string DescSenha { get; set; }

        [DisplayName("Validade Senha")]
        public DateTime ValidadeSenha { get; set; }
        public string Email { get; set; }

        [Column("IdPerfilUsuario")]
        public enumPerfilUsuario PerfilUsuario { get; set; } = enumPerfilUsuario.Todos;

        [Column("CodStatus")]
        public enumStatus Status { get; set; } = enumStatus.Todos;

        [Column("IdUsuarioCadastro")]
        public override int IdUsuarioOperacao { get => base.IdUsuarioOperacao; set => base.IdUsuarioOperacao = value; }
    }
}
