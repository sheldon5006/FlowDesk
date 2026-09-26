import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateShift, Shift } from '../../../models/shift.model';

@Injectable({
  providedIn: 'root'
})
export class ShiftService {

  private readonly apiUrl = 'https://localhost:7113/api/shifts';

  constructor(
  private readonly http: HttpClient
    ) 
    {}

  getAll(): Observable<Shift[]> {
    return this.http.get<Shift[]>(this.apiUrl);
  }

  getById(id: number): Observable<Shift> {
    return this.http.get<Shift>(`${this.apiUrl}/${id}`);
  }

  create(shift: CreateShift): Observable<Shift> {
    return this.http.post<Shift>(this.apiUrl, shift);
  }

  publish(id: number): Observable<void> {
    return this.http.put<void>(
        `${this.apiUrl}/${id}/publish`,
        {}
    );
    }

    complete(id: number): Observable<void> {
        return this.http.put<void>(
            `${this.apiUrl}/${id}/complete`,
            {}
        );
    }

    cancel(id: number): Observable<void> {
        return this.http.put<void>(
            `${this.apiUrl}/${id}/cancel`,
            {}
        );
    }
}