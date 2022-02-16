import { Component, OnInit } from '@angular/core';
import {  AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from 'src/app/helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  public form! : FormGroup;
  public campoObrigatorioMSG = 'Campo Obrigatório';

  constructor(public fb:FormBuilder) { }
  formOptions: AbstractControlOptions = {
    validators: ValidatorField.MustMatch('senha','confirmeSenha')
  };

  ngOnInit(): void {
    this.validation();
  }

  private validation():void {
    this.form = this.fb.group({
      titulo        :["",[Validators.required]],
      primeiroNome  :["",[Validators.required,Validators.minLength(3),Validators.maxLength(20)]],
      ultimoNome    :["",[Validators.required,Validators.minLength(3),Validators.maxLength(50)]],
      email         :["",[Validators.required,Validators.email]],
      telefone      :["",[Validators.required,Validators.minLength(9),Validators.maxLength(13)]],
      funcao        :["",[Validators.required,]],
      //descricao     :["",[Validators.required,Validators.minLength(3),Validators.maxLength(20)]],
      senha         :["",[Validators.required,Validators.minLength(6),Validators.maxLength(10)]],
      confirmeSenha :["",[Validators.required,Validators.minLength(6),Validators.maxLength(10)]]
    },this.formOptions);
  }

  public maxLengthMSG(value:number) : string {
    return `Máximo de ${value} caracteries`;
  }
  public minLengthMSG(value:number) : string {
    return `Minimo de ${value} caracteries`;
  }

  get f():any {
    return this.form.controls;
  }

}
