using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProEventos.Application.Dtos
{
    public class EventoDto
    {
        
        public int Id {get;set;}
        
        [Required, StringLength(50, MinimumLength = 4)]
        public string Tema {get;set;} 
        
        [Required, StringLength(50, MinimumLength = 4)]
        public string Local {get;set;}  


        [
            Display(Name ="QTD Pessoas"),
            Required,
            Range(1,1000)
        ]
        public int QtdPessoas {get;set;}
        [Required]
        public string DataEvento {get;set;}
        
        [Required]
        public string ImageUrl {get;set;}
        [
            Required,
            Phone
        ]
        public string Telefone { get; set; }
        
        [
            Display(Name ="E-mail"),
            Required(ErrorMessage ="Campo Obrigatório"),
            EmailAddress(ErrorMessage ="E-mail inválido")
        ]        
        public string Email { get; set; }

        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}