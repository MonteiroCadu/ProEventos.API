
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {
        Task<Lote[]> GetAllByEventoIdAsync(int eventoId);        
        Task<Lote> GetByIdsAsync(int eventoId, int id);
    }
}