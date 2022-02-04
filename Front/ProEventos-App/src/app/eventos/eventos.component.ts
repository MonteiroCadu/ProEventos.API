import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public eventosFiltrados: any = [];

  public exibirImagem: boolean = true;
  private _filtroLista: string = '';
  constructor(private http: HttpClient) { }

  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value: string ) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;

  }
  filtrarEventos(filtrarPor: string) : any {

    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
       (evento: {tema:string}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }


  setExibirImagem(){
    this.exibirImagem = !this.exibirImagem
  }

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void{
    this.http.get('https://localhost:5001/api/Eventos').subscribe(
      {
        next: (v) => {
              this.eventos = v,
              this.eventosFiltrados = v
            },
        error: (e) => console.error(e),
        complete: () =>  console.info('complete')
      }

    );
  }

}
