import {
  Component,
  OnInit
} from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';

import {
  AssignmentService
} from '../../../core/services/assignment/assignment.service';

import {
  AuthService
} from '../../../core/services/auth/auth.service';

import {
  Assignment
} from '../../../models/assignment.model';

@Component({
  selector: 'app-assignment-list',
  standalone: true,
  imports: [
    ButtonModule,
    InputTextModule,
    TableModule,
    TagModule
  ],
  templateUrl: './assignment-list.html',
  styleUrl: './assignment-list.scss'
})
export class AssignmentList implements OnInit {

  assignments: Assignment[] = [];

  loading = false;

  confirmingAssignmentId: number | null = null;

  isAdmin = false;
  isEmployee = false;

  constructor(
    private readonly assignmentService: AssignmentService,
    private readonly authService: AuthService
  ) {}

  ngOnInit(): void {
    this.isAdmin = this.authService.isAdmin();
    this.isEmployee = this.authService.isEmployee();

    this.loadAssignments();
  }

  loadAssignments(): void {
    this.loading = true;

    if (this.isAdmin) {
      this.assignmentService.getAll().subscribe({
        next: (assignments) => {
          this.assignments = assignments;
          this.loading = false;
        },

        error: (error) => {
          console.error(
            'Failed to load assignments',
            error
          );

          this.loading = false;
        }
      });

      return;
    }

    const employeeId = this.authService.getEmployeeId();

    if (employeeId === null) {
      this.assignments = [];
      this.loading = false;
      return;
    }

    this.assignmentService
      .getByEmployee(employeeId)
      .subscribe({
        next: (assignments) => {
          this.assignments = assignments;
          this.loading = false;
        },

        error: (error) => {
          console.error(
            'Failed to load employee assignments',
            error
          );

          this.loading = false;
        }
      });
  }

  confirmAssignment(
    assignmentId: number
  ): void {

    if (!this.isAdmin) {
      return;
    }

    this.confirmingAssignmentId =
      assignmentId;

    this.assignmentService
      .confirm(assignmentId)
      .subscribe({
        next: () => {
          this.confirmingAssignmentId = null;

          this.loadAssignments();
        },

        error: (error) => {
          this.confirmingAssignmentId = null;

          console.error(
            'Failed to confirm assignment',
            error
          );

          alert(
            error.error?.message ??
            'Unable to confirm assignment.'
          );
        }
      });
  }

  onGlobalFilter(
    event: Event,
    table: Table
  ): void {

    const value =
      (event.target as HTMLInputElement)
        .value;

    table.filterGlobal(
      value,
      'contains'
    );
  }
}