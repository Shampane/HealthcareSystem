import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "./data/services/auth.service";
import { inject, signal } from "@angular/core";

const isAuth = signal<boolean>(false);

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);

  authService.checkToken().subscribe({
    next: () => {
      isAuth.set(true);
    },
    error: () => {
      isAuth.set(false);
    },
  });

  return isAuth();
};
