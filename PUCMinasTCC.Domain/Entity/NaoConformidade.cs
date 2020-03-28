using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO.Attributes;
using System.ComponentModel;

namespace PUCMinasTCC.Domain.Entity
{
    public class NaoConformidade:BaseEntity
    {
        [Column("IdNaoConformidade")]
        public int? IdNaoConformidade { get; set; }

        [Column("DescricaoNaoConformidade")]
        public string Descricao { get; set; }

        [Column("IdOrigemNc")]
        [DisplayName("Origem")]
        public enumOrigemNC OrigemNc { get; set; }

        [Column("CodStatus")]
        public enumStatus Status { get; set; } = enumStatus.Todos;
    }
}
