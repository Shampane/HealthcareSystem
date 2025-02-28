import { Component } from '@angular/core';
import { LoginFormComponent } from "../../components/login-form/login-form.component";

@Component({
  selector: "app-auth-page",
  imports: [LoginFormComponent],
  templateUrl: "./auth-page.component.html",
  styleUrl: "./auth-page.component.scss",
})
export class AuthPageComponent {}
