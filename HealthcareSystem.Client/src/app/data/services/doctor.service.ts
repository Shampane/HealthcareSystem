import { HttpClient, HttpParams } from "@angular/common/http";
import { inject, Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { GetDoctorsRequest } from "../requests/doctorRequests";
import { DoctorGetResponse } from "../responses/doctorResponses";
import { Doctor } from "../entities/doctor";

@Injectable({
  providedIn: "root",
})
export class DoctorService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = "https://localhost:8081/api/doctors";

  getDoctors(request?: GetDoctorsRequest): Observable<DoctorGetResponse> {
    let params = new HttpParams();
    if (request) {
      Object.keys(request).forEach((key) => {
        const value = (request as any)[key];
        if (value !== undefined && value !== null) {
          params = params.set(key, value.toString());
        }
      });
    }
    return this.httpClient.get<DoctorGetResponse>(this.baseUrl, {
      params,
      withCredentials: true,
    });
  }

  getDoctorById(request: string): Observable<Doctor> {
    return this.httpClient.get<Doctor>(this.baseUrl + "/" + request);
  }
}
