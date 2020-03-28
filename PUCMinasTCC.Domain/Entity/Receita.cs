using PUCMinasTCC.FrameworkDAO.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PUCMinasTCC.Domain.Entity
{
    public class Receita:BaseEntity
    {
        [Column("IdDespesa")]
        public int? IdReceita { get; set; }

        [Column("Ano")]
        [DisplayFormat(DataFormatString  = "{0:0000}", ApplyFormatInEditMode = true)]
        [DisplayName("Ano(AAAA)")]
        public string Ano { get; set; }

        [Column("CNPJ")]
        [DisplayFormat(DataFormatString = "{0:00\\.000\\.000/0000-00}", ApplyFormatInEditMode = true)]
        public long? CNPJ { get; set; }

        [Column("OrcamentoAtualziado")]
        [DataType(DataType.Currency)]
        [DisplayName("Orçamento Atualziado(Valor Previsto")]
        public decimal? OrcamentoAtualziado { get; set; }

        [Column("ReceitaRealizada")]
        [DisplayName("Receita Realizada(Valor Arrecadado)")]
        [DataType(DataType.Currency)]
        public decimal? ReceitaRealizada { get; set; }

        [Column("PorcentagemPrevisto")]
        [DisplayName("% Previsto/Realizado")]
        [DataType(DataType.Currency)]
        public decimal? PorcentagemPrevisto { get; set; }

        
        [Column("ValorLancado")]
        [DisplayName("Valor Lançado")]
        [DataType(DataType.Currency)]
        public decimal? ValorLancado { get; set; }
    }
}
