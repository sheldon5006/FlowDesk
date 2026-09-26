import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { DashboardSummary } from '../../../models/dashboard.models';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private readonly apiUrl =
    'https://localhost:7113/api/dashboard';

  constructor(
    private readonly http: HttpClient
  ) {}

  getSummary(): Observable<DashboardSummary> {
    return this.http.get<DashboardSummary>(
      `${this.apiUrl}/summary`
    );
  }
}