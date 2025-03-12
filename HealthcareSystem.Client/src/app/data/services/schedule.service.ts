import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { Schedule } from "../entities/schedule";

@Injectable({
  providedIn: "root",
})
export class ScheduleService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = "https://localhost:8081/api/schedules";

  getSchedulesByDoctorId(request: string): Observable<Schedule[]> {
    return this.httpClient.get<Schedule[]>(this.baseUrl + "/" + request);
  }
}
