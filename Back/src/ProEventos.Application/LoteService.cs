using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    
    public class LoteService : ILoteService
    {
        private readonly ILotePersist _lotePersist;
        private readonly IMapper _mapper;
        private readonly IGeralPersist _geralPersist;
        public LoteService(ILotePersist lotePersist,IGeralPersist geralPersist, IMapper mapper)
        {
            this._mapper        = mapper;
            this._lotePersist   = lotePersist; 
            this._geralPersist  = geralPersist;          
        }
        
         public async Task<LoteDto[]> Save(int eventoId, LoteDto[] LotesDto)
        {
            List<LoteDto> listaRetorno = new List<LoteDto>();
            try
            {
                foreach (var loteDto in LotesDto)
                {
                    var modelo = this._mapper.Map<Lote>(loteDto);                    
                    //Verifica se não tem id note, logo se trada de um lote novo, o mesmo deve ser adicionado.
                    if(loteDto.Id == 0){                        
                        modelo.EventoId = eventoId;    
                        this._geralPersist.Add<Lote>(modelo);                        

                    } else { //Lote ja cadastrado, logo o mesmo deve ser alterado.                                                
                        var loteConsulta = this._lotePersist.GetByIdsAsync(eventoId,loteDto.Id);
                        if(loteConsulta == null) 
                            throw new Exception($"Lote id: {loteDto.Id}, passado para alteração, não cadastrado para o evento id:{eventoId}");
                        
                        this._geralPersist.Update<Lote>(modelo);
                        
                    }
                    await this._geralPersist.SaveChangesAsync();                        
                    var dtoRetorno = this._mapper.Map<LoteDto>(modelo);
                    listaRetorno.Add(dtoRetorno);
                }

                return  this._mapper.Map<LoteDto[]>(listaRetorno);
            }
            catch (System.Exception  ex)
            {
                
                throw new Exception($"Erro ao salvar lotes, messagem do erro: { ex.Message} ");
            }

        }

        public async Task<bool> Delete(int eventoId, int id)
        {
            try
            {
                var dominio = await this._lotePersist.GetByIdsAsync(eventoId,id);
                this._geralPersist.Delete<Lote>(dominio);

                return await this._geralPersist.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        public async Task<LoteDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            var retonroModels = await this._lotePersist.GetAllByEventoIdAsync(eventoId);
            var listaDtoRetorno = this._mapper.Map<LoteDto[]>(retonroModels);

            return listaDtoRetorno;
        }

        public async Task<LoteDto> GetByIdsAsync(int eventoId, int id)
        {
            var model = await this._lotePersist.GetByIdsAsync(eventoId,id);
            var dtoRetorno = this._mapper.Map<LoteDto>(model);

            return dtoRetorno;
        }

    }
}