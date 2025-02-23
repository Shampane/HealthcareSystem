import { Doctor } from "../entities/doctor";

export interface DoctorGetResponse {
  totalCount: number;
  data: Doctor[];
}
