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
    public class EventoPersist : IEventoPersist
    {
        private readonly ProEventosContext _contexto;
        public EventoPersist(ProEventosContext contexto)
        {
            this._contexto = contexto;
            
        }
        public async Task<Evento[]> GetAllAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if(includePalestrantes) {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query.OrderBy(e => e.Id)
                         .AsNoTracking();

            return await query.ToArrayAsync();                

        }

        public async Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais)
                .AsNoTracking();

            if(includePalestrantes) {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .OrderBy(e => e.Id)
                .Where(e => e.Tema.ToLower().Contains(tema.ToLower()))
                .AsNoTracking();

            return await query.ToArrayAsync();  
        }

        public async Task<Evento> GetByIdAsync(int id, bool includePalestrantes)
        {
            IQueryable<Evento> query = _contexto.Eventos
                .Include(e => e.Lotes)
                .Include(e => e.RedesSociais);

            if(includePalestrantes) {
                query = query.Include(e => e.PalestrantesEventos)
                             .ThenInclude(pe => pe.Palestrante);
            }

            query = query
                .OrderBy(e => e.Id)
                .Where(e => e.Id == id)
                .AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }
    }
}