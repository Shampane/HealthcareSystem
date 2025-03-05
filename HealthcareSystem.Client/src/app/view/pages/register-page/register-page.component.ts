import { Component, inject, signal } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";
import { RouterLink } from "@angular/router";
import { AuthService } from "../../../data/services/auth.service";
import { RegisterRequest } from "../../../data/requests/authRequests";
import { ModalCardComponent } from "../../shared/modal-card/modal-card.component";

@Component({
  selector: "app-register-page",
  imports: [ReactiveFormsModule, RouterLink, ModalCardComponent],
  templateUrl: "./register-page.component.html",
  styleUrl: "./register-page.component.scss",
})
export class RegisterPageComponent {
  isSuccessful = signal<boolean | null>(null);
  responseMessages = signal<string[]>([]);

  authService = inject(AuthService);

  registerFormGroup = new FormGroup({
    username: new FormControl(""),
    email: new FormControl(""),
    gender: new FormControl("male"),
    password: new FormControl(""),
    confirmPassword: new FormControl(""),
    enableTwoFactor: new FormControl(true),
  });

  clearSuccessful = (): void => {
    this.responseMessages.set([]);
    this.isSuccessful.set(null);
  };

  onSubmit = (): void => {
    const formGroup = this.registerFormGroup.value;
    const request: RegisterRequest = {
      username: formGroup.username!,
      email: formGroup.email!,
      gender: formGroup.gender!,
      password: formGroup.password!,
      confirmPassword: formGroup.confirmPassword!,
      enableTwoFactor: formGroup.enableTwoFactor!,
    };
    this.authService.register(request).subscribe({
      next: () => {
        this.responseMessages.set(["User was successfully registered"]);
        this.registerFormGroup.reset();
        this.isSuccessful.set(true);
      },
      error: (response) => {
        this.responseMessages.set(response.error);
        this.isSuccessful.set(false);
      },
    });
  };
}
