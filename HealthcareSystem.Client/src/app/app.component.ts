import { Component } from "@angular/core";
import { FooterComponent } from "./view/shared/footer/footer.component";
import { HeaderComponent } from "./view/shared/header/header.component";
import { RouterOutlet } from "@angular/router";

@Component({
  selector: "app-root",
  templateUrl: "./app.component.html",
  imports: [FooterComponent, HeaderComponent, RouterOutlet],
})
export class AppComponent {
  title = "HealthcareSystem.Client";
}
