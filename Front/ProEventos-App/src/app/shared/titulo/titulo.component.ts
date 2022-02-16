import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {

  @Input() tituloName = '';
  @Input() subTitulo  = 'Desde 2022';
  @Input() iconClass  = 'fa fa-user';
  @Input() botaoListar = false;

  constructor(private router:Router) { }

  ngOnInit(): void {
  }

  listar(): void {
    this.router.navigate([`/${this.tituloName.toLocaleLowerCase()}/lista`]);
  }

}
