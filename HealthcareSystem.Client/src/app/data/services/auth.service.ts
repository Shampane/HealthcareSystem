import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { LoginRequest, RegisterRequest } from "../requests/authRequests";
import { Observable } from "rxjs";
import { TokenResponse } from "../responses/tokenResponse";

@Injectable({
  providedIn: "root",
})
export class AuthService {
  httpClient: HttpClient = inject(HttpClient);
  baseUrl = "https://localhost:8081/auth/";

  register(request: RegisterRequest): Observable<object> {
    return this.httpClient.post<object>(this.baseUrl + "register", request);
  }

  login(request: LoginRequest): Observable<TokenResponse> {
    return this.httpClient.post<TokenResponse>(this.baseUrl + "login", request, {
      withCredentials: true,
    });
  }

  checkToken(): Observable<any> {
    return this.httpClient.get<any>(this.baseUrl + "checkToken", {
      withCredentials: true,
    });
  }
}
