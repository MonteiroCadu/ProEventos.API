using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IEventoPersist
    {
        Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false);
        Task<Evento[]> GetAllAsync(bool includePalestrantes = false);
        Task<Evento> GetByIdAsync(int id, bool includePalestrantes = false);
    }
}