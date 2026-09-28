import {
  Component,
  OnInit
} from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import {
  Table,
  TableModule
} from 'primeng/table';
import { TagModule } from 'primeng/tag';

import { UserService } from '../../../core/services/user/user.service';
import { User } from '../../../models/user.model';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    ButtonModule,
    DialogModule,
    InputTextModule,
    TableModule,
    TagModule,
    DatePipe,
  ],
  templateUrl: './user-list.html',
  styleUrl: './user-list.scss'
})
export class UserList implements OnInit {

  users: User[] = [];

  loading = false;

  selectedUser: User | null = null;

  viewDialogVisible = false;

  constructor(
    private readonly userService: UserService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.loading = true;

    this.userService.getAll().subscribe({
      next: (users) => {
        this.users = users;
        this.loading = false;
      },

      error: (error) => {
        console.error(
          'Failed to load users',
          error
        );

        this.loading = false;
      }
    });
  }

  viewUser(user: User): void {
    this.userService.getById(user.id).subscribe({
      next: (result) => {
        this.selectedUser = result;
        this.viewDialogVisible = true;
      },

      error: (error) => {
        console.error(
          'Failed to load user',
          error
        );
      }
    });
  }

  getRoleSeverity(
    role: string
  ): 'success' | 'info' {
    return role === 'Admin'
      ? 'success'
      : 'info';
  }

  onGlobalFilter(
    event: Event,
    table: Table
  ): void {

    const value =
      (event.target as HTMLInputElement).value;

    table.filterGlobal(
      value,
      'contains'
    );
  }
}