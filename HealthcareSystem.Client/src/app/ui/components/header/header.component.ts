import { Component } from '@angular/core';
import { IHeaderLinks } from '../../interfaces/IHeaderLinks';
import { NgOptimizedImage } from '@angular/common';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: 'header.component.scss',
  imports: [NgOptimizedImage],
})
export class HeaderComponent {
  links: IHeaderLinks[] = [
    { url: '', title: 'Home' },
    { url: '', title: 'Doctors' },
    { url: '', title: 'Contact' },
    { url: '', title: 'About' },
  ];
}
