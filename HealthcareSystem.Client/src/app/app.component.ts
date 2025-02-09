import { Component } from '@angular/core';
import { HeaderComponent } from './ui/components/header/header.component';

@Component({
  selector: 'app-root',
  imports: [HeaderComponent],
  templateUrl: './app.component.html',
  styles: [],
})
export class AppComponent {
  title = 'HealthcareSystem.Client';
}
