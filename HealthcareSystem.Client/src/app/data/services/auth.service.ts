import { inject, Injectable } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { RegisterRequest } from "../requests/authRequests";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = "https://localhost:8081/auth/";

  register(request?: RegisterRequest): Observable<HttpErrorResponse> {
    return this.httpClient.post<HttpErrorResponse>(
      this.baseUrl + "register",
      request,
    );
  }
}
