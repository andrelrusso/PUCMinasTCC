using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.FrameworkDAO.Attributes;

namespace PUCMinasTCC.Domain.Entity
{
    public class NaoConformidade:BaseEntity
    {
        [Column("IdNaoConformidade")]
        public int IdNaoConformidade { get; set; }

        [Column("DescricaoNaoConformidade")]
        public string DescricaoNaoConformidade { get; set; }

        [Column("CodStatus")]
        public enumStatus CodStatus { get; set; }
    }
}
