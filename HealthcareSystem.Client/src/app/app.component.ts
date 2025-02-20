import { Component, inject } from '@angular/core';
import { HomePageComponent } from './pages/home-page.component';
import { FooterComponent } from './shared/footer.component';
import { HeaderComponent } from './shared/header.component';
import { DoctorService } from './services/doctor.service';

@Component({
  selector: 'app-root',
  imports: [HomePageComponent, FooterComponent, HeaderComponent],
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'HealthcareSystem.Client';
  doctorService = inject(DoctorService);
  doctors = [];

  constructor() {
    this.doctorService.getTestDoctors().subscribe((data) => (this.doctors = data));
    console.log(this.doctors);
  }
}
