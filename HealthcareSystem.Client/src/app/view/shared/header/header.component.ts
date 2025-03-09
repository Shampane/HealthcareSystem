import { Component, computed, inject, signal } from "@angular/core";
import { PagesLinks } from "../../interfaces/pagesLinks";
import { RouterLink } from "@angular/router";
import { AuthService } from "../../../data/services/auth.service";
import { toSignal } from "@angular/core/rxjs-interop";
import { catchError, map, of } from "rxjs";

@Component({
  selector: "app-header",
  templateUrl: "./header.component.html",
  styleUrl: "header.component.scss",
  imports: [RouterLink],
})
export class HeaderComponent {
  authService = inject(AuthService);
  isAuthenticated = signal<boolean>(false);

  links: PagesLinks[] = [
    { url: "home", title: "home" },
    { url: "doctors", title: "doctors" },
    { url: "contact", title: "contact" },
    { url: "about", title: "about" },
  ];

  constructor() {
    this.authService.checkAuthentication().subscribe({
      next: () => {
        this.isAuthenticated.set(true);
      },
      error: () => {
        this.isAuthenticated.set(false);
      },
    });
  }
}
