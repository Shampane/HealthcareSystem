export interface GetAppointmentsRequest {
  doctorId?: string;
  userId?: string;
  pageIndex?: number;
  pageSize?: number;
  startTime?: Date;
  endTime?: Date;
}

export interface CreateAppointmentRequest {
  scheduleId: string;
  userEmail: string;
}
