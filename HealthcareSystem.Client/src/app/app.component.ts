import { Component } from "@angular/core";
import { FooterComponent } from "./view/shared/footer/footer.component";
import { HomePageComponent } from "./view/pages/home/home-page.component";
import { HeaderComponent } from "./view/shared/header/header.component";
import { DoctorsPageComponent } from "./view/pages/doctors-page/doctors-page.component";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  imports: [
    FooterComponent,
    HomePageComponent,
    HeaderComponent,
    DoctorsPageComponent,
  ],
})
export class AppComponent {
  title = "HealthcareSystem.Client";
}
