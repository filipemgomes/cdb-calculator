import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';

export interface SimulateRequest {
  valorInicial: number;
  prazoMeses: number;
}

export interface SimulateResponse {
  resultadoBruto: number;
  resultadoLiquido: number;
}

export interface ValidationErrorResponse {
  erros: string[];
}

@Injectable({ providedIn: 'root' })
export class CdbService {
  private readonly http = inject(HttpClient);
  private readonly endpoint = '/api/cdb/simular';

  simulate(request: SimulateRequest): Observable<SimulateResponse> {
    return this.http
      .post<SimulateResponse>(this.endpoint, request)
      .pipe(catchError((error: HttpErrorResponse) => throwError(() => error)));
  }
}
