import { Routes } from '@angular/router';

import { AppShell } from './core/layout/app-shell/app-shell';

import { EmployeePage } from './features/employees/employee-page/employee-page';
import { ShiftPage } from './features/shifts/shift-page/shift-page';
import { AssignmentPage } from './features/assignments/assignment-page/assignment-page';

import { LoginComponent } from './features/auth/login/login';
import { authGuard } from './core/guards/auth.guard';
import { UserPage } from './features/users/user-page/user-page';

export const routes: Routes = [

  // Login must be outside the protected AppShell
  {
    path: 'login',
    component: LoginComponent
  },

  // Protected application
  {
    path: '',
    component: AppShell,
    canActivate: [authGuard],
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
        path: 'assignments',
        component: AssignmentPage
      },
      {
        path: 'users',
        component: UserPage
      },
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      }
    ]
  },

  {
    path: '**',
    redirectTo: 'dashboard'
  }
];