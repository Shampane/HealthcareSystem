import { Component } from '@angular/core';
import { HomePromoComponent } from '../../components/home-promo/home-promo.component';
import { HomeSpecializationComponent } from '../../components/home-specialization/home-specialization.component';
import { HomeDoctorListComponent } from '../../components/home-doctor-list/home-doctor-list.component';
import { HomeLoginComponent } from '../../components/home-login/home-login.component';

@Component({
  selector: 'app-home-page',
  imports: [
    HomePromoComponent,
    HomeSpecializationComponent,
    HomeDoctorListComponent,
    HomeLoginComponent,
  ],
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {}
