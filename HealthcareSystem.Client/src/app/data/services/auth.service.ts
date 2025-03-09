import { inject, Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { LoginRequest, RegisterRequest } from "../requests/authRequests";
import { catchError, map, Observable, of } from "rxjs";
import {
  IsAuthenticatedResponse,
  TokenResponse,
  UserInfoResponse,
} from "../responses/authResponses";

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

  logout(): Observable<object> {
    return this.httpClient.post(
      this.baseUrl + "logout",
      {},
      {
        withCredentials: true,
      },
    );
  }

  checkAuthentication(): Observable<IsAuthenticatedResponse> {
    return this.httpClient.get<IsAuthenticatedResponse>(
      this.baseUrl + "isAuthenticated",
      {
        withCredentials: true,
      },
    );
  }

  getUserInfo(): Observable<UserInfoResponse> {
    return this.httpClient.get<UserInfoResponse>(this.baseUrl + "getUserInfo", {
      withCredentials: true,
    });
  }
}
