import { Component, OnInit } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { Table, TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';

import { EmployeeService } from '../../../core/services/employee/employee.service';
import { Employee } from '../../../models/employee.model';

@Component({
  selector: 'app-employee-list',
  standalone: true,
  imports: [
    ButtonModule,
    InputTextModule,
    TableModule,
    TagModule
  ],
  templateUrl: './employee-list.html',
  styleUrl: './employee-list.scss'
})
export class EmployeeList implements OnInit {

  employees: Employee[] = [];

  loading = false;

  constructor(
    private readonly employeeService: EmployeeService
  ) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;

    this.employeeService.getAll().subscribe({
      next: (employees) => {
        this.employees = employees;
        this.loading = false;
      },

      error: (error) => {
        console.error(
          'Failed to load employees',
          error
        );

        this.loading = false;
      }
    });
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