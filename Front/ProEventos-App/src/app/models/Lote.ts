import { Evento } from "./Evento";

export interface Lote {
   id: number;
   nome: string;
   preco: number;
   dataInicio?: Date;
   dataFim?: Date;
   qantidade: number;
   eventoId: number;
   evento: Evento;
}
