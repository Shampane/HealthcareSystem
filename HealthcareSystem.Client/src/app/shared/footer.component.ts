import { Component } from '@angular/core';
import { PagesLinks } from '../interfaces/view/pagesLinks.interface';

@Component({
  selector: 'app-footer',
  imports: [],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.scss',
})
export class FooterComponent {
  links: PagesLinks[] = [
    { url: '', title: 'Home' },
    { url: '', title: 'Doctors' },
    { url: '', title: 'Contact' },
    { url: '', title: 'About' },
  ];
}
