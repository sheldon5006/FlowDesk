export interface Shift {
  id: number;
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  requiredEmployees: number;
  assignedEmployeeCount: number;
  status: string;
}

export interface CreateShift {
  date: string;
  startTime: string;
  endTime: string;
  location: string;
  requiredEmployees: number;
}