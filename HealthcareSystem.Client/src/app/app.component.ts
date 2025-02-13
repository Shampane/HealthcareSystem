import { Component } from '@angular/core';
import { HeaderComponent } from './ui/components/header/header.component';
import { HomePageComponent } from './ui/pages/home-page/home-page.component';
import { FooterComponent } from './ui/components/footer/footer.component';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, HomePageComponent, FooterComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'HealthcareSystem.Client';
}
