using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface IPalestrantePersist
    {
        Task<Palestrante[]> GetAllByNomeAsync(string nome, bool includeEventos);
        Task<Palestrante[]> GetAllAsync(bool includeEventos);
        Task<Palestrante> GetByIdAsync(int id, bool includeEventos);
    }
}