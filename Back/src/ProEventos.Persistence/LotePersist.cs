
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Persistence
{
    public class LotePersist : ILotePersist
    {
        private readonly ProEventosContext _contexto;

        public LotePersist(ProEventosContext contexto)
        {
            this._contexto = contexto;
        }
        public async Task<Lote[]> GetAllByEventoIdAsync(int eventoId)
        {
           
                IQueryable<Lote> query = _contexto.Lotes
                   // .Include(lote => lote.Evento)
                    .Where(lote => lote.EventoId == eventoId)
                    .OrderBy(lote => lote.Id)
                    .AsNoTracking();

                return await query.ToArrayAsync();
        }

        public async Task<Lote> GetByIdsAsync(int eventoId, int id)
        {
            IQueryable<Lote> query = this._contexto.Lotes
                               // .Include(lote => lote.Evento)
                                .Where(lote => lote.EventoId == eventoId && lote.Id == id)
                                .AsNoTracking();
            return await query.FirstOrDefaultAsync();
        }
    }
}