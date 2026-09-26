import { Component, OnInit } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';

import { ShiftService } from '../../../core/services/shift/shift.service';
import { SchedulingService } from '../../../core/services/scheduling/scheduling.service';
import { AssignmentService } from '../../../core/services/assignment/assignment.service';

import { Shift } from '../../../models/shift.model';
import { SchedulingCandidate } from '../../../models/scheduling.model';
import { Assignment } from '../../../models/assignment.model';

@Component({
  selector: 'app-shift-list',
  standalone: true,
  imports: [
    ButtonModule,
    TableModule,
    TagModule,
    InputTextModule,
    DialogModule
  ],
  templateUrl: './shift-list.html',
  styleUrl: './shift-list.scss'
})
export class ShiftList implements OnInit {

  shifts: Shift[] = [];

  selectedShift: Shift | null = null;

  selectedShiftId: number | null = null;

  candidates: SchedulingCandidate[] = [];

  assignments: Assignment[] = [];

  loading = false;

  loadingCandidates = false;

  assigningEmployeeId: number | null = null;

  confirmingAssignmentId: number | null = null;

  processingShiftId: number | null = null;

  manageDialogVisible = false;

  constructor(
    private readonly shiftService: ShiftService,
    private readonly schedulingService: SchedulingService,
    private readonly assignmentService: AssignmentService
  ) {}

  ngOnInit(): void {
    this.loadShifts();
  }

  loadShifts(): void {
    this.loading = true;

    this.shiftService.getAll().subscribe({
      next: (shifts) => {
        this.shifts = shifts;
        this.loading = false;
      },

      error: (error) => {
        console.error(
          'Failed to load shifts',
          error
        );

        this.loading = false;
      }
    });
  }

  manageShift(shift: Shift): void {
    this.selectedShift = shift;
    this.selectedShiftId = shift.id;
    this.manageDialogVisible = true;

    this.loadCandidates(shift.id);
    this.loadAssignments(shift.id);
  }

  private loadCandidates(shiftId: number): void {
    this.loadingCandidates = true;
    this.candidates = [];

    this.schedulingService
      .getCandidates(shiftId)
      .subscribe({
        next: (candidates) => {
          this.candidates = candidates;
          this.loadingCandidates = false;
        },

        error: (error) => {
          console.error(
            'Failed to load scheduling candidates',
            error
          );

          this.loadingCandidates = false;
        }
      });
  }

  private loadAssignments(shiftId: number): void {
    this.assignmentService
      .getByShift(shiftId)
      .subscribe({
        next: (assignments) => {
          this.assignments = assignments;
        },

        error: (error) => {
          console.error(
            'Failed to load shift assignments',
            error
          );
        }
      });
  }

  assignCandidate(candidate: SchedulingCandidate): void {
    if (this.selectedShiftId === null) {
      return;
    }

    const shiftId = this.selectedShiftId;

    this.assigningEmployeeId = candidate.employeeId;

    this.assignmentService
      .create({
        employeeId: candidate.employeeId,
        shiftId: shiftId
      })
      .subscribe({
        next: () => {
          this.assigningEmployeeId = null;

          this.loadShifts();
          this.loadCandidates(shiftId);
          this.loadAssignments(shiftId);
        },

        error: (error) => {
          this.assigningEmployeeId = null;

          console.error(
            'Failed to assign employee',
            error
          );

          alert(
            error.error?.message ??
            'Unable to assign employee.'
          );
        }
      });
  }

  confirmAssignment(assignmentId: number): void {
    if (this.selectedShiftId === null) {
      return;
    }

    const shiftId = this.selectedShiftId;

    this.confirmingAssignmentId = assignmentId;

    this.assignmentService
      .confirm(assignmentId)
      .subscribe({
        next: () => {
          this.confirmingAssignmentId = null;

          this.loadAssignments(shiftId);
          this.loadCandidates(shiftId);
          this.loadShifts();
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

  publishShift(shiftId: number): void {
    this.processingShiftId = shiftId;

    this.shiftService
      .publish(shiftId)
      .subscribe({
        next: () => {
          this.processingShiftId = null;
          this.loadShifts();
        },

        error: (error) => {
          this.processingShiftId = null;

          console.error(
            'Failed to publish shift',
            error
          );

          alert(
            error.error?.message ??
            'Unable to publish shift.'
          );
        }
      });
  }

  completeShift(shiftId: number): void {
    this.processingShiftId = shiftId;

    this.shiftService
      .complete(shiftId)
      .subscribe({
        next: () => {
          this.processingShiftId = null;
          this.loadShifts();
        },

        error: (error) => {
          this.processingShiftId = null;

          console.error(
            'Failed to complete shift',
            error
          );

          alert(
            error.error?.message ??
            'Unable to complete shift.'
          );
        }
      });
  }

  cancelShift(shiftId: number): void {
    this.processingShiftId = shiftId;

    this.shiftService
      .cancel(shiftId)
      .subscribe({
        next: () => {
          this.processingShiftId = null;
          this.loadShifts();
        },

        error: (error) => {
          this.processingShiftId = null;

          console.error(
            'Failed to cancel shift',
            error
          );

          alert(
            error.error?.message ??
            'Unable to cancel shift.'
          );
        }
      });
  }

  getStaffingClass(shift: Shift): string {
    if (
      shift.assignedEmployeeCount >=
      shift.requiredEmployees
    ) {
      return 'staffing-complete';
    }

    if (shift.assignedEmployeeCount > 0) {
      return 'staffing-partial';
    }

    return 'staffing-empty';
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