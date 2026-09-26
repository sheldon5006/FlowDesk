import { Component, viewChild } from '@angular/core';

import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';

import { EmployeeForm } from '../employee-form/employee-form';
import { EmployeeList } from '../employee-list/employee-list';

@Component({
  selector: 'app-employee-page',
  standalone: true,
  imports: [
    ButtonModule,
    DialogModule,
    EmployeeForm,
    EmployeeList
  ],
  templateUrl: './employee-page.html',
  styleUrl: './employee-page.scss'
})
export class EmployeePage {

  readonly employeeList = viewChild(EmployeeList);

  addEmployeeDialogVisible = false;

  openAddEmployeeDialog(): void {
    this.addEmployeeDialogVisible = true;
  }

  onEmployeeCreated(): void {
    this.addEmployeeDialogVisible = false;

    this.employeeList()?.loadEmployees();
  }
}