import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Doctor } from '../entities/doctor';
import { GetDoctorsRequest } from '../requests/doctorRequests';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = 'https://localhost:8081/api/';

  getTestDoctors(request?: GetDoctorsRequest): Observable<Doctor[]> {
    let params = new HttpParams();
    if (request) {
      Object.keys(request).forEach((key) => {
        const value = (request as any)[key];
        if (value !== undefined && value !== null) {
          params = params.set(key, value.toString());
        }
      });
    }

    return this.httpClient.get<Doctor[]>(this.baseUrl + 'doctors', { params });
  }
}
