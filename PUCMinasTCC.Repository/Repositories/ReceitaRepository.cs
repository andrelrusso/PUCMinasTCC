using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Repository.Repositories
{
    public class ReceitaRepository:IReceitaRepository
    {
        private readonly IDbContext context;
        public ReceitaRepository(IDbContext context)
        {
            this.context = context;
        }

        public void Gerenciar(Receita value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.CNPJ == 0 || value.CNPJ == null) throw new ArgumentOutOfRangeException(nameof(value.CNPJ), "CNPJ obrigatório");
            context.InitProcedure("SpGerenciarReceitas");
            context.AddParameter("IdReceita", value.IdReceita, p => p != 0);
            context.AddParameter("Ano", value.Ano, p => !string.IsNullOrWhiteSpace(p));
            context.AddParameter("CNPJ", value.CNPJ, p => p != 0);
            context.AddParameter("OrcamentoAtualizado", value.OrcamentoAtualizado, p => p != 0);
            context.AddParameter("ReceitaRealizada", value.ReceitaRealizada, p => p != 0);
            context.AddParameter("PorcentagemPrevisto", value.PorcentagemPrevisto, p => p != 0);
            context.AddParameter("ValorLancado", value.ValorLancado, p => p != 0);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            value.IdReceita = context.ExecuteScalar<int>();
        }

        public async Task<Receita> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarReceitas");
            context.AddParameter("IdReceita", id);
            return await context.GetAsync<Receita>();
        }

        public async Task<IList<Receita>> ToListAsync(Receita filtro)
        {
            context.InitProcedure("SpBuscarReceitas");
            if (filtro != null)
            {
                context.AddParameter("IdReceita", filtro.IdReceita, p => p != 0);
                context.AddParameter("Ano", filtro.Ano, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("CNPJ", filtro.CNPJ, p => p != 0);

            }
            return await context.ListAsync<Receita>();
        }


    }
}
