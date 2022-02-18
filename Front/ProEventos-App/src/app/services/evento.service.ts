import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, take } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable()
export class EventoService {

  constructor(private http: HttpClient) { }
  private baseURL = 'https://localhost:5001/api/Eventos';

  public getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL).pipe(take(1));
  }

  public getEventosByTema(tema:string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${tema}/tema`).pipe(take(1));
  }

  public getEventoById(id:number): Observable<Evento> {
    return this.http.get<Evento>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public delete(id:string): Observable<any> {
    return this.http.delete<Evento>(`${this.baseURL}/${id}`).pipe(take(1));
  }

  public post(evento:Evento): Observable<Evento> {
    return this.http.post<Evento>(this.baseURL,evento).pipe(take(1));
  }

  public put(evento:Evento): Observable<Evento> {
    const id = evento?.id;
    return this.http.put<Evento>(`${this.baseURL}/${id}`,evento).pipe(take(1));
  }

  public salvar(evento:Evento, modoEdicao:boolean): Observable<Evento> {
    const id = evento?.id;

    return modoEdicao
          ? this.http.put<Evento>(`${this.baseURL}/${id}`,evento).pipe(take(1))
          :this.http.post<Evento>(`${this.baseURL}`,evento).pipe(take(1));
  }


}
