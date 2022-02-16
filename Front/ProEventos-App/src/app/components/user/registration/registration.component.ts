import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from 'src/app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  public form! : FormGroup;
  public obrigatorioMSG = 'Campo obrigatório';
  formOptions: AbstractControlOptions = {
    validators: ValidatorField.MustMatch('senha','confirmeSenha')
  };

  constructor(public fb:FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }

  private validation() : void {
    this.form = this.fb.group({
        primeiroNome: ["",[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        ultimoNome: ["",[Validators.required,Validators.minLength(3),Validators.maxLength(30)]],
        email: ["",[Validators.required,Validators.email]],
        userName: ["",[Validators.required,Validators.minLength(4),Validators.maxLength(10)]],
        senha: ["",[Validators.required,Validators.minLength(6)]],
        confirmeSenha: ["",[Validators.required,Validators.minLength(6)]]
    },this.formOptions);
  }

  get f(): any {
    return this.form.controls;
  }

}
