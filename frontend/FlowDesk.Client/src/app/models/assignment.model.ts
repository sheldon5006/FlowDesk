export interface Assignment {
   id: number;

  employeeId: number;

  employeeName: string;

  shiftId: number;

  shiftDate: string;

  startTime: string;

  endTime: string;

  location: string;

  status: string;

  assignedAt: string;
}

export interface CreateAssignment {
  employeeId: number;
  shiftId: number;
}