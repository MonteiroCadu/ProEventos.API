import { HttpClient } from '@angular/common/http';
import { Component, OnInit,TemplateRef  } from '@angular/core';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  modalRef?: BsModalRef;
  message?: string;

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public exibirImagem: boolean = true;
  private _filtroLista: string = '';

  constructor(
      private eventosService: EventoService,
      private modalService: BsModalService,
      private toastr: ToastrService,
      private spinner: NgxSpinnerService
  ) { }

  public get filtroLista() : string {
    return this._filtroLista;
  }
  public set filtroLista(value: string ) {
    this._filtroLista = value;
    this.eventosFiltrados = this._filtroLista ? this.filtrarEventos(this._filtroLista) : this.eventos;

  }
  filtrarEventos(filtrarPor: string) : Evento[] {

    filtrarPor = filtrarPor.toLocaleLowerCase();

    return this.eventos.filter(
       evento => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }


  setExibirImagem(){
    this.exibirImagem = !this.exibirImagem
  }

  ngOnInit(): void {
    this.spinner.show();
    this.getEventos();
    /** spinner starts on init */


    setTimeout(() => {
      /** spinner ends after 5 seconds */
      this.spinner.hide();
    }, 5000);
  }

  public getEventos(): void {
    this.eventosService.getEventos().subscribe(
      {
        next: (v) => {
              this.eventos = v,
              this.eventosFiltrados = v
            },
        error: (e) => {
              this.spinner.hide(),
              this.toastr.error(`Erro ao carregar os Eventos${e}`,'Erro!')
        },
        complete: () =>  this.spinner.hide()
      }

    );
  }

  openModal(template: TemplateRef<any>) : void{
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    this.message = 'Confirmed!';
    this.toastr.success('O Evento foi deletado com sucesso.', 'Deletado!');

    this.modalRef?.hide();
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }


}
