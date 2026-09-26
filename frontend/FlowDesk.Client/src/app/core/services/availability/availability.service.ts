import { inject, Injectable } from '@angular/core';
import { Availability, CreateAvailability } from '../../../models/availability.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})

export class AvailabilityService {
  private http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7113/api/employees';

  getByEmployee(employeeId: number): Observable<Availability[]> {
    return this.http.get<Availability[]>(
      `${this.apiUrl}/${employeeId}/availability`
    );
  }

  create(
    employeeId: number,
    availability: CreateAvailability
  ): Observable<Availability> {
    return this.http.post<Availability>(
      `${this.apiUrl}/${employeeId}/availability`,
      availability
    );
  }

  delete(
    employeeId: number,
    availabilityId: number
  ): Observable<void> {
    return this.http.delete<void>(
      `${this.apiUrl}/${employeeId}/availability/${availabilityId}`
    );
  }
}
