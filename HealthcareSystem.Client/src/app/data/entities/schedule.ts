export interface Schedule {
  id: string;
  doctorId: string;
  doctorName: string;
  startTime: Date;
  endTime: Date;
  isAvailable: boolean;
}
