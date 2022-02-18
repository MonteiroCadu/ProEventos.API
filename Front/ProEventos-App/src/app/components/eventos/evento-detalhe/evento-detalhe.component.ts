import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { ValidatorField } from 'src/app/helpers/ValidatorField';
import { Evento } from 'src/app/models/Evento';
import { EventoService } from 'src/app/services/evento.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  public form! : FormGroup;
  message?: string;
  modalRef?: any;
  evento = {} as  Evento;
  modoEdicao = false;
  public eventoId = '';

    constructor(
      private fb : FormBuilder,
      private modalService  : BsModalService,
      private localeService : BsLocaleService,
      private activatedRouter: ActivatedRoute,
      private router  :Router,
      private eventoService: EventoService,
      private toastr: ToastrService,
      private spinner: NgxSpinnerService
    ) {
      this.localeService.use('pt-br');
    }

    ngOnInit(): void {
      this.carregarEvento();
      this.validation();
    }

    public carregarEvento(): void {
      let eventoIdParam = this.activatedRouter.snapshot.paramMap.get('id');
      //eventoIdParam = this.eventoId;
      if(eventoIdParam !== null) {
        this.spinner.show();
        this.modoEdicao = true;
        this.eventoId = eventoIdParam;

        this.eventoService.getEventoById(+eventoIdParam).subscribe(
          {
            next: (evento :Evento) => {
                  this.evento = {... evento};
                  this.form.patchValue(this.evento);
                },
            error: (e) => {
              this.spinner.hide();
              this.toastr.error("Erro ao carregar o Evento","Erro!")
            },
            complete: () =>  {
              this.spinner.hide();
            }
          }
        );
      }
    }

    public salvarEvento(): void {
      if(!this.form.valid) {
        return;
      }

      this.evento = this.modoEdicao
                  ?  { id:this.eventoId, ...this.form.value}
                  : {...this.form.value};

      this.spinner.show();
      this.eventoService.salvar(this.evento,this.modoEdicao).subscribe({
        next: (evento :Evento) => {
          this.evento = {... evento};
          id:this.eventoId = evento.id.toString();
          this.toastr.success("Evento salvo","Sucesso");
       },
        error: (e) => {
          this.toastr.error(e,"Erro!")
        }
      }).add(() => {this.spinner.hide()});
      //console.log(`/eventos/detalhe/${this.evento.id}`);
      //if(!this.modoEdicao) this.router.navigate([`/eventos/detalhe/${{eventoId}}`]);
    }

    public validation() : void {
      this.form = this.fb.group({
        tema :  ["",[Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
        local : ["",[Validators.required, Validators.minLength(4),Validators.maxLength(50)]],
        qtdPessoas : ["",[Validators.required, Validators.max(1000)]],
        dataEvento : ["",Validators.required],
        imageUrl :   ["",Validators.required],
        telefone :   ["",Validators.required],
        email :      ["",[Validators.required, Validators.email]]
      });
    }
    get f(): any {
      return this.form.controls;
    }

    get bsConfig() : any {
      return{
        isAnimated: true,
        adaptivePosition: true,
        dateInputFormat: 'DD/MM/YYYY hh:mm a',
        containerClass:'theme-default',
        showWeekNumbers: false
      }
    }


    openModalResetForm(template: TemplateRef<any>) : void{
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }

    confirmResetForm(): void {
      this.modalRef?.hide();

      if(this.modoEdicao) {
        this.carregarEvento();
      } else {
        this.form.reset();
      }

    }

    declineResetForm(): void {
      this.message = 'Declined!';
      this.modalRef?.hide();
    }

    public cssValidator(campo:FormControl) : any {
      return ValidatorField.cssValidator(campo);
    }
}
