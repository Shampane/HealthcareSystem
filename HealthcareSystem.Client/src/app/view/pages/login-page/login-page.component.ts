import { Component, inject, signal } from "@angular/core";
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
import { RouterLink } from "@angular/router";
import { AuthService } from "../../../data/services/auth.service";
import { LoginRequest } from "../../../data/requests/authRequests";
import { ModalCardComponent } from "../../shared/modal-card/modal-card.component";

@Component({
  selector: "app-login-page",
  imports: [FormsModule, ReactiveFormsModule, RouterLink, ModalCardComponent],
  templateUrl: "./login-page.component.html",
  styleUrl: "./login-page.component.scss",
})
export class LoginPageComponent {
  isSuccessful = signal<boolean | null>(null);
  responseMessages = signal<string[]>([]);

  authService = inject(AuthService);

  loginFormGroup = new FormGroup({
    email: new FormControl(""),
    password: new FormControl(""),
  });

  clearSuccessful = (): void => {
    this.responseMessages.set([]);
    this.isSuccessful.set(null);
  };

  handleSuccess = () => {
    window.location.href = "/user";
  };

  onSubmit = (): void => {
    const formGroup = this.loginFormGroup.value;
    const request: LoginRequest = {
      email: formGroup.email!,
      password: formGroup.password!,
    };
    this.authService.login(request).subscribe({
      next: () => {
        this.responseMessages.set(["User was successfully login"]);
        this.loginFormGroup.reset();
        this.isSuccessful.set(true);
      },
      error: (response) => {
        this.responseMessages.set([response.error.message]);
        this.isSuccessful.set(false);
      },
    });
  };
  protected readonly window = window;
}
