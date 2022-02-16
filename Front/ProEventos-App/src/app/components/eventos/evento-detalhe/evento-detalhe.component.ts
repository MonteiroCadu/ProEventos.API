import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  public form! : FormGroup;
  message?: string;
  modalRef?: any;

    constructor(
      private fb : FormBuilder,
      private modalService: BsModalService,
    ) { }

    ngOnInit(): void {
    this.validation();
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


    openModalResetForm(template: TemplateRef<any>) : void{
      this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
    }

    confirmResetForm(): void {
      this.message = 'Confirmed!';
      this.modalRef?.hide();

      this.form.reset();
    }

    declineResetForm(): void {
      this.message = 'Declined!';
      this.modalRef?.hide();
    }
}
