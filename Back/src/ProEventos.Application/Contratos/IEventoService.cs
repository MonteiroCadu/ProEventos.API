using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<EventoDto> Add(EventoDto eventoDto);
        Task<EventoDto> Update(int id, EventoDto eventoDto);
        Task<bool> Delete(int id);
        Task<EventoDto[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false);
        Task<EventoDto[]> GetAllAsync(bool includePalestrantes = false);
        Task<EventoDto> GetByIdAsync(int id, bool includePalestrantes = false);
    }
}