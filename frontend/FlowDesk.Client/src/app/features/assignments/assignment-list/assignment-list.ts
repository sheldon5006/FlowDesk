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

  constructor(
    private readonly assignmentService: AssignmentService
  ) {}

  ngOnInit(): void {
    this.loadAssignments();
  }

  loadAssignments(): void {
    this.loading = true;

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
  }

  confirmAssignment(
    assignmentId: number
  ): void {

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