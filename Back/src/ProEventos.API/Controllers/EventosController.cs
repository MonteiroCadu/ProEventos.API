﻿
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Data;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {

        // private readonly ILogger<EventoController> _logger;
        private readonly AppDbContext context;

        public EventosController(AppDbContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return context.Eventos;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get( int id)
        {
            return context.Eventos.Where(e => e.EventoId == id);
        }

        [HttpGet("buscaPorTema/{tema}")]
        public IEnumerable<Evento> Get( string tema)
        {
            return context.Eventos.Where(e => e.Tema == tema);
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
