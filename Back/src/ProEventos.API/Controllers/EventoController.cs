
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        
       // private readonly ILogger<EventoController> _logger;

        public EventoController()
        {
            
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return new Evento[] {
                new Evento(){
                EventoId = 1,
                Tema = "Angular 11 e .NET",
                Local = "Belo horizonte",
                lote = "1o lote",
                QtdPessoas = 250,
                DataEvento = System.DateTime.Now.AddDays(2).ToString(),
                imageUrl = "foto.png"
                },
                new Evento(){
                EventoId = 2,
                Tema = "Angular 11 e .NET",
                Local = "Belo horizonte",
                lote = "1o lote",
                QtdPessoas = 250,
                DataEvento = System.DateTime.Now.AddDays(3).ToString(),
                imageUrl = "foto.png"
                }
            };
        }
        
        [HttpPost]
        public string Post()
        {
            return "Post";
        }

        [HttpPut("{id}")]
        public string Put(int id)
        {
            return $"Put id: {id}";
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            return $"Delete id: {id}";
        }
        
    }
}
