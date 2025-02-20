export interface GetSchedulesRequest {
  doctorId?: string;
  pageIndex?: number;
  pageSize?: number;
  startTime?: Date;
  endTime?: Date;
}

export interface UpdateScheduleRequest {
  isAvailable: boolean;
}

export interface CreateScheduleRequest {
  doctorId: string;
  startTime?: Date;
  endTime?: Date;
}
