import { Component, inject } from '@angular/core';
import { FormBuilder,FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeService } from '../../../core/services/employee/employee.service';
import { output } from '@angular/core';

@Component({
  selector: 'app-employee-form',
  imports: [ReactiveFormsModule],
  templateUrl: './employee-form.html',
  styleUrl: './employee-form.scss',
})
export class EmployeeForm {
 employeeCreated = output<void>();

  form : FormGroup;
 

    constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
  ) {
     this.form = this.fb.nonNullable.group({
    firstName: ['', [Validators.required, Validators.maxLength(50)]],
    lastName: ['', [Validators.required, Validators.maxLength(50)]],
    email: ['', [Validators.required, Validators.email]]
  });
  }


  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.employeeService.create(this.form.getRawValue()).subscribe({
      next: (employee) => {
        console.log('Created employee:', employee);
        this.form.reset();
        this.employeeCreated.emit();
      },
      error: (error) => {
        console.error('Failed to create employee', error);
      }
    });
  }

}
