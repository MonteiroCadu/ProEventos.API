using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;
            
        }
        public async Task<Evento> Add(Evento model)
        {
            try
            {
                this._geralPersist.Add<Evento>(model);
                if(await this._geralPersist.SaveChangesAsync()) 
                    return await this.GetByIdAsync(model.Id,false);

                return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
       
        public async Task<Evento> Update(int id, Evento model)  
        {
            try
            {
               var evento = await this._eventoPersist.GetByIdAsync(id,false);
               if(evento == null) return null;

               model.Id = evento.Id;

               this._geralPersist.Update<Evento>(model);
               if(await this._geralPersist.SaveChangesAsync())
                    return model;

               return null;
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> Delete(int id)
        {
            try
            {
               var  evento = await this._eventoPersist.GetByIdAsync(id,false);
               if(evento == null) throw new Exception("Evento para delete não encontrado");
               

               this._geralPersist.Delete<Evento>(evento);
               return await this._geralPersist.SaveChangesAsync(); 
            }
            catch (System.Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllAsync(bool includePalestrantes = false)
        {
            try
            {
                return await this._eventoPersist.GetAllAsync(includePalestrantes);;            

            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false)
        {
             try
            {
                return await this._eventoPersist.GetAllByTemaAsync(tema,includePalestrantes);               

            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetByIdAsync(int id, bool includePalestrantes = false)
        {
             try
            {
                return await this._eventoPersist.GetByIdAsync(id,includePalestrantes);               

            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        
    }
}