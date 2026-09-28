export type UserRole = 'Admin' | 'Employee';

export interface User {
  id: number;
  email: string;
  role: UserRole;
  isActive: boolean;
  createdAt: string;
  employeeId: number | null;
  employeeName: string | null;
}

export interface CreateUser {
  email: string;
  password: string;
  role: UserRole;
  employeeId: number | null;
}