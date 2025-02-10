import { Component } from '@angular/core';
import { HomePromoComponent } from '../../components/home-promo/home-promo.component';

@Component({
  selector: 'app-home-page',
  imports: [HomePromoComponent],
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {}
