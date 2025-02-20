import { Component } from '@angular/core';
import { PagesLinks } from '../interfaces/view/pagesLinks.interface';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: 'header.component.scss',
})
export class HeaderComponent {
  links: PagesLinks[] = [
    { url: '', title: 'Home' },
    { url: '', title: 'Doctors' },
    { url: '', title: 'Contact' },
    { url: '', title: 'About' },
  ];
}
