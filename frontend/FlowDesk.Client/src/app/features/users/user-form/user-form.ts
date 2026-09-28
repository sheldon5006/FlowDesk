import {
  Component,
  EventEmitter,
  OnInit,
  Output
} from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,FormGroup
} from '@angular/forms';

import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { SelectModule } from 'primeng/select';

import { EmployeeService } from '../../../core/services/employee/employee.service';
import { UserService } from '../../../core/services/user/user.service';

import {
  CreateUser,
  UserRole
} from '../../../models/user.model';

import { Employee } from '../../../models/employee.model';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    InputTextModule,
    ButtonModule,
    SelectModule
  ],
  templateUrl: './user-form.html',
  styleUrl: './user-form.scss'
})
export class UserForm implements OnInit {

  @Output()
  userCreated = new EventEmitter<void>();

  @Output()
  cancelled = new EventEmitter<void>();

  employees: Employee[] = [];

  roles = [
    {
      label: 'Admin',
      value: 'Admin' as UserRole
    },
    {
      label: 'Employee',
      value: 'Employee' as UserRole
    }
  ];

  loading = false;
  loadingEmployees = false;
form : FormGroup;


  constructor(
    private readonly fb: FormBuilder,
    private readonly userService: UserService,
    private readonly employeeService: EmployeeService
  ) {  this.form = this.fb.nonNullable.group({
    email: [
      '',
      [
        Validators.required,
        Validators.email
      ]
    ],

    password: [
      '',
      Validators.required
    ],

    role: [
      'Employee' as UserRole,
      Validators.required
    ],

    employeeId: [
      null as number | null
    ]
  });}

  ngOnInit(): void {
    this.loadEmployees();

    this.form.controls['role'].valueChanges.subscribe(role => {

      if (role === 'Admin') {
        this.form.controls['employeeId'].setValue(null);
      }

    });
  }

  loadEmployees(): void {
    this.loadingEmployees = true;

    this.employeeService.getAll().subscribe({
      next: (employees) => {
        this.employees = employees;
        this.loadingEmployees = false;
      },

      error: (error) => {
        console.error(
          'Failed to load employees',
          error
        );

        this.loadingEmployees = false;
      }
    });
  }

  save(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    const value = this.form.getRawValue();

    const request: CreateUser = {
      email: value.email,
      password: value.password,
      role: value.role,
      employeeId: value.role === 'Admin'
        ? null
        : value.employeeId
    };

    this.loading = true;

    this.userService.create(request).subscribe({
      next: () => {
        this.loading = false;
        this.form.reset({
          email: '',
          password: '',
          role: 'Employee',
          employeeId: null
        });

        this.userCreated.emit();
      },

      error: (error) => {
        this.loading = false;

        console.error(
          'Failed to create user',
          error
        );

        alert(
          error.error?.message ??
          'Unable to create user.'
        );
      }
    });
  }

  cancel(): void {
    this.cancelled.emit();
  }
}