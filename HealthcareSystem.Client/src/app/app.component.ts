import { Component } from '@angular/core';
import { HeaderComponent } from './ui/components/header/header.component';
import { HomePageComponent } from './ui/pages/home-page/home-page.component';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent, HomePageComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'HealthcareSystem.Client';
}
