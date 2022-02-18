using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProEventos.Domain
{
    //[Table("Evento")] //Nao obrigatorio, é utilizado quando o nome da tabela e diferente do noma da classe
    public class Evento
    {
        //[Key] // So é obrigatorio caso o nome do atributo nao seja ID
        public int Id {get;set;}
        
        public string Tema {get;set;} 
        public string Local {get;set;}
        
        public int QtdPessoas {get;set;}
        public DateTime? DataEvento {get;set;}
        public string ImageUrl {get;set;}
        public string Telefone { get; set; }
        public string Email { get; set; }

        [NotMapped]
        public int ContagemDias { get; set; }
        public IEnumerable<Lote> Lotes { get; set; }
        public IEnumerable<RedeSocial> RedesSociais { get; set; }
        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }

    }
}