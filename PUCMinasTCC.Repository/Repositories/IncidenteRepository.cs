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
    public class IncidenteRepository : IIncidenteRepository
    {
        private readonly IDbContext context;
        public IncidenteRepository(IDbContext context)
        {
            this.context = context;
        }
        public void Gerenciar(Incidente value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.NaoConformidade?.IdNaoConformidade == 0) throw new ArgumentOutOfRangeException(nameof(value.NaoConformidade), "Selecione uma Não Conformidade");
            context.InitProcedure("SpGerenciarIncidentes");
            context.AddParameter("IdIncidente", value.IdIncidente, p => p != 0);
            context.AddParameter("DescricaoIncidente", value.Descricao, p => !string.IsNullOrWhiteSpace(p));
            context.AddParameter("IdNaoConformidade", value.NaoConformidade?.IdNaoConformidade, p => p != 0);
            context.AddParameter("IdUsuarioOperacao", value.IdUsuarioOperacao);
            context.AddParameter("IdEstadoIncidente", (int) value.EstadoIncidente, p => p != (int) enumEstadoIncidente.Todos);
            value.IdIncidente = context.ExecuteScalar<int>();
        }

        public async Task<Incidente> Get(int id)
        {
            if (id == 0) throw new ArgumentNullException(nameof(id));
            context.InitProcedure("SpBuscarIncidentes");
            context.AddParameter("IdIncidente", id);
            return await context.GetAsync<Incidente>();
        }

        public async Task<IList<Incidente>> ToListAsync(Incidente filtro)
        {
            context.InitProcedure("SpBuscarIncidentes");
            if (filtro != null)
            {
                context.AddParameter("IdIncidente", filtro.IdIncidente, p => p != 0);
                context.AddParameter("DescricaoIncidente", filtro.Descricao, p => !string.IsNullOrWhiteSpace(p));
                context.AddParameter("IdNaoConformidade", filtro.NaoConformidade?.IdNaoConformidade, p => p != 0);
                context.AddParameter("IdEstadoIncidente", (int)filtro.EstadoIncidente, p => p != 0);
               
            }
            return await context.ListAsync<Incidente>();
        }
    }
}
