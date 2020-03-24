using PUCMinasTCC.Domain.Context;
using PUCMinasTCC.Domain.Entity;
using PUCMinasTCC.Domain.Enums;
using PUCMinasTCC.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Repository.Repositories
{
    public class NaoConformidadeRepository : INaoConformidadeRepository
    {
        private readonly IDbContext context;
        public NaoConformidadeRepository(IDbContext context)
        {
            this.context = context;
        }
        public void Gerenciar(NaoConformidade value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            context.InitProcedure("SpGerenciarNaoConformidades");
            context.AddParameter("IdNaoConformidade", value.IdNaoConformidade, p => p != 0);
            context.AddParameter("DescricaoNaoConformidade", value.Descricao, p => !string.IsNullOrWhiteSpace(p));
            context.AddParameter("IdOrigemNc", (int)value.OrigemNc, p => p != (int)enumOrigemNC.Todas);
            context.AddParameter("CodStatus", (int)value.Status);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            value.IdNaoConformidade = context.ExecuteScalar<int>();
        }

        public async Task<NaoConformidade> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarNaoConformidades");
            context.AddParameter("IdNaoConformidade", id);
            return await context.GetAsync<NaoConformidade>();
        }

        public async Task<IList<NaoConformidade>> ToListAsync(NaoConformidade filtro)
        {
            context.InitProcedure("SpBuscarNaoConformidades");
            if (filtro != null)
            {
                context.AddParameter("IdNaoConformidade", filtro.IdNaoConformidade, p => p != 0);
                context.AddParameter("DescricaoNaoConformidade", filtro.Descricao, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("IdOrigemNc", (int)filtro.OrigemNc, p => p != 0);
                context.AddParameter("CodStatus", (int)filtro.Status, p => p != (int)enumStatus.Todos);

            }
            return await context.ListAsync<NaoConformidade>();
        }
    }
}
