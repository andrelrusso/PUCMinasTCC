using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO.Attributes;
using System.ComponentModel;

namespace PUCMinasTCC.Domain.Entity
{
    public class Incidente : BaseEntity
    {
        [Column("IdIncidente")]
        public int? IdIncidente { get; set; }

        [Column("DescricaoIncidente")]
        public string Descricao { get; set; }

        //[Column("IdNaoConformidade")]
        //public int? IdNaoConformidade { get; set; }

        //[Column("IdNaoConformidade")]
        [DisplayName("Não Conformidade")]
        [EntityFromSameQuery]
        public NaoConformidade NaoConformidade  { get; set; }

        [Column("IdEstadoIncidente")]
        [DisplayName("Estado")]
        public enumEstadoIncidente EstadoIncidente { get; set; }
    }
}
