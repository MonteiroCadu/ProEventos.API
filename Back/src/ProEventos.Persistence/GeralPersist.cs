using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext _contexto;
        public GeralPersist(ProEventosContext contexto)
        {
            this._contexto = contexto;
            
        }
        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }

        public void DeleteRange<T>(T[] arrayEntity) where T : class
        {
            _contexto.RemoveRange(arrayEntity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }
    }
}