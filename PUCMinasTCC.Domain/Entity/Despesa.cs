using PUCMinasTCC.FrameworkDAO.Attributes;

namespace PUCMinasTCC.Domain.Entity
{
    public class Despesa:BaseEntity
    {
        public int IdDespesa { get; set; }

        [Column("MesAno")]
        public string MesAno { get; set; }

        [Column("CNPJ")]
        public string CNPJ { get; set; }

        [Column("ValorEmpenhado")]
        public float ValorEmpenhado { get; set; }

        [Column("ValorLiquidado")]
        public float ValorLiquidado { get; set; }

        [Column("ValorPago")]
        public float ValorPago { get; set; }

        [Column("ValorRestosAPagarPagos")]
        public float ValorRestosAPagarPagos { get; set; }
    }
}
