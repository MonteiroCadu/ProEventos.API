using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly ILoteService _loteService;
        public LotesController(ILoteService loteService)
        {
            this._loteService = loteService;
            
        }

        [HttpGet("{eventoId}")]
        public async Task<IActionResult> Get(int eventoId) {
             try
            {
                var lotes = await _loteService.GetAllByEventoIdAsync(eventoId); 
                if(lotes == null) return NoContent();

                return Ok(lotes);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar lotes. Erro:{ex.Message}");
            }
        }

         [HttpGet("{eventoId}/{id}")]
        public async Task<IActionResult> GetByIds(int eventoId,int id) {
             try
            {
                var lote = await _loteService.GetByIdsAsync(eventoId,id);
                if(lote == null) return NoContent();

                return Ok(lote);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar lotes. Erro:{ex.Message}");
            }
        }

        [HttpPut("{eventoId}")]
        public async Task<IActionResult> Save(int eventoId,LoteDto[] lotes) {
             try
            {
                var lotesRetorno = await _loteService.Save(eventoId,lotes);
                if(lotesRetorno == null) return NoContent();

                return Ok(lotesRetorno);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar lotes. Erro:{ex.Message}");
            }
        }

        [HttpDelete("{eventoId}/{id}")]
        public async Task<IActionResult> Delete(int eventoId,int id) {
             try
            {
                var loteRetorno = this._loteService.GetByIdsAsync(eventoId,id);
                if (loteRetorno == null) return NoContent();

                return await _loteService.Delete(eventoId,id)
                        ? Ok(new  {message = "Deletado"}) 
                        : throw new System.Exception($"Erro não espessifico ao tentar deletar o lote Id: {id}");

                 
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar deletar lote. Erro:{ex.Message}");
            }
        }
    }
}