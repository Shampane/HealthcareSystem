import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = 'https://localhost:8081/api/';

  getTestDoctors(): Observable<any> {
    return this.httpClient.get(this.baseUrl + 'doctors');
  }
}
