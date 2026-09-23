import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class Api {

  private http = inject(HttpClient);

  private apiUrl = 'https://localhost:7113';

  getTest() {
    return this.http.get<{ message: string }>(
      `${this.apiUrl}/api/test`
    );
  }
  
}
