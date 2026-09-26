import { Component, inject, viewChild } from '@angular/core';
import { Api } from './core/services/api';
import { EmployeeList } from './features/employees/employee-list/employee-list';
import { EmployeeForm } from './features/employees/employee-form/employee-form';
import { AvailabilityComponent } from './features/employees/availability/availability';
import { ShiftForm } from './features/shifts/shift-form/shift-form';
import { ShiftList } from './features/shifts/shift-list/shift-list';
import { ShiftPage } from './features/shifts/shift-page/shift-page';
import { AppShell } from './core/layout/app-shell/app-shell';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [EmployeeList, EmployeeForm, AvailabilityComponent, ShiftForm, ShiftList, ShiftPage, AppShell, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
   private api = inject(Api);

  message = '';
  employeeList = viewChild(EmployeeList);

  ngOnInit(): void {
    this.api.getTest().subscribe({
      next: (response) => {
        this.message = response.message;
      },
      error: (error) => {
        console.error('API error:', error);
        this.message = 'API connection failed';
      }
    });
  }

  onEmployeeCreated(): void {
    this.employeeList()?.loadEmployees();
  }
}
