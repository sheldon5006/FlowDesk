import { Component, inject, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShiftService } from '../../../core/services/shift/shift.service';
import { CreateShift } from '../../../models/shift.model';
import { ButtonModule } from 'primeng/button';
import { DatePickerModule } from 'primeng/datepicker';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-shift-form',
  imports: [ReactiveFormsModule,
    DatePickerModule,
    InputTextModule,
    InputNumberModule,
    ButtonModule],
  templateUrl: './shift-form.html',
  styleUrl: './shift-form.scss',
})

export class ShiftForm {

  shiftCreated = output<void>();

  loading = false;
  shiftForm: FormGroup;

  constructor(
    private readonly fb: FormBuilder,
    private readonly shiftService: ShiftService
  ) {
     this.shiftForm = this.fb.group({
          date: this.fb.control<Date | null>(null, Validators.required),

          startTime: this.fb.control<Date | null>(null,Validators.required),

          endTime: this.fb.control<Date | null>(null, Validators.required),

          location: this.fb.control('',[ Validators.required, Validators.maxLength(150)]),

          requiredEmployees: this.fb.control( 1,[ Validators.required, Validators.min(1)]
          )
        });
  }

 

  createShift(): void {

    if (this.shiftForm.invalid) {
      this.shiftForm.markAllAsTouched();
      return;
    }

    const formValue = this.shiftForm.getRawValue();

    const request: CreateShift = {
      date: this.formatDate(formValue.date!),
      startTime: this.formatTime(formValue.startTime!),
      endTime: this.formatTime(formValue.endTime!),
      location: formValue.location!.trim(),
      requiredEmployees: formValue.requiredEmployees!
    };

    this.loading = true;

    this.shiftService.create(request).subscribe({
      next: () => {

        this.loading = false;

        this.shiftForm.reset({
          date: null,
          startTime: null,
          endTime: null,
          location: '',
          requiredEmployees: 1
        });

        this.shiftCreated.emit();
      },

      error: (error) => {

        this.loading = false;

        console.error(
          'Failed to create shift',
          error
        );
      }
    });
  }

  private formatDate(date: Date): string {
    return [
      date.getFullYear(),
      String(date.getMonth() + 1).padStart(2, '0'),
      String(date.getDate()).padStart(2, '0')
    ].join('-');
  }

  private formatTime(date: Date): string {
    return [
      String(date.getHours()).padStart(2, '0'),
      String(date.getMinutes()).padStart(2, '0'),
      '00'
    ].join(':');
  }
}