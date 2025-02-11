import { Component } from '@angular/core';
import { HomePromoComponent } from '../../components/home-promo/home-promo.component';
import { HomeSpecializationComponent } from '../../components/home-specialization/home-specialization.component';

@Component({
  selector: 'app-home-page',
  imports: [HomePromoComponent, HomeSpecializationComponent],
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {}
