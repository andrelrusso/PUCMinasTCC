using PUCMinasTCC.FrameworkDAO.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUCMinasTCC.Domain.Entity
{
    public class Despesa:BaseEntity
    {
        [Column("IdDespesa")]
        public int? IdDespesa { get; set; }

        [Column("MesAno")]
        [DisplayFormat(DataFormatString  = "{0:00/0000}", ApplyFormatInEditMode = true)]
        [DisplayName("Mês(MM)/Ano(AAAA)")]
        public string MesAno { get; set; }

        [Column("CNPJ")]
        [DisplayFormat(DataFormatString = "{0:00\\.000\\.000/0000-00}", ApplyFormatInEditMode = true)]
        public long? CNPJ { get; set; }

        [Column("ValorEmpenhado")]
        [DataType(DataType.Currency)]
        [DisplayName("Valor Empenhado")]
        public decimal? ValorEmpenhado { get; set; }

        [Column("ValorLiquidado")]
        [DisplayName("Valor Liquidado")]
        [DataType(DataType.Currency)]
        public decimal? ValorLiquidado { get; set; }

        [Column("ValorPago")]
        [DisplayName("Valor Pago")]
        [DataType(DataType.Currency)]
        public decimal? ValorPago { get; set; }

        [DisplayName("Valor Restante à Pagar")]
        [Column("ValorRestosAPagarPagos")]
        [DataType(DataType.Currency)]
        public decimal? ValorRestosAPagarPagos { get; set; }
    }
}
