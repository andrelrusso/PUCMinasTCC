using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Repository.Repositories
{
    public class DespesaRepository:IDespesaRepository
    {
        private readonly IDbContext context;
        public DespesaRepository(IDbContext context)
        {
            this.context = context;
        }

        public void Gerenciar(Despesa value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.CNPJ == 0 || value.CNPJ == null) throw new ArgumentOutOfRangeException(nameof(value.CNPJ), "CNPJ obrigatório");
            context.InitProcedure("SpGerenciarDespesas");
            context.AddParameter("IdDespesa", value.IdDespesa, p => p != 0);
            context.AddParameter("MesAno", value.MesAno, p => !string.IsNullOrWhiteSpace(p));
            context.AddParameter("CNPJ", value.CNPJ, p => p != 0);
            context.AddParameter("ValorEmpenhado", value.ValorEmpenhado, p => p != 0);
            context.AddParameter("ValorLiquidado", value.ValorLiquidado, p => p != 0);
            context.AddParameter("ValorPago", value.ValorPago, p => p != 0);
            context.AddParameter("ValorRestosAPagarPagos", value.ValorPago, p => p != 0);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            value.IdDespesa = context.ExecuteScalar<int>();
        }

        public async Task<Despesa> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarDespesas");
            context.AddParameter("IdDespesa", id);
            return await context.GetAsync<Despesa>();
        }

        public async Task<IList<Despesa>> ToListAsync(Despesa filtro)
        {
            context.InitProcedure("SpBuscarDespesas");
            if (filtro != null)
            {
                context.AddParameter("IdDespesa", filtro.IdDespesa, p => p != 0);
                context.AddParameter("MesAno", filtro.MesAno, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("CNPJ", filtro.CNPJ, p => p != 0);

            }
            return await context.ListAsync<Despesa>();
        }


    }
}
