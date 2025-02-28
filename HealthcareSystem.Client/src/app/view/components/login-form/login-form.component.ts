import { Component, signal } from "@angular/core";
import { FormControl, FormGroup, ReactiveFormsModule } from "@angular/forms";

@Component({
  selector: "app-login-form",
  imports: [ReactiveFormsModule],
  templateUrl: "./login-form.component.html",
  styleUrl: "./login-form.component.scss",
})
export class LoginFormComponent {
  isLogin = signal(true);

  loginFormGroup = new FormGroup({
    email: new FormControl(""),
    password: new FormControl(""),
  });
  registerFormGroup = new FormGroup({
    username: new FormControl(""),
    email: new FormControl(""),
    gender: new FormControl(""),
    password: new FormControl(""),
    confirmPassword: new FormControl(""),
    enableTwoFactor: new FormControl(),
  });

  onSubmitLogin() {
    console.log(this.loginFormGroup.value);
  }

  onSubmitRegister() {
    console.log(this.registerFormGroup.value);
  }
}
