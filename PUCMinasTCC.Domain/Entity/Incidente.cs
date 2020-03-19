using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO.Attributes;

namespace PUCMinasTCC.Domain.Entity
{
    public class Incidente : BaseEntity
    {
        [Column("IdIncidente")]
        public int IdIncidente { get; set; }

        [Column("DescricaoIncidente")]
        public string DescricaoIncidente { get; set; }

        [Column("IdNaoConformidade")]
        public int IdNaoConformidade { get; set; }

        [Column("IdNaoConformidade")]
        public NaoConformidade NaoConformidade  { get; set; }

        [Column("IdEstadoIncidente")]
        public enumEstadoIncidente EstadoIncidente { get; set; }
    }
}
