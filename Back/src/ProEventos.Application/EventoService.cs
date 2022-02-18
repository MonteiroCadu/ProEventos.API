using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using AutoMapper;

namespace ProEventos.Application
{
    public class EventoService : IEventoService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;
        private readonly IMapper _mapper;
        public EventoService(IGeralPersist geralPersist,IEventoPersist eventoPersist, IMapper mapper)
        {
            this._mapper = mapper;
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;
            
        }
        public async Task<EventoDto> Add(EventoDto eventoDto)
        {
            try
            {
                Evento model = _mapper.Map<Evento>(eventoDto);

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
       
        public async Task<EventoDto> Update(int id, EventoDto eventoDto)  
        {
            Evento model = _mapper.Map<Evento>(eventoDto);            
            try
            {
               
               var evento = await this._eventoPersist.GetByIdAsync(id,false);
               if(evento == null) return null;

               model.Id = evento.Id;

               this._geralPersist.Update<Evento>(model);
               if(await this._geralPersist.SaveChangesAsync())
                    return await this.GetByIdAsync(id,false);

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

        public async Task<EventoDto[]> GetAllAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos =  await this._eventoPersist.GetAllAsync(includePalestrantes);
                var eventosDtoRetorno = this._mapper.Map<EventoDto[]>(eventos);
                return eventosDtoRetorno; 
            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto[]> GetAllByTemaAsync(string tema, bool includePalestrantes = false)
        {
             try
            {
                var eventos = await this._eventoPersist.GetAllByTemaAsync(tema,includePalestrantes);
                var eventosDtoRetorno = this._mapper.Map<EventoDto[]>(eventos);
                return eventosDtoRetorno;               

            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public async Task<EventoDto> GetByIdAsync(int id, bool includePalestrantes = false)
        {
             try
            {
                var evento = await this._eventoPersist.GetByIdAsync(id,includePalestrantes);
                var eventoDtoRetorno = this._mapper.Map<EventoDto>(evento);
                return eventoDtoRetorno;               

            }
            catch (System.Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }
        
    }
}