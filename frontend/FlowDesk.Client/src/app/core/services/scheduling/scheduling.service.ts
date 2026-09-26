import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SchedulingCandidate } from '../../../models/scheduling.model';

@Injectable({
  providedIn: 'root',
})
export class SchedulingService {
   private readonly http = inject(HttpClient);

  private readonly apiUrl =
    'https://localhost:7113/api/scheduling';

  getCandidates(shiftId: number): Observable<SchedulingCandidate[]> {
    return this.http.get<SchedulingCandidate[]>(
      `${this.apiUrl}/shifts/${shiftId}/candidates`
    );
  }
}
