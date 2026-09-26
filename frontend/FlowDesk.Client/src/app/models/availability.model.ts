export interface Availability {
  id: number;
  dayOfWeek: string;
  startTime: string;
  endTime: string;
}

export interface CreateAvailability {
  dayOfWeek: string;
  startTime: string;
  endTime: string;
}