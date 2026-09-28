import { Component } from '@angular/core';

import {
  Router,
  RouterLink,
  RouterOutlet
} from '@angular/router';

import { AuthService } from '../../services/auth/auth.service';
import { Button } from 'primeng/button';

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [
    RouterLink,
    RouterOutlet,
    Button
  ],
  templateUrl: './app-shell.html',
  styleUrl: './app-shell.scss'
})
export class AppShell {

  isAdmin = false;
  isEmployee = false;
  role = '';

  constructor(
    private readonly authService: AuthService,
    private readonly router: Router
  ) {
    this.isAdmin = this.authService.isAdmin();
    this.isEmployee = this.authService.isEmployee();
    this.role = this.authService.getRole() ?? '';
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}