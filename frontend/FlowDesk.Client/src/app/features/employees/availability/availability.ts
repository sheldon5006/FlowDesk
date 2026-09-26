import {
  Component,
  OnInit
} from '@angular/core';

import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { SelectModule } from 'primeng/select';
import { DatePickerModule } from 'primeng/datepicker';

import { AvailabilityService } from '../../../core/services/availability/availability.service';

import {
  Availability,
  CreateAvailability
} from '../../../models/availability.model';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-availability',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    ButtonModule,
    DialogModule,
    SelectModule,
    DatePickerModule,
    InputTextModule
  ],
  templateUrl: './availability.html',
  styleUrl: './availability.scss'
})
export class AvailabilityComponent implements OnInit {

  // Temporary until authentication/employee selection is implemented.
  employeeId = 2;

  availabilities: Availability[] = [];

  form: FormGroup;

  loading = false;

  saving = false;

  deletingAvailabilityId: number | null = null;

  addDialogVisible = false;

  daysOfWeek = [
    { label: 'Monday', value: 'Monday' },
    { label: 'Tuesday', value: 'Tuesday' },
    { label: 'Wednesday', value: 'Wednesday' },
    { label: 'Thursday', value: 'Thursday' },
    { label: 'Friday', value: 'Friday' },
    { label: 'Saturday', value: 'Saturday' },
    { label: 'Sunday', value: 'Sunday' }
  ];

  constructor(
    private readonly fb: FormBuilder,
    private readonly availabilityService: AvailabilityService
  ) {
    this.form = this.fb.nonNullable.group({
      dayOfWeek: [
        'Monday',
        Validators.required
      ],

      startTime: [
        '08:00',
        Validators.required
      ],

      endTime: [
        '16:00',
        Validators.required
      ]
    });
  }

  ngOnInit(): void {
    this.loadAvailability();
  }

  loadAvailability(): void {
    this.loading = true;

    this.availabilityService
      .getByEmployee(this.employeeId)
      .subscribe({
        next: (data) => {
          this.availabilities = data;
          this.loading = false;
        },

        error: (error) => {
          console.error(
            'Failed to load availability',
            error
          );

          this.loading = false;
        }
      });
  }

  openAddDialog(): void {
    this.resetForm();

    this.addDialogVisible = true;
  }

  closeAddDialog(): void {
    this.addDialogVisible = false;
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const data: CreateAvailability =
      this.form.getRawValue();

    this.saving = true;

    this.availabilityService
      .create(this.employeeId, data)
      .subscribe({
        next: () => {

          this.saving = false;

          this.addDialogVisible = false;

          this.resetForm();

          this.loadAvailability();
        },

        error: (error) => {

          this.saving = false;

          console.error(
            'Failed to create availability',
            error
          );
        }
      });
  }

  delete(availabilityId: number): void {

    this.deletingAvailabilityId =
      availabilityId;

    this.availabilityService
      .delete(
        this.employeeId,
        availabilityId
      )
      .subscribe({
        next: () => {

          this.deletingAvailabilityId = null;

          this.loadAvailability();
        },

        error: (error) => {

          this.deletingAvailabilityId = null;

          console.error(
            'Failed to delete availability',
            error
          );
        }
      });
  }

  private resetForm(): void {
    this.form.reset({
      dayOfWeek: 'Monday',
      startTime: '08:00',
      endTime: '16:00'
    });
  }

  get availableDays() {
    const existingDays = new Set(
      this.availabilities.map(
        availability => availability.dayOfWeek
      )
    );

    return this.daysOfWeek.filter(
      day => !existingDays.has(day.value)
    );
  }
}