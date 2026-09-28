import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Injectable, inject } from '@angular/core';
import {
  CreateUser,
  User
} from '../../../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private readonly http: HttpClient) {}

  private readonly apiUrl =
    'https://localhost:7113/api/users';

  getAll(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  getById(id: number): Observable<User> {
    return this.http.get<User>(
      `${this.apiUrl}/${id}`
    );
  }

  create(user: CreateUser): Observable<User> {
    return this.http.post<User>(
      this.apiUrl,  
      user
    );
  }
}