import { Component } from '@angular/core';
import { HomePromoComponent } from '../../components/home-promo/home-promo.component';
import { HomeSpecializationComponent } from '../../components/home-specialization/home-specialization.component';
import { HomeDoctorListComponent } from '../../components/home-doctor-list/home-doctor-list.component';
import { HomeLoginComponent } from '../../components/home-login/home-login.component';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  imports: [
    HomePromoComponent,
    HomeSpecializationComponent,
    HomeDoctorListComponent,
    HomeLoginComponent,
  ],
})
export class HomePageComponent {}
