using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _contexto;
        public PalestrantePersist(ProEventosContext contexto)
        {
            this._contexto = contexto;
            
        }
        public async Task<Palestrante[]> GetAllAsync(bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
            .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id)
                         .AsNoTracking();

            return await query.ToArrayAsync();         

        }

        public async Task<Palestrante[]> GetAllByNomeAsync(string nome, bool includeEventos)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
            .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()))
                         .AsNoTracking();

            return await query.ToArrayAsync(); 
        }

        public async Task<Palestrante> GetByIdAsync(int id, bool includeEventos)
        {
            IQueryable<Palestrante> query = _contexto.Palestrantes
            .Include(p => p.RedesSociais);

            if(includeEventos) {
                query = query.Include(p => p.PalestranteEventos)
                             .ThenInclude(pe => pe.Evento);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Id == id)
                         .AsNoTracking();

            return await query.FirstOrDefaultAsync(); 
        }
    }
}