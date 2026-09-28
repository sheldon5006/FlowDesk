import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {
  CreateAssignment,
  Assignment
} from '../../../models/assignment.model';

@Injectable({
  providedIn: 'root',
})
export class AssignmentService {

  private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:7113/api/assignments';

  create(
    assignment: CreateAssignment
  ): Observable<Assignment> {
    return this.http.post<Assignment>(
      this.apiUrl,
      assignment
    );
  }

  getByShift(
    shiftId: number
  ): Observable<Assignment[]> {
    return this.http.get<Assignment[]>(
      `${this.apiUrl}/shift/${shiftId}`
    );
  }

  getByEmployee(
    employeeId: number
  ): Observable<Assignment[]> {
    return this.http.get<Assignment[]>(
      `${this.apiUrl}/employee/${employeeId}`
    );
  }

  confirm(
    assignmentId: number
  ): Observable<void> {
    return this.http.put<void>(
      `${this.apiUrl}/${assignmentId}/confirm`,
      {}
    );
  }

  getAll(): Observable<Assignment[]> {
    return this.http.get<Assignment[]>(
      this.apiUrl
    );
  }
}