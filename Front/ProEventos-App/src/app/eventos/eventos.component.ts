import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public eventos: any = [];
  public exibirImagem: boolean = true;
  private _filtroLista: string = '';
  constructor(private http: HttpClient) { }

  public get filtroLista(){
    return this._filtroLista;
  }
  public set filtroLista(filtro:string ){
    this._filtroLista = filtro;
  }

  public filtrar(){

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
        next: (v) => this.eventos = v,
        error: (e) => console.error(e),
        complete: () => console.info('complete')
      }

    );
  }

}
