import { Component, computed, inject } from "@angular/core";
import { AuthService } from "../../../data/services/auth.service";
import { toSignal } from "@angular/core/rxjs-interop";

@Component({
  selector: "app-user-page",
  imports: [],
  templateUrl: "./user-page.component.html",
  styleUrl: "./user-page.component.scss",
})
export class UserPageComponent {
  authService = inject(AuthService);

  userData = toSignal(this.authService.getUserInfo());
  userEmail = computed(() => this.userData()?.email);
  userName = computed(() => this.userData()?.name);

  handleLogout = (): void => {
    this.authService.logout().subscribe();
    window.location.href = "/login";
  };
}
