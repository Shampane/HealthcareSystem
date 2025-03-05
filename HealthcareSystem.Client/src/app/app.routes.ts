import { Routes } from "@angular/router";
import { HomePageComponent } from "./view/pages/home-page/home-page.component";
import { DoctorsPageComponent } from "./view/pages/doctors-page/doctors-page.component";
import { NotFoundPageComponent } from "./view/pages/not-found-page/not-found-page.component";
import { LoginPageComponent } from "./view/pages/login-page/login-page.component";
import { RegisterPageComponent } from "./view/pages/register-page/register-page.component";
import { authGuard } from "./auth.guard";
import { UserPageComponent } from "./view/pages/user-page/user-page.component";

export const routes: Routes = [
  { path: "", component: HomePageComponent },
  { path: "doctors", component: DoctorsPageComponent },
  { path: "home", redirectTo: "", pathMatch: "full" },
  { path: "login", component: LoginPageComponent },
  { path: "register", component: RegisterPageComponent },
  { path: "user", component: UserPageComponent, canActivate: [authGuard] },
  { path: "**", component: NotFoundPageComponent },
];
