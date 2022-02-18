using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface ILoteService    {
        
        Task<LoteDto[]> Save(int eventoId, LoteDto[] LotesDto);
        Task<bool> Delete(int eventoId, int id);
        Task<LoteDto[]> GetAllByEventoIdAsync(int eventoId);        
        Task<LoteDto> GetByIdsAsync(int eventoId, int id);
    }
}