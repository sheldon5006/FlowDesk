import { Routes } from '@angular/router';

import { AppShell } from './core/layout/app-shell/app-shell';

import { EmployeePage } from './features/employees/employee-page/employee-page';
import { ShiftPage } from './features/shifts/shift-page/shift-page';
import { AssignmentPage } from './features/assignments/assignment-page/assignment-page';

export const routes: Routes = [
  {
    path: '',
    component: AppShell,
    children: [

      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard')
            .then(m => m.Dashboard)
      },

      {
        path: 'employees',
        component: EmployeePage
      },

      {
        path: 'availability',
        loadComponent: () =>
          import('./features/employees/availability/availability')
            .then(m => m.AvailabilityComponent)
      },

      {
        path: 'shifts',
        component: ShiftPage
      },

      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'assignments',
        component: AssignmentPage
        },
    ]
  },

  {
    path: '**',
    redirectTo: 'dashboard'
  }
];