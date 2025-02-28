import { Component } from "@angular/core";
import {
  FormControl,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
} from "@angular/forms";
import { RouterLink } from "@angular/router";

@Component({
  selector: "app-login-page",
  imports: [FormsModule, ReactiveFormsModule, RouterLink],
  templateUrl: "./login-page.component.html",
  styleUrl: "./login-page.component.scss",
})
export class LoginPageComponent {
  loginFormGroup = new FormGroup({
    email: new FormControl(""),
    password: new FormControl(""),
  });

  onSubmit() {
    console.log(this.loginFormGroup.value);
  }
}
