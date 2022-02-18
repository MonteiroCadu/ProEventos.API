import { Component, OnInit,TemplateRef } from '@angular/core';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { Router } from '@angular/router';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {


  modalRef?: BsModalRef;
  message?: string;
  public idEvento ='';

  public eventos: Evento[] = [];
  public eventosFiltrados: Evento[] = [];

  public exibirImagem: boolean = true;
  private _filtroLista: string = '';

  constructor(
      private eventosService: EventoService,
      private modalService: BsModalService,
      private toastr: ToastrService,
      private spinner: NgxSpinnerService,
      private router: Router
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

  public delete(id:string): void {
    this.eventosService.delete(id).subscribe(
      {
        next: (v) => {
          this.toastr.success(`Evento id:${id} deletado`,'Sucesso!')
        },
        error: (e) => {
              this.spinner.hide(),
              this.toastr.error(`Erro ao deletar o evento id:${id}`,'Erro!')
        },
        complete: () =>  this.spinner.hide()
      }

    );
  }
  openModal(event:any, template: TemplateRef<any>,id:any) : void{
    event.stopPropagation();
    this.idEvento = id;
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {
    //this.message = 'Confirmed!';
    this.modalRef?.hide();
    this.spinner.show();
    this.delete(this.idEvento);
  }

  decline(): void {
    this.message = 'Declined!';
    this.modalRef?.hide();
  }

  detalheEvento(id:number) :void{
    this.router.navigate([`eventos/detalhe/${id}`]);
  }

}
