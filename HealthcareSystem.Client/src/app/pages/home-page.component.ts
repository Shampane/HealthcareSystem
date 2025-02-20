import { Component } from '@angular/core';
import { HomePromoComponent } from '../components/home/home-promo.component';
import { HomeSpecializationComponent } from '../components/home/home-specialization.component';
import { HomeDoctorListComponent } from '../components/home/home-doctor-list.component';
import { HomeLoginComponent } from '../components/home/home-login.component';

@Component({
  selector: 'app-home-page',
  imports: [
    HomePromoComponent,
    HomeSpecializationComponent,
    HomeDoctorListComponent,
    HomeLoginComponent,
    HomePromoComponent,
    HomeSpecializationComponent,
    HomeDoctorListComponent,
    HomeLoginComponent,
  ],
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {}
