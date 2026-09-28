import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { LoginRequest, LoginResponse } from '../../../models/auth.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly apiUrl = 'https://localhost:7113/api/auth';
  private readonly storageKey = 'flowdesk_auth';

  constructor(private readonly http: HttpClient) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http
      .post<LoginResponse>(`${this.apiUrl}/login`, request)
      .pipe(
        tap(response => {
          sessionStorage.setItem(
            this.storageKey,
            JSON.stringify(response)
          );
        })
      );
  }

  logout(): void {
    sessionStorage.removeItem(this.storageKey);
  }

  getAuth(): LoginResponse | null {
    const value = sessionStorage.getItem(this.storageKey);

    if (!value) {
      return null;
    }

    try {
      return JSON.parse(value) as LoginResponse;
    } catch {
      this.logout();
      return null;
    }
  }

  getToken(): string | null {
    return this.getAuth()?.token ?? null;
  }

  getRole(): 'Admin' | 'Employee' | null {
    return this.getAuth()?.role ?? null;
  }

  getEmployeeId(): number | null {
    return this.getAuth()?.employeeId ?? null;
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  isAdmin(): boolean {
    return this.getRole() === 'Admin';
  }

  isEmployee(): boolean {
    return this.getRole() === 'Employee';
  }
}