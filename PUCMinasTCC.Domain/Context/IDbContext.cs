using PUCMinasTCC.FrameworkDAO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace PUCMinasTCC.Domain.Context
{
    public interface IDbContext
    {
        IDataContext db { get; set; }
        void InitProcedure(string name);
        void InitProcedure(string name, DbTransaction transaction);
        void AddParameter<T>(string name, T value, Func<T, bool> condition);
        void AddParameter<T>(string name, T value);
        IList<T> List<T>() where T : class, new();
        Task<IList<T>> ListAsync<T>() where T : class, new();
        T Get<T>() where T : class, new();
        Task<T> GetAsync<T>() where T : class, new();
        void Execute();
        T ExecuteScalar<T>();
    }
}
