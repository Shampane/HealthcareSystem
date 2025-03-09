import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "./data/services/auth.service";
import { inject, signal } from "@angular/core";
import { map } from "rxjs/operators";
import { catchError, of } from "rxjs";

const isAuth = signal<boolean>(false);

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);

  return authService.checkAuthentication().pipe(
    map(() => true),
    catchError(() => of(false)),
  );
};

export const notAuthGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);

  return authService.checkAuthentication().pipe(
    map(() => false),
    catchError(() => of(true)),
  );
};
