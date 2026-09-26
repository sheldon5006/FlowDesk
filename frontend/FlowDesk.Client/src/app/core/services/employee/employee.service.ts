import { inject, Injectable } from '@angular/core';

import { CreateEmployee, Employee } from '../../../models/employee.model';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private http = inject(HttpClient);

  private readonly apiUrl = 'https://localhost:7113/api/employees';

  getAll(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.apiUrl);
  }

  create(employee: CreateEmployee): Observable<Employee> {
    return this.http.post<Employee>(this.apiUrl, employee);
  }
}
