using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Application.Contratos
{
    public interface IEventoService
    {
        Task<Evento> Add(Evento model);
        Task<Evento> Update(int id, Evento model);
        Task<bool> Delete(int id);
        Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllAsync(bool includePalestrantes = false);
        Task<Evento> GetByIdAsync(int id, bool includePalestrantes = false);
    }
}